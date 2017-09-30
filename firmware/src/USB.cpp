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
#include <EEPROM.h>

#ifdef CORE_TEENSY_RAWHID

#include "AnalogInput.h"
#include "HubConfiguration.h"
#include "USB.h"
#include "USBCommands.h"
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
                uint8_t *dst = &usbSendBuffer[0];
                *dst++ = KOMAHUB_FIRMWARE_VERSION_MAJOR;
                *dst++ = KOMAHUB_FIRMWARE_VERSION_MINOR;
                const HubConfiguration::FactoryConfig& factoryConfig = hubConfiguration->getFactoryConfig();

                *dst++ = (factoryConfig.serial & 0xFF00) >> 8;
                *dst++ = factoryConfig.serial & 0xFF;
                *dst++ = factoryConfig.fuseSpeed;
                *dst++ = factoryConfig.sqmZeroPoint;
                *dst++ = (uint8_t)(
                    (factoryConfig.features.tempprobes ? (1 << 0) : 0) +
                    (factoryConfig.features.skyquality ? (1 << 1) : 0) +
                    (factoryConfig.features.ambientpth ? (1 << 2) : 0) +
                    (factoryConfig.features.skytemp ? (1 << 3) : 0));
                RawHID.send(usbSendBuffer, 1000);
                break;
            }

            case GETOUTPUTSETTINGS: {
                GetOutputSettingsCommand* cmd = (GetOutputSettingsCommand*)&data[pos];
                int output = cmd->outputNumber;
                
                uint8_t *dst = &usbSendBuffer[0];
                const HubConfiguration::OutputSettings& outputSettings = hubConfiguration->getOutputSettings();
                
                for (unsigned int i = 0; i < 16; i++)  {
                    *dst++ = outputSettings.outputs[output].name[i];
                }
                *dst++ = outputSettings.outputs[output].fuseCurrent;
                *dst++ = outputSettings.outputs[output].type.type;

                RawHID.send(usbSendBuffer, 1000);
                pos += sizeof(GetOutputSettingsCommand);
                break;
            }

            case GETSTATUS: {
                uint8_t *dst = &usbSendBuffer[0];
                const HubConfiguration::State& state = hubConfiguration->getState();
                *dst++ = state.relayIsOpenBits;
                *dst++ = state.fuseIsBlownBits;
                for (int i = 0; i < 6; i++) {
                    *dst++ = state.pwmPercentages[i];
                }
                // voltage
                *dst++ = (uint8_t)(VoltageMonitor::getInputVoltage() * 10);
                // amps
                for (int i = 0; i < 6; i++) {
                    *dst++ = (uint8_t)(VoltageMonitor::getOutputPower(i) * 10);
                }
                // temperatures
                // weather
                // sqm
                RawHID.send(usbSendBuffer, 1000);
                break;
            }

            case DUMPFACTORY: {
                uint8_t *dst = usbSendBuffer;
                for (unsigned int i = 0; i < sizeof(HubConfiguration::FactoryConfig); i++)
                    *dst++ = EEPROM.read(i);
                RawHID.send(usbSendBuffer, 1000);
                break;
            }
            case DUMPOUTPUTS: {
                uint8_t *dst = usbSendBuffer;
                for (unsigned int i = 0; i < 64; i++)
                    *dst++ = EEPROM.read(i + 32);
                RawHID.send(usbSendBuffer, 1000);
                break;
            }
            case DUMPSTATE: {
                uint8_t *dst = usbSendBuffer;
                uint8_t *src = (uint8_t*)&hubConfiguration->getState();
                for (unsigned int i = 0; i < sizeof(HubConfiguration::State); i++)
                    *dst++ = src[i];
                RawHID.send(usbSendBuffer, 1000);
                break;
            }

            case FACTORYRESET: {
                FactoryResetCommand* cmd = (FactoryResetCommand*)&data[pos];
                USB::hubConfiguration->factoryReset(cmd->serial, cmd->r6ohms, cmd->r7ohms);
                pos += sizeof(FactoryResetCommand);
                break;
            }
            case SETRELAY: {
                SetRelayCommand* cmd = (SetRelayCommand*)&data[pos];
                if (cmd->enabled) {
                    hubConfiguration->getState().relayIsOpenBits |= (1 << cmd->outputNumber);
                } else {
                    hubConfiguration->getState().relayIsOpenBits &= !(1 << cmd->outputNumber);
                }
                hubConfiguration->saveState();
                pos += sizeof(SetRelayCommand);
                break;
            }
            case SETPWMDUTY: {
                SetPwmDutyCommand* cmd = (SetPwmDutyCommand*)&data[pos];
                hubConfiguration->getState().pwmPercentages[cmd->outputNumber] = cmd->duty;
                hubConfiguration->saveState();
                pos += sizeof(SetPwmDutyCommand);
                break;
            }
            case RESETFUSE: {
                ResetFuseCommand* cmd = (ResetFuseCommand*)&data[pos];
                hubConfiguration->getState().fuseIsBlownBits &= !(1 << cmd->outputNumber);
                hubConfiguration->getState().relayIsOpenBits &= !(1 << cmd->outputNumber);
                hubConfiguration->saveState();
                pos += sizeof(ResetFuseCommand);
                break;
            }
            case CONFIGUREOUTPUT: {
                HubConfiguration::OutputSettings& outputSettings = hubConfiguration->getOutputSettings();

                ConfigureOutputCommand* cmd = (ConfigureOutputCommand*)&data[pos];
                outputSettings.outputs[cmd->outputNumber].type.type = cmd->outputType;
                outputSettings.outputs[cmd->outputNumber].fuseCurrent = cmd->fuseCurrent;
                memcpy(outputSettings.outputs[cmd->outputNumber].name, cmd->name, 16);
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

#endif