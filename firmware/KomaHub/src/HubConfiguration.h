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

#ifndef HUBCONFIGURATION_H
#define HUBCONFIGURATION_H

class HubConfiguration {
public:
    struct FactoryConfig {
        char komahub[8];
        uint16_t serial;
        uint8_t numberOfOutputs;
        float r6r7divisor;
        uint8_t fuseSpeed;
        uint8_t sqmZeroPoint;
        
        struct Features {
            uint8_t onewire:1;
            uint8_t sqm:1;
            uint8_t bme280:1;
            uint8_t reserved:5;
        } features;
        uint8_t reserved[12];
        // 32 bytes
    };

    struct Output {
        char name[15];
        uint8_t fuseCurrent;
        // 16 bytes
    };

    struct OutputSettings {
        Output outputs[6];
        char reserved[16];
        // 128 bytes
    };

    struct State {
        uint8_t counter;
        uint8_t relayIsPwmBits;
        uint8_t pwmIsFastBits;
        uint8_t relayIsOpenBits;
        uint8_t fuseIsBlownBits;
        uint8_t pwmPercentages[6];
        // 11 bytes
    };

public:
    HubConfiguration();
    void loadStoredConfiguration();
    void saveState();
    void saveOutputConfiguration();
    void factoryReset(int serialNumber, int numberOfOutputs, int r6ohms, int r7ohms, uint8_t features, uint8_t sqmZeroPoint, uint8_t fuseSpeed);

    HubConfiguration::FactoryConfig& getFactoryConfig();
    HubConfiguration::OutputSettings& getOutputSettings();
    HubConfiguration::State& getState();

private:
    void initEEPROM(const FactoryConfig& factoryConfig, const OutputSettings& outputSettings, const State& state);
    void resetConfiguration();
    int findLatestStateIndex();

private:
    FactoryConfig factoryConfig;
    OutputSettings outputSettings;
    State state;
    uint8_t stateIndex;
};


#endif