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

#include <TaskScheduler.h>

#include "AnalogInput.h"
#include "Config.h"
#include "HubConfiguration.h"
#include "SQM.h"
#include "TemperatureSensors.h"
#include "USB.h"
#include "Weather.h"
#include "VoltageMonitor.h"

static Scheduler taskScheduler;
static HubConfiguration configuration;
static Task usbTask(10, TASK_FOREVER, &USB::loop);
static Task sqmTask(1000, TASK_FOREVER, &SQM::loop);
static Task analogInputTask(100, TASK_FOREVER, &AnalogInput::loop);
static Task temperatureSensorsTask(2000, TASK_FOREVER, &TemperatureSensors::loop);
static Task weatherTask(2000, TASK_FOREVER, &Weather::loop);
static Task voltageMonitorTask(1000, TASK_FOREVER, &VoltageMonitor::loop);

void setup() {
    configuration.loadStoredConfiguration();

    taskScheduler.init();
    USB::init(&configuration);
    taskScheduler.addTask(usbTask);
/*
    AnalogInput::init(&configuration);
    taskScheduler.addTask(analogInputTask);

    VoltageMonitor::init();
    taskScheduler.addTask(voltageMonitorTask);

    if (configuration.getFactoryConfig().features.sqm) {
        SQM::init(&configuration);
        taskScheduler.addTask(sqmTask);
    }
    if (configuration.getFactoryConfig().features.onewire) {
        TemperatureSensors::init();
        taskScheduler.addTask(temperatureSensorsTask);
    }
    if (configuration.getFactoryConfig().features.bme280) {
        Weather::init();
        taskScheduler.addTask(weatherTask);
    }
*/
    taskScheduler.enableAll();
}

void loop() {
    taskScheduler.execute();
}