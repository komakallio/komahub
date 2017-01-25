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
#include <EEPROM.h>
#include "HubConfiguration.h"

#define EEPROM_SIZE 1024

#define FACTORYCONFIG_OFFSET 0
#define FACTORYCONFIG_RESERVED_SIZE 32
#define OUTPUTSETTINGS_OFFSET (FACTORYCONFIG_OFFSET + FACTORYCONFIG_RESERVED_SIZE)
#define OUTPUTSETTINGS_RESERVED_SIZE (16*8)
#define STATE_OFFSET (OUTPUTSETTINGS_OFFSET + OUTPUTSETTINGS_RESERVED_SIZE)
#define MAX_STATE_INDEX ((EEPROM_SIZE-FACTORYCONFIG_RESERVED_SIZE-OUTPUTSETTINGS_RESERVED_SIZE)/sizeof(State))

static void eepromcpyto(int location, const void *src, int size) {
    const uint8_t *srcPtr = (const uint8_t*)src;
    for (int loc = location; loc < location+size; loc++) {
        if (EEPROM.read(loc) != *srcPtr) {
            EEPROM.write(loc, *srcPtr);
        }
        srcPtr++;
    }
}

static void eepromcpyfrom(void *dst, int location, int size) {
    uint8_t *dstPtr = (uint8_t*)dst;
    for (int loc = location; loc < location+size; loc++) {
        *dstPtr++ = EEPROM.read(loc);
    }
}

HubConfiguration::HubConfiguration()
{
}

void HubConfiguration::loadStoredConfiguration() {
    memset(&this->factoryConfig, 0, sizeof(FactoryConfig));
    memset(&this->outputSettings, 0, sizeof(OutputSettings));
    memset(&this->state, 0, sizeof(State));

    eepromcpyfrom(&this->factoryConfig, FACTORYCONFIG_OFFSET, sizeof(FactoryConfig));
    if (memcmp(&this->factoryConfig.komahub[0], "KOMAHUB\0", 8) != 0) {
        pinMode(11, OUTPUT);
        digitalWrite(11, HIGH);
        delay(1000);
        digitalWrite(11, LOW);     
        resetConfiguration();
        return;
    }

    eepromcpyfrom(&this->outputSettings, OUTPUTSETTINGS_OFFSET, sizeof(OutputSettings));

    this->stateIndex = findLatestStateIndex();
    eepromcpyfrom(&this->state, STATE_OFFSET + sizeof(State)*this->stateIndex, sizeof(State)); 
}

int HubConfiguration::findLatestStateIndex() {
    int max=0, index=0;
    int maxBelow100=0, indexBelow100=0;
    for (unsigned int i = 0; i < MAX_STATE_INDEX; i++) {
        int counter = EEPROM.read(STATE_OFFSET + sizeof(State)*i);
        if (counter > max) {
            max = counter;
            index = i;
        }
        if (counter > maxBelow100 && counter < 100) {
            maxBelow100 = counter;
            indexBelow100 = i;
        }
    }

    if (max == 255) {
        return indexBelow100;
    } else {
        return index;
    }
}

void HubConfiguration::factoryReset(int serialNumber, int numberOfOutputs, int r6ohms, int r7ohms, uint8_t features, uint8_t sqmZeroPoint, uint8_t fuseSpeed) {
    resetConfiguration();
    this->factoryConfig.serial = serialNumber;
    this->factoryConfig.numberOfOutputs = numberOfOutputs;
    this->factoryConfig.r6r7divisor = r6ohms / (float)r7ohms;
    this->factoryConfig.features.onewire = (features & 0x01) != 0;
    this->factoryConfig.features.sqm = (features & 0x02) != 0;
    this->factoryConfig.features.bme280 = (features & 0x04) != 0;
    this->factoryConfig.sqmZeroPoint = sqmZeroPoint;
    this->factoryConfig.fuseSpeed = fuseSpeed;
    initEEPROM(this->factoryConfig, this->outputSettings, this->state);
}

void HubConfiguration::resetConfiguration() {
    memset(&this->factoryConfig, 0, sizeof(FactoryConfig));
    memcpy(&this->factoryConfig.komahub[0], "KOMAHUB\0", 8);
    this->factoryConfig.serial = 0;
    this->factoryConfig.numberOfOutputs = 6;
    this->factoryConfig.r6r7divisor = 6;
    this->factoryConfig.fuseSpeed = 10;

    memset(&this->outputSettings, 0, sizeof(OutputSettings));
    for (int i = 0; i < 7; i++) {
        strncpy(this->outputSettings.outputs[i].name, (String("Output ") + String(i+1)).c_str(), 16);
        this->outputSettings.outputs[i].fuseCurrent = 50; // 5 amps
    }

    memset(&this->state, 0, sizeof(State));
    this->state.counter = 1;
}

void HubConfiguration::initEEPROM(const FactoryConfig& factoryConfig, const OutputSettings& outputSettings, const State& state) {
    for (int i = 0; i < EEPROM_SIZE; i++) {
        EEPROM.write(i, 0);
    }
    eepromcpyto(FACTORYCONFIG_OFFSET, &factoryConfig, sizeof(FactoryConfig));
    eepromcpyto(OUTPUTSETTINGS_OFFSET, &outputSettings, sizeof(OutputSettings));
    eepromcpyto(STATE_OFFSET, &state, sizeof(State));
}

HubConfiguration::FactoryConfig& HubConfiguration::getFactoryConfig() {
    return this->factoryConfig;
}

HubConfiguration::OutputSettings& HubConfiguration::getOutputSettings() {
    return this->outputSettings;
}

HubConfiguration::State& HubConfiguration::getState() {
    return this->state;
}

void HubConfiguration::saveState() {
    this->state.counter++;
    this->stateIndex++;
    if (this->stateIndex > MAX_STATE_INDEX) {
        this->stateIndex = 0;
    }
    eepromcpyto(STATE_OFFSET + sizeof(State)*this->stateIndex, &this->state, sizeof(State)); 
}

void HubConfiguration::saveOutputConfiguration() {
    eepromcpyto(OUTPUTSETTINGS_OFFSET, &outputSettings, sizeof(OutputSettings));
}
