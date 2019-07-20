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

#include "AnalogInput.h"
#include "FanControl.h"
#include "HubConfiguration.h"
#include "KomaHubPins.h"
#include "PowerOutputs.h"
#include "USBCommands.h"
#include "VersionSpecifics.h"

#define PWM_CYCLE_COUNT 20

HubConfiguration* PowerOutputs::hubConfiguration;
int PowerOutputs::pwmCounter;

static float measurementBuffer[8*6];
static uint8_t measurementBufferHead;

bool PowerOutputs::pwmState(const HubConfiguration::State& state, int output) {
    if ((state.relayIsOpenBits & (1 << output)) == 0) {
        return false;
    }

    int duty = state.pwmPercentages[output];
    int cycleOffset = output * PWM_CYCLE_COUNT / 6;
    int pos = 100 * ((pwmCounter + cycleOffset) % PWM_CYCLE_COUNT) / PWM_CYCLE_COUNT;
    return (pos < duty);
}

void PowerOutputs::init(HubConfiguration* hubConfiguration) {
    PowerOutputs::hubConfiguration = hubConfiguration;
    pwmCounter = 0;
    memset(measurementBuffer, 0, 8*6*sizeof(float));
}

void PowerOutputs::loop() {
    pwmCounter = (pwmCounter + 1) % 20;
    if (pwmCounter == 0) {
        AnalogInput::resetAverageCollectingPeriod();
        measurementBufferHead = (measurementBufferHead + 1) & 0x7;
        for (int i = 0; i < 6; i++) {
            measurementBuffer[measurementBufferHead*6+i] = getOutputPower(i);
        }
    }
    tripFusesIfNecessary();
    updatePowerOutputs();
}

bool PowerOutputs::isInOverload(int output, int numSamplesToCheck) {
    const HubConfiguration::OutputSettings& outputSettings = hubConfiguration->getOutputSettings();

    int p = measurementBufferHead;
    for (int i = 0; i < numSamplesToCheck; i++) {
        if (measurementBuffer[p*6 + output] < outputSettings.outputs[output].fuseCurrent/10.0f) {
            return false;
        }
        p = (p - 1) & 0x7;
    }

    return true;
}

void PowerOutputs::updatePowerOutputs() {
    const HubConfiguration::OutputSettings& outputSettings = hubConfiguration->getOutputSettings();
    const HubConfiguration::State& state = hubConfiguration->getState();

    int outputPins[] = { KomaHub::P1, KomaHub::P2, KomaHub::P3, KomaHub::P4, KomaHub::P5, KomaHub::P6 };
    for (int output = 0; output < 6; output++) {
        if ((state.fuseIsBlownBits & (1 << output)) != 0) {
            digitalWrite(outputPins[output], LOW);
            continue;
        }

        switch (outputSettings.outputs[output].type.type) {
            case OFF:
                digitalWrite(outputPins[output], LOW);
                break;
            case DC:
                digitalWrite(outputPins[output], (state.relayIsOpenBits & (1 << output)) != 0 ? HIGH : LOW);
                break;
            case PWM:
                digitalWrite(outputPins[output], pwmState(state, output));
                break;
            case PWM_FAN:
                digitalWrite(outputPins[output], state.pwmPercentages[output] > 0 ? HIGH : LOW);
                FanControl::setFanSpeed(state.pwmPercentages[output]);
                break;
        }
    }
}

void PowerOutputs::tripFusesIfNecessary() {
    const HubConfiguration::FactoryConfig& factoryConfig = hubConfiguration->getFactoryConfig();
    HubConfiguration::State& state = hubConfiguration->getState();
    uint8_t oldFuseIsBlownBits = state.fuseIsBlownBits;
    for (int output = 0; output < 6; output++) {
        if (isInOverload(output, factoryConfig.fuseDelay/10)) {
            state.fuseIsBlownBits |= (1 << output);
        }
    }

    if (oldFuseIsBlownBits != state.fuseIsBlownBits) {
        hubConfiguration->saveState();
    }
}

float PowerOutputs::getOutputPower(int outputNumber) {
    const HubConfiguration::Output& output = hubConfiguration->getOutputSettings().outputs[outputNumber];

    uint16_t adcValue = AnalogInput::getAverageValues()[outputNumber+1];
    if (adcValue == 0) {
        return 0.0f;
    }

    // Approximate true power usage with a second-order function
    float divisor = VersionSpecifics::getPowerOutputACoefficientDivisor();
    return 0.001 * ((output.coeffs.a/divisor)*adcValue*adcValue + output.coeffs.b*adcValue + output.coeffs.c);
}
