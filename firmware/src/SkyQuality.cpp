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

#include <math.h>
#include <FreqCount.h>
#include <FreqMeasure.h>

#include "HubConfiguration.h"
#include "KomaHubPins.h"
#include "SkyQuality.h"

SkyQuality::Mode SkyQuality::mode = COUNT;
int SkyQuality::freq = 0;
bool SkyQuality::sensorPresent;
HubConfiguration* SkyQuality::hubConfiguration;

void SkyQuality::init(HubConfiguration* hubConfiguration) {
    SkyQuality::hubConfiguration = hubConfiguration;
    SkyQuality::freq = 0;
    SkyQuality::sensorPresent = false;

    pinMode(KomaHub::AUX1, INPUT);
    pinMode(KomaHub::AUX1_2, INPUT);

    switchToCount();
}

void SkyQuality::stop() {
    FreqMeasure.end();
    FreqCount.end();
}

void SkyQuality::switchToMeasure() {
    FreqMeasure.begin();
    FreqCount.end();
    SkyQuality::mode = MEASURE;
}

void SkyQuality::switchToCount() {
    FreqCount.begin(1000);
    FreqMeasure.end();
    SkyQuality::mode = COUNT;
}

void SkyQuality::loop() {
    switch (SkyQuality::mode) {
        case MEASURE:
        {
            if (FreqMeasure.available()) {
                freq = FreqMeasure.read();
            }
            if (freq > 1500) {
                switchToCount();
            }
            break;
        }
        case COUNT:
        {
            if (FreqMeasure.available()) {
                freq = FreqCount.read();
            }
            if (freq < 1000) {
                switchToMeasure();
            }
            break;
        }
    }
    if (freq > 0)
        sensorPresent = true;
}

int SkyQuality::getFrequencyHz() {
    return freq;
}

float SkyQuality::getSkyQuality() {
    return hubConfiguration->getFactoryConfig().skyQualityOffset/10.0 - 2.5 * log10(freq);
}

bool SkyQuality::isSensorPresent() {
    return sensorPresent;
}