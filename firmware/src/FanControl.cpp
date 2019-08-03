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
#include "KomaHubPins.h"
#include "FanControl.h"
#include "HubConfiguration.h"
#include "TemperatureSensors.h"
#include "Weather.h"

#define FAN_COOLDOWN 1.0
#define FAN_IDLE 0.5
#define FAN_TEMP_HYSTERESIS 0.5

HubConfiguration* FanControl::hubConfiguration;
float FanControl::currentFanSpeed = 0.0f;

void FanControl::init(class HubConfiguration* configuration) {
    hubConfiguration = configuration;
    pinMode(KomaHub::AUX1, OUTPUT);
    TCCR4A = 0xA3;            // COM4A1, COM4B1, PWM4A, PWM4B
    TCCR4B = 0x04;            // CK/8  -> 3,92kHz (8MHz 10Bit??))
}

void FanControl::setFanSpeed(float percentage) {
    currentFanSpeed = percentage;
    analogWrite(KomaHub::AUX1, (int)(percentage*255));
}

void FanControl::loop() {
    for (int output = 0; output < 6; output++) {
        if (hubConfiguration->getOutputSettings().outputs[0].type.type != PWM_FAN) {
            continue;
        }

        int sensor = hubConfiguration->getOutputSettings().outputs[0].type.pidSensor;
        float temperature = TemperatureSensors::getCurrentTemperatureValues()[sensor];

        if (currentFanSpeed == FAN_IDLE && temperature > Weather::getTemperature() + FAN_TEMP_HYSTERESIS) {
            setFanSpeed(FAN_COOLDOWN);
        }
        if (currentFanSpeed == FAN_COOLDOWN && temperature < Weather::getTemperature()) {
            setFanSpeed(FAN_IDLE);
        }
    }
}
