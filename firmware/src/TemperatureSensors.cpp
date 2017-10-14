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

#include <DallasTemperature.h>
#include <OneWire.h>
#include "HubConfiguration.h"
#include "KomaHubPins.h"
#include "TemperatureSensors.h"

static OneWire oneWire(KomaHub::AUX2);
static DallasTemperature sensors(&oneWire);

int TemperatureSensors::numberOfSensors = 0;
float TemperatureSensors::temperatures[4] = {0};
bool TemperatureSensors::temperaturesRequested = false;
HubConfiguration* TemperatureSensors::hubConfiguration;

void TemperatureSensors::init(HubConfiguration* hubConfiguration) {
    if (!hubConfiguration->getFactoryConfig().features.tempprobes)
        return;

    TemperatureSensors::hubConfiguration = hubConfiguration;

    temperaturesRequested = false;
    sensors.begin();
    sensors.setWaitForConversion(false);
    numberOfSensors = min(4, sensors.getDeviceCount());
}

void TemperatureSensors::loop() {
    if (!hubConfiguration->getFactoryConfig().features.tempprobes)
        return;

    if (temperaturesRequested) {
        for (int i = 0; i < numberOfSensors; i++) {
            temperatures[i] = sensors.getTempCByIndex(i);
        }
    }

    sensors.requestTemperatures();
    temperaturesRequested = true;
}

const float* TemperatureSensors::getCurrentTemperatureValues() {
    return temperatures;
}

int TemperatureSensors::getNumberOfSensors() {
    return numberOfSensors;
}
