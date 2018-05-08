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
#include <DS1620.h>
#include "HubConfiguration.h"
#include "KomaHubPins.h"
#include "TemperatureSensors.h"

static DS1620 primarySensor(KomaHub::SS, KomaHub::SDA, KomaHub::SCL);
static DS1620 secondarySensor(KomaHub::SCLK, KomaHub::MISO, KomaHub::MOSI);

int TemperatureSensors::numberOfSensors = 0;
float TemperatureSensors::temperatures[4] = {0};
bool TemperatureSensors::temperaturesRequested = false;
HubConfiguration* TemperatureSensors::hubConfiguration;

void TemperatureSensors::init(HubConfiguration* hubConfiguration) {
    TemperatureSensors::hubConfiguration = hubConfiguration;
    temperaturesRequested = false;
    numberOfSensors = 2;
}

void TemperatureSensors::loop() {
    if (temperaturesRequested) {
        temperatures[0] = primarySensor.getTemperature();
        temperatures[1] = secondarySensor.getTemperature();
    }

    primarySensor.startConversion();
    secondarySensor.startConversion();
    temperaturesRequested = true;
}

const float* TemperatureSensors::getCurrentTemperatureValues() {
    return temperatures;
}

int TemperatureSensors::getNumberOfSensors() {
    return numberOfSensors;
}
