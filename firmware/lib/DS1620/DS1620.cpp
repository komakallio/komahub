#include <Arduino.h>
#include "DS1620.h"

DS1620::DS1620(int dq, int clk, int rst) :
    _dq(dq), _clk(clk), _rst(rst) {
    pinMode(_dq, INPUT);
    pinMode(_clk, OUTPUT);
    pinMode(_rst, OUTPUT);
    digitalWrite(_clk, 1);
    digitalWrite(_rst, 0);
}

float DS1620::getTemperature() {
    float temp = 0.0;
    uint16_t rawTemp = readTemperatureRaw();
    temp = rawTemp / 2.0f;
    if (rawTemp >= 0x100) {
        temp -= 256.0f;
    }
    return temp;
}

uint16_t DS1620::readTemperatureRaw() {
    uint16_t temp = 0;
    uint8_t tempLSB, tempMSB = 0;
    
    digitalWrite(_rst, 1);
    shiftOut(READ_TEMPERATURE);
    tempLSB = shiftIn();
    tempMSB = shiftIn();
    digitalWrite(_rst, 0);
    temp = tempMSB * 256;
    temp += tempLSB;
    return temp;
}

void DS1620::startConversion() {
    digitalWrite(_rst, 1);
    shiftOut(START_CONVERT);
    digitalWrite(_rst, 0);
}

uint8_t DS1620::shiftIn() {
    uint8_t temp = 0;
    uint8_t data = 0;
    pinMode(_dq, INPUT);
    for (int i = 0; i < 8; i++) {
        digitalWrite(_clk, 0);
        delayMicroseconds(CLOCK_DELAY);
        temp = digitalRead(_dq);
        data = data >> 1;
        data = data | (temp << 7);
        digitalWrite(_clk, 1);
        delayMicroseconds(CLOCK_DELAY);
    }
    return data;   
}

void DS1620::shiftOut(uint8_t data) {
    pinMode(_dq, OUTPUT);
    for (int i = 0; i < 8; i++) {
        digitalWrite(_clk, 0);
        digitalWrite(_dq, data & 1);
        delayMicroseconds(CLOCK_DELAY);
        digitalWrite(_clk, 1);
        delayMicroseconds(CLOCK_DELAY);
        data = data >> 1;
    }
    pinMode(_dq, INPUT);
}