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

#include <Adafruit_BME280.h>
#include "HubConfiguration.h"
#include "Weather.h"

static Adafruit_BME280 bme;

HubConfiguration* Weather::hubConfiguration;

float Weather::temperature;
float Weather::pressure;
float Weather::humidity;

bool Weather::sensorPresent = false;

void Weather::init(HubConfiguration* hubConfiguration) {
    Weather::hubConfiguration = hubConfiguration;

    temperature = 0;
    pressure = 0;
    humidity = 0;

    sensorPresent = bme.begin(0x76);
    if (sensorPresent) {
        bme.setSampling(
            Adafruit_BME280::MODE_NORMAL,
            Adafruit_BME280::SAMPLING_X16,
            Adafruit_BME280::SAMPLING_X16,
            Adafruit_BME280::SAMPLING_X16,
            Adafruit_BME280::FILTER_OFF,
            Adafruit_BME280::STANDBY_MS_1000);
    }
}

void Weather::loop() {
    if (!sensorPresent)
        return;

    temperature = bme.readTemperature();
    pressure = bme.readPressure();
    humidity = bme.readHumidity();
}

float Weather::getTemperature() {
    return temperature;
}

float Weather::getPressure() {
    return pressure;
}

float Weather::getHumidity() {
    return humidity;
}

float Weather::getDewPoint() {
    // replace with better formula
    return temperature - ((100 - humidity)/5.0);
}

bool Weather::isSensorPresent() {
    return sensorPresent;
}
