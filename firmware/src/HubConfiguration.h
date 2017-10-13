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

enum OutputType {
    OFF = 0,
    DC = 1,
    PWM = 2,
    PWM_PID_HEAT = 3,
    PWM_PID_COOL = 4,
    PWM_FAST = 5
};

class HubConfiguration {
public:
    struct FactoryConfig {
        char komahub[8];
        uint16_t serial;
        float r6r7divisor;
        uint8_t fuseDelay;
        uint8_t skyQualityOffset;
        
        struct Features {
            uint8_t tempprobes:1;
            uint8_t skyquality:1;
            uint8_t ambientpth:1;
            uint8_t skytemp:1;
            uint8_t reserved:4;
        } features;
        
        uint8_t boardRevision;
        // max 32 bytes
    } __attribute__((__packed__));

    struct Output {
        char name[16];
        uint8_t fuseCurrent;
        struct Type {
            uint8_t type:4;
            uint8_t pidSensor:2;
        } type;
        struct Coefficients {
            int8_t a;
            uint8_t b;
            uint8_t c;
        } coeffs;
        // 21 bytes * 6 = 126 bytes
    } __attribute__((__packed__));

    struct OutputSettings {
        Output outputs[6];
        // max 128 bytes
    } __attribute__((__packed__));

    struct State {
        uint8_t counter;
        uint8_t relayIsOpenBits;
        uint8_t fuseIsBlownBits;
        uint8_t pwmPercentages[6];
        // 9 bytes
    } __attribute__((__packed__));

public:
    HubConfiguration();
    void loadStoredConfiguration();
    void saveState();
    void saveOutputConfiguration();
    void saveFactoryConfig();
    void factoryReset(int serialNumber, int r6ohms, int r7ohms);

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
