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
#include <Adafruit_MLX90614.h>

#include "HubConfiguration.h"
#include "SkyTemperature.h"

HubConfiguration* SkyTemperature::hubConfiguration;

static Adafruit_MLX90614 mlx = Adafruit_MLX90614();
float SkyTemperature::skyTemperature = 0.0f;
float SkyTemperature::ambientTemperature = 0.0f;
bool SkyTemperature::sensorPresent;

void SkyTemperature::init(HubConfiguration* hubConfiguration) {
    SkyTemperature::hubConfiguration = hubConfiguration;

    sensorPresent = mlx.begin();
    if (sensorPresent) 
        sensorPresent = mlx.readObjectTempC() < 100.0;
}

void SkyTemperature::loop() {
    if (!sensorPresent)
        return;

    skyTemperature = mlx.readObjectTempC();
    ambientTemperature = mlx.readAmbientTempC();
}

float SkyTemperature::getSkyTemperature() {
    return skyTemperature;
}

float SkyTemperature::getAmbientTemperature() {
    return ambientTemperature;
}

bool SkyTemperature::isSensorPresent() {
    return sensorPresent;
}