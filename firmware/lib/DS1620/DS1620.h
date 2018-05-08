#ifndef DS1620_H
#define DS1620_H

#define CLOCK_DELAY 1
#define READ_TEMPERATURE 0xAA
#define START_CONVERT    0xEE

class DS1620 {
public:
    DS1620(int dq, int clk, int rst);

    float getTemperature();
    void startConversion();
    uint16_t readTemperatureRaw();

private:
    void shiftOut(uint8_t data);
    uint8_t shiftIn();
        
    int _dq;
    int _clk;
    int _rst;
};

#endif