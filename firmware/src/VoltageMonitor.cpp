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

#include "AnalogInput.h"
#include "HubConfiguration.h"
#include "VoltageMonitor.h"

HubConfiguration* VoltageMonitor::hubConfiguration;

void VoltageMonitor::init(HubConfiguration* configuration) {
    VoltageMonitor::hubConfiguration = configuration;
}

void VoltageMonitor::loop() {
    float inputVoltage = getInputVoltage();
    if (inputVoltage < 10.0f || inputVoltage > 15.0f) {
        // shut down all ports?
    }
}

float VoltageMonitor::getInputVoltage() {
    uint16_t adcValue = AnalogInput::getAverageValues()[0];
    return hubConfiguration->getFactoryConfig().r6r7divisor * 4.096 * adcValue / 1024.0f;
}

