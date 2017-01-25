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
        uint8_t cmdlength = data[pos++];
        if (pos+cmdlength >= maxlen) {
            return;
        }

        uint8_t command = data[pos++];
        switch (command) {
            case END: {
                return;
            }

            case STATUS: {
                uint8_t *dst = &usbSendBuffer[0];
                *dst++ = KOMAHUB_FIRMWARE_VERSION_MAJOR;
                *dst++ = KOMAHUB_FIRMWARE_VERSION_MINOR;
                *dst++ = hubConfiguration->getFactoryConfig().numberOfOutputs;
                *dst++ = (hubConfiguration->getFactoryConfig().serial & 0xFF00) >> 8;
                *dst++ = hubConfiguration->getFactoryConfig().serial & 0xFF;
                const HubConfiguration::State& state = hubConfiguration->getState();
                *dst++ = state.relayIsOpenBits;
                *dst++ = state.fuseIsBlownBits;
                *dst++ = state.relayIsPwmBits;
                *dst++ = state.pwmIsFastBits;
                for (int i = 0; i < 7; i++) {
                    *dst++ = state.pwmPercentages[i];
                }
                // voltage
                *dst++ = (uint8_t)(VoltageMonitor::getInputVoltage() * 10);
                // amps
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

            case FACTORYRESET: {
                if (cmdlength-1 != sizeof(FactoryResetCommand)) {
                    pos += cmdlength-1;
                    continue;
                }

                FactoryResetCommand* cmd = (FactoryResetCommand*)&data[pos];
                USB::hubConfiguration->factoryReset(cmd->serial, cmd->numberOfOutputs, cmd->r6ohms, cmd->r7ohms, cmd->features, cmd->sqmZeroPoint, cmd->fuseSpeed);
                pos += cmdlength-1;
                break;
            }
            case SETRELAY: {
                if (cmdlength-1 != sizeof(SetRelayCommand)) {
                    pos += cmdlength-1;
                    continue;
                }

                SetRelayCommand* cmd = (SetRelayCommand*)&data[pos];
                if (cmd->enabled) {
                    hubConfiguration->getState().relayIsOpenBits |= (1 << cmd->outputNumber);
                } else {
                    hubConfiguration->getState().relayIsOpenBits &= !(1 << cmd->outputNumber);
                }
//                hubConfiguration->saveState();
                pos += cmdlength-1;
                break;
            }
            case SETPWMDUTY: {
                if (cmdlength-1 != sizeof(SetPwmDutyCommand)) {
                    pos += cmdlength-1;
                    continue;
                }

                pinMode(11, OUTPUT);
                digitalWrite(11, HIGH);
                delay(1000);
                digitalWrite(11, LOW);

                SetPwmDutyCommand* cmd = (SetPwmDutyCommand*)&data[pos];
                hubConfiguration->getState().pwmPercentages[cmd->outputNumber] = cmd->duty;
//                hubConfiguration->saveState();
                pos += cmdlength-1;
                break;
            }
            case RESETFUSE: {
                if (cmdlength-1 != sizeof(ResetFuseCommand)) {
                    pos += cmdlength-1;
                    continue;
                }

                ResetFuseCommand* cmd = (ResetFuseCommand*)&data[pos];
                hubConfiguration->getState().fuseIsBlownBits &= !(1 << cmd->outputNumber);
//                hubConfiguration->saveState();
                pos += cmdlength-1;
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
