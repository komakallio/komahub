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
#include <PID_v1.h>
#include "DewController.h"
#include "FanControl.h"
#include "HubConfiguration.h"
#include "TemperatureSensors.h"
#include "Weather.h"

#define POWER_OUTPUT_PRIMARY 3
#define POWER_OUTPUT_SECONDARY 4

#define TEMP_SENSOR_PRIMARY 1
#define TEMP_SENSOR_PRIMARY_OFFSET 0.5
#define TEMP_SENSOR_SECONDARY 0
#define TEMP_SENSOR_SECONDARY_OFFSET 0.5
#define DEWPOINT_OFFSET 2.0
#define FAN_COOLDOWN 1.0
#define FAN_IDLE 0.5

static const double Kp1 = 0, Ki1 = 0, Kd1 = 0;
static const double Kp2 = 0, Ki2 = 0, Kd2 = 0;

static double primaryInput, primaryOutput, secondaryInput, secondaryOutput, setpoint;
static PID primaryPid(&primaryInput, &primaryOutput, &setpoint, Kp1, Ki1, Kd1, DIRECT);
static PID secondaryPid(&secondaryInput, &secondaryOutput, &setpoint, Kp2, Ki2, Kd2, DIRECT);

HubConfiguration* DewController::hubConfiguration;

void DewController::init(class HubConfiguration* configuration) {
    hubConfiguration = configuration;

    primaryPid.SetOutputLimits(0.0, 1.0);
    secondaryPid.SetOutputLimits(0.0, 1.0);

    primaryPid.SetMode(AUTOMATIC);
    secondaryPid.SetMode(AUTOMATIC);
}

void DewController::loop() {
    primaryInput = TemperatureSensors::getCurrentTemperatureValues()[TEMP_SENSOR_PRIMARY] + TEMP_SENSOR_PRIMARY_OFFSET;
    secondaryInput = TemperatureSensors::getCurrentTemperatureValues()[TEMP_SENSOR_SECONDARY] + TEMP_SENSOR_SECONDARY_OFFSET;
    setpoint = Weather::getDewPoint() + DEWPOINT_OFFSET;

    FanControl::setFanSpeed(primaryInput > setpoint ? FAN_COOLDOWN : FAN_IDLE);

    primaryPid.Compute();
    secondaryPid.Compute();

    hubConfiguration->getState().pwmPercentages[POWER_OUTPUT_PRIMARY] = primaryOutput;
    hubConfiguration->getState().pwmPercentages[POWER_OUTPUT_SECONDARY] = secondaryOutput;
}
