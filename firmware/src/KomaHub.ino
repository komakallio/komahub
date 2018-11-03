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
#ifdef CORE_TEENSY_SERIAL
#include <EEPROM.h>
#elif defined(CORE_TEENSY_RAWHID) && !defined(CORE_KOMAHUB_OK)
#error "KomaHub custom core (with proper USB vendor/product ids) not found!"
#elif !defined(CORE_TEENSY_RAWHID)
#error "Teensy core (serial/raw hid) not selected!"
#endif

#include "AnalogInput.h"
#include "HubConfiguration.h"
#include "PowerOutputs.h"
#include "SkyQuality.h"
#include "SkyTemperature.h"
#include "TemperatureSensors.h"
#include "USB.h"
#include "Weather.h"
#include "VersionSpecifics.h"
#include "VoltageMonitor.h"

static Scheduler taskScheduler;
static HubConfiguration configuration;
#ifdef CORE_TEENSY_RAWHID
static Task usbTask(10, TASK_FOREVER, &USB::loop);
#endif
static Task skyQualityTask(1000, TASK_FOREVER, &SkyQuality::loop);
static Task skyTemperatureTask(1000, TASK_FOREVER, &SkyTemperature::loop);
static Task analogInputTask(10, TASK_FOREVER, &AnalogInput::loop);
static Task powerOutputsTask(5, TASK_FOREVER, &PowerOutputs::loop);
static Task temperatureSensorsTask(2000, TASK_FOREVER, &TemperatureSensors::loop);
static Task weatherTask(2000, TASK_FOREVER, &Weather::loop);
static Task voltageMonitorTask(10, TASK_FOREVER, &VoltageMonitor::loop);

void setup() {
    configuration.loadStoredConfiguration();
    VersionSpecifics::setBoardRevision(configuration.getFactoryConfig().boardRevision);

    taskScheduler.init();
#ifdef CORE_TEENSY_RAWHID
    USB::init(&configuration);
    taskScheduler.addTask(usbTask);
#endif

    AnalogInput::init(&configuration);
    taskScheduler.addTask(analogInputTask);

    PowerOutputs::init(&configuration);
    taskScheduler.addTask(powerOutputsTask);

    VoltageMonitor::init(&configuration);
    taskScheduler.addTask(voltageMonitorTask);

    SkyQuality::init(&configuration);
    taskScheduler.addTask(skyQualityTask);

    SkyTemperature::init(&configuration);
    taskScheduler.addTask(skyTemperatureTask);

    TemperatureSensors::init(&configuration);
    taskScheduler.addTask(temperatureSensorsTask);

    Weather::init(&configuration);
    taskScheduler.addTask(weatherTask);

    taskScheduler.enableAll();
}

void loop() {
    taskScheduler.execute();
#ifdef CORE_TEENSY_SERIAL
    if (Serial.available()) {
        char ch = Serial.read();
        if (ch == 'd') {
            Serial.print("eeprom dump: ");
            for (int i = 0; i < 1024; i++) {
                if (i % 32 == 0) Serial.println();
                Serial.print(EEPROM.read(i), HEX);
                Serial.print(" ");
            };
            Serial.println();
        }
        if (ch == 'f') {
            configuration.factoryReset(1, 6, 12000, 2000, 255, 220, 15);
            Serial.println("factory was reset");
        }
    }
#endif
}
