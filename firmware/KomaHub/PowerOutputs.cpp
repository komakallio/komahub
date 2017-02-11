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

#include "HubConfiguration.h"
#include "KomaHubPins.h"
#include "PowerOutputs.h"

#define PWM_CYCLE_LENGTH_MS 100

HubConfiguration* PowerOutputs::hubConfiguration;

static bool pwmState(const HubConfiguration::State& state, int pin) {
    if ((state.relayIsOpenBits & (1 << pin)) == 0) {
        return false;
    }

    int duty = state.pwmPercentages[pin];
    int cycleOffset = pin * PWM_CYCLE_LENGTH_MS / 6;
    int pos = 100 * ((millis() + cycleOffset) % PWM_CYCLE_LENGTH_MS) / PWM_CYCLE_LENGTH_MS;
    return (pos < duty);
}

void PowerOutputs::init(HubConfiguration* hubConfiguration) {
    PowerOutputs::hubConfiguration = hubConfiguration;
    pinMode(11, OUTPUT);   
}

void PowerOutputs::loop() {
    const HubConfiguration::FactoryConfig& factoryConfig = hubConfiguration->getFactoryConfig();
    const HubConfiguration::State& state = hubConfiguration->getState();
    int outputPins[] = { KomaHub::P1, KomaHub::P2, KomaHub::P3, KomaHub::P4, KomaHub::P5, KomaHub::P6 };
    for (int pin = 0; pin < factoryConfig.numberOfOutputs; pin++) {
        if ((state.relayIsPwmBits & (1 << pin)) != 0 && 
            (state.pwmIsFastBits & (1 << pin)) == 0) {
            digitalWrite(outputPins[pin], pwmState(state, pin));
            if (pin == 0)
                digitalWrite(11, pwmState(state, pin) ? HIGH : LOW);
        } else {
            digitalWrite(outputPins[pin], (state.relayIsOpenBits & (1 << pin)) != 0 ? HIGH : LOW);
            if (pin == 0)
                digitalWrite(11, (state.relayIsOpenBits & (1 << pin)) != 0 ? HIGH : LOW);
          }
    }
}
