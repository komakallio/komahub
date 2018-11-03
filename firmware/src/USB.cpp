/*
 * Copyright (c) 2016 Jari Saukkonen
 *
 * Permission is hereby granted, free of charge, to any person obtaining
 * a copy of this software and associated documentation files (the
 * "Software"), to deal in the Software without restriction, including
 * without limitation the rights to use, copy, modify, merge, publish,
 * distribute, sublicense, and/or sell copies of the Software, and to
 * permit persons to whom the Software is furnished to do so, subject to
 * the following conditions:
 *
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
 * MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
 * IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY
 * CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
 * TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
 * SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

#include <Arduino.h>

#ifdef CORE_TEENSY_RAWHID

#include "AnalogInput.h"
#include "HubConfiguration.h"
#include "PowerOutputs.h"
#include "SkyQuality.h"
#include "SkyTemperature.h"
#include "TemperatureSensors.h"
#include "USB.h"
#include "USBCommands.h"
#include "Weather.h"
#include "Version.h"
#include "VoltageMonitor.h"

static uint8_t usbSendBuffer[64];
static uint8_t usbRecvBuffer[64];

HubConfiguration* USB::hubConfiguration;

void USB::loop() {
    int available = RawHID.available();
    if (available > 0) {
        memset(usbSendBuffer, 0, 64);
        RawHID.recv(usbRecvBuffer, available);
        handleCommands(usbRecvBuffer, available);
    }
}

void USB::handleCommands(uint8_t* data, unsigned int maxlen) {
    unsigned int pos = 0;
    if (data[pos++] != 'K')
        return;

    while (pos < maxlen) {
        uint8_t command = data[pos++];
        switch (command) {
            case END: {
                return;
            }

            case GETFACTORYSETTINGS: {
                const HubConfiguration::FactoryConfig& factoryConfig = hubConfiguration->getFactoryConfig();
                GetFactorySettingsResponse response;

                response.firmwareVersion = (KOMAHUB_FIRMWARE_VERSION_MAJOR << 8) + KOMAHUB_FIRMWARE_VERSION_MINOR;
                response.serialNumber = factoryConfig.serial;
                response.fuseDelay = factoryConfig.fuseDelay;
                response.skyQualityOffset = factoryConfig.skyQualityOffset;
                response.features = (uint8_t)(
                    (factoryConfig.features.tempprobes ? (1 << 0) : 0) +
                    (factoryConfig.features.skyquality ? (1 << 1) : 0) +
                    (factoryConfig.features.ambientpth ? (1 << 2) : 0) +
                    (factoryConfig.features.skytemp ? (1 << 3) : 0));
                response.boardRevision = factoryConfig.boardRevision;

                memcpy(usbSendBuffer, &response, sizeof(GetFactorySettingsResponse));
                RawHID.send(usbSendBuffer, 1000);
                break;
            }

            case GETOUTPUTSETTINGS: {
                GetOutputSettingsCommand* cmd = (GetOutputSettingsCommand*)&data[pos];
                if (!isValidOutput(cmd->outputNumber))
                    break;
                GetOutputSettingsResponse response;

                const HubConfiguration::OutputSettings& outputSettings = hubConfiguration->getOutputSettings();
                const HubConfiguration::Output& output = outputSettings.outputs[cmd->outputNumber];
                memcpy(&response.name, output.name, 16);
                response.fuseCurrent = output.fuseCurrent;
                response.type = output.type.type;

                memcpy(usbSendBuffer, &response, sizeof(GetOutputSettingsResponse));
                RawHID.send(usbSendBuffer, 1000);
                pos += sizeof(GetOutputSettingsCommand);
                break;
            }

            case GETSTATUS: {
                GetStatusResponse response;

                const HubConfiguration::State& state = hubConfiguration->getState();
                response.relayIsOpenBits = state.relayIsOpenBits;
                response.fuseIsBlownBits = state.fuseIsBlownBits;
                for (int i = 0; i < 6; i++) {
                    response.pwmPercentages[i] = state.pwmPercentages[i];
                }
                // input voltage
                response.inputVoltage = (uint8_t)roundf(VoltageMonitor::getInputVoltage() * 10);
                // output power, amps
                for (int i = 0; i < 6; i++) {
                    response.outputPower[i] = (uint8_t)roundf(PowerOutputs::getOutputPower(i) * 10);
                }
                // 1-wire temperature probes
                response.numberOfTemperatureProbes = TemperatureSensors::getNumberOfSensors();
                for (int i = 0; i < 4; i++) {
                    response.temperatureProbes[i] = TemperatureSensors::getCurrentTemperatureValues()[i] * 10;
                }
                // weather
                response.pthpresent = Weather::isSensorPresent();
                response.temperature = Weather::getTemperature() * 10;
                response.humidity = Weather::getHumidity();
                response.pressure = Weather::getPressure() * 10;
                response.dewpoint = Weather::getDewPoint() * 10;
                // sky quality
                response.skyqualitypresent = SkyQuality::isSensorPresent();
                response.skyquality = SkyQuality::getSkyQuality() * 10;
                response.skyqualityfreq = SkyQuality::getFrequencyHz() * 10;
                // sky temperature
                response.skytemperaturepresent = SkyTemperature::isSensorPresent();
                response.skytemperature = SkyTemperature::getSkyTemperature() * 10;
                response.skyambienttemperature = SkyTemperature::getAmbientTemperature() * 10;

                memcpy(usbSendBuffer, &response, sizeof(GetStatusResponse));
                RawHID.send(usbSendBuffer, 1000);
                break;
            }

            case GETRAWPOWERUSAGE: {
                GetRawPowerUsageResponse response;
                memcpy(&response.rawValues, AnalogInput::getAverageValues(), 8*sizeof(uint16_t));
                memcpy(usbSendBuffer, &response, sizeof(GetRawPowerUsageResponse));
                RawHID.send(usbSendBuffer, 1000);
                break;
            }

            case FACTORYRESET: {
                FactoryResetCommand* cmd = (FactoryResetCommand*)&data[pos];
                USB::hubConfiguration->factoryReset(cmd->serial, cmd->r6ohms, cmd->r7ohms, cmd->boardRevision);
                pos += sizeof(FactoryResetCommand);
                break;
            }

            case REBOOT_BOOTLOADER: {
                cli();
                // disable watchdog, if enabled
                // disable all peripherals
                UDCON = 1;
                UCSR1B = 0;
                delay(5);
                EIMSK = 0; PCICR = 0; SPCR = 0; ACSR = 0; EECR = 0; ADCSRA = 0;
                TIMSK0 = 0; TIMSK1 = 0; TIMSK3 = 0; TIMSK4 = 0; UCSR1B = 0; TWCR = 0;
                DDRB = 0; DDRC = 0; DDRD = 0; DDRE = 0; DDRF = 0; TWCR = 0;
                PORTB = 0; PORTC = 0; PORTD = 0; PORTE = 0; PORTF = 0;
                asm volatile("jmp 0x7000");
                break;
            }

            case SETRELAY: {
                SetRelayCommand* cmd = (SetRelayCommand*)&data[pos];
                if (!isValidOutput(cmd->outputNumber))
                    break;
                if (cmd->enabled) {
                    hubConfiguration->getState().relayIsOpenBits |= (1 << cmd->outputNumber);
                } else {
                    hubConfiguration->getState().relayIsOpenBits &= ~(1 << cmd->outputNumber);
                }
                hubConfiguration->saveState();
                pos += sizeof(SetRelayCommand);
                break;
            }

            case SETPWMDUTY: {
                SetPwmDutyCommand* cmd = (SetPwmDutyCommand*)&data[pos];
                if (!isValidOutput(cmd->outputNumber))
                    break;
                hubConfiguration->getState().pwmPercentages[cmd->outputNumber] = cmd->duty;
                hubConfiguration->saveState();
                pos += sizeof(SetPwmDutyCommand);
                break;
            }

            case RESETFUSE: {
                ResetFuseCommand* cmd = (ResetFuseCommand*)&data[pos];
                if (!isValidOutput(cmd->outputNumber))
                    break;
                hubConfiguration->getState().fuseIsBlownBits &= ~(1 << cmd->outputNumber);
                hubConfiguration->getState().relayIsOpenBits &= ~(1 << cmd->outputNumber);
                hubConfiguration->saveState();
                pos += sizeof(ResetFuseCommand);
                break;
            }

            case CONFIGUREOUTPUT: {
                HubConfiguration::OutputSettings& outputSettings = hubConfiguration->getOutputSettings();

                ConfigureOutputCommand* cmd = (ConfigureOutputCommand*)&data[pos];
                if (!isValidOutput(cmd->outputNumber))
                    break;
                outputSettings.outputs[cmd->outputNumber].type.type = cmd->outputType;
                outputSettings.outputs[cmd->outputNumber].fuseCurrent = cmd->fuseCurrent;
                memcpy(outputSettings.outputs[cmd->outputNumber].name, cmd->name, 16);
                hubConfiguration->saveOutputConfiguration();
                pos += sizeof(ConfigureOutputCommand);
                break;
            }

            case CONFIGURESETTINGS: {
                HubConfiguration::FactoryConfig& factoryConfig = hubConfiguration->getFactoryConfig();

                ConfigureSettingsCommand* cmd = (ConfigureSettingsCommand*)&data[pos];
                factoryConfig.fuseDelay = cmd->fuseDelay;
                factoryConfig.skyQualityOffset = cmd->skyQualityOffset;

                if (factoryConfig.features.skyquality && !cmd->featureSkyQuality)
                    SkyQuality::stop();
                if (!factoryConfig.features.tempprobes && cmd->featureTempProbe)
                    TemperatureSensors::init(hubConfiguration);
                if (!factoryConfig.features.skyquality && cmd->featureSkyQuality)
                    SkyQuality::init(hubConfiguration);
                if (!factoryConfig.features.skytemp && cmd->featureSkyTemperature)
                    SkyTemperature::init(hubConfiguration);
                if (!factoryConfig.features.ambientpth && cmd->featureAmbientPTH)
                    Weather::init(hubConfiguration);

                factoryConfig.features.tempprobes = cmd->featureTempProbe;
                factoryConfig.features.skyquality = cmd->featureSkyQuality;
                factoryConfig.features.ambientpth = cmd->featureAmbientPTH;
                factoryConfig.features.skytemp = cmd->featureSkyTemperature;
                hubConfiguration->saveFactoryConfig();

                pos += sizeof(ConfigureSettingsCommand);
                break;
            }

            case CALIBRATEOUTPUT: {
                HubConfiguration::OutputSettings& outputSettings = hubConfiguration->getOutputSettings();
                CalibrateOutputCommand* cmd = (CalibrateOutputCommand*)&data[pos];
                if (!isValidOutput(cmd->outputNumber))
                    break;
                outputSettings.outputs[cmd->outputNumber].coeffs.a = cmd->a;
                outputSettings.outputs[cmd->outputNumber].coeffs.b = cmd->b;
                outputSettings.outputs[cmd->outputNumber].coeffs.c = cmd->c;
                hubConfiguration->saveOutputConfiguration();
                pos += sizeof(ConfigureOutputCommand);
                break;
            }
        }
    }
}

void USB::handlePacket(uint8_t* buffer) {
    uint8_t identifier = buffer[0];
    unsigned int len = buffer[1];
    uint8_t* data = &buffer[2];
    if (identifier != 'K' ||
        len >= 62) {
        return;
    }

    handleCommands(data, len);
}

void USB::init(HubConfiguration* hubConfiguration) {
    USB::hubConfiguration = hubConfiguration;
}

bool USB::isValidOutput(int output) {
    return (output >= 0 && output <= 6);
}

#endif
