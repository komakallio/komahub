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

SkyQuality::Mode SkyQuality::mode = MEASURE;
float SkyQuality::freq = 0;
bool SkyQuality::sensorPresent;
HubConfiguration* SkyQuality::hubConfiguration;

static double sum = 0;
static int count = 0;
static unsigned long lastMeasureTime = 0;
static bool usingTimer = false;

void SkyQuality::init(HubConfiguration* hubConfiguration) {
    SkyQuality::hubConfiguration = hubConfiguration;
    SkyQuality::freq = 0;
    SkyQuality::sensorPresent = false;

    pinMode(KomaHub::AUX1, INPUT);
    pinMode(KomaHub::AUX1_2, INPUT);

    switchToMeasure();
}

void SkyQuality::stop() {
    FreqMeasure.end();
    FreqCount.end();
}

void SkyQuality::switchToMeasure() {
    FreqMeasure.begin();
    FreqCount.end();
    SkyQuality::mode = MEASURE;
    lastMeasureTime = millis();
    sum = count = 0;
}

void SkyQuality::switchToCount() {
    FreqCount.begin(1000);
    FreqMeasure.end();
    SkyQuality::mode = COUNT;
    sum = count = 0;
}

void SkyQuality::loop() {
    switch (SkyQuality::mode) {
        case MEASURE:
        {
            if (FreqMeasure.available()) {
                SkyQuality::sensorPresent = true;

                unsigned long now = millis();
                unsigned long timeSinceLastMeasure = now - lastMeasureTime;
                lastMeasureTime = now;
                if (timeSinceLastMeasure > 1000) {
                    // Over once second since last pulse. Just measure the time interval directly when we are below 1Hz
                    // (SkyQuality::loop is driven at 20Hz so there is +-25ms error which shouldn't skew results too much)
                    usingTimer = true;
                    SkyQuality::freq = 1.0/(timeSinceLastMeasure/1000.0);
                    return;
                }

                if (usingTimer) {
                    // Previous freq sample was produced from a time interval 
                    // Reset counters to continue normal operations
                    usingTimer = false;
                    count = sum = 0;
                }

                sum += FreqMeasure.read();
                count = count + 1;
                if (count > 10) {
                    SkyQuality::freq = (sum > 0 ? FreqMeasure.countToFrequency(sum/count) : 0);
                    sum = count = 0;
                }
            }
            
            if (SkyQuality::freq > 1500) {
                switchToCount();
            }
            break;
        }
        case COUNT:
        {
            if (FreqCount.available()) {
                SkyQuality::sensorPresent = true;
                sum += FreqCount.read();
                count++;

                if (count >= 5) {
                    SkyQuality::freq = sum/count;
                    sum = count = 0;
                }                
            }
            if (SkyQuality::freq < 1000) {
                switchToMeasure();
            }
            break;
        }
    }
}

float SkyQuality::getFrequencyHz() {
    return freq;
}

float SkyQuality::getSkyQuality() {
    if (freq > 0)
        return hubConfiguration->getFactoryConfig().skyQualityOffset/10.0 - 2.5 * log10(freq);
    else
        return 22.0f;
}

bool SkyQuality::isSensorPresent() {
    return sensorPresent;
}