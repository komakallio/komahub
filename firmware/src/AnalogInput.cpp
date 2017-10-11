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
#include <wiring_private.h>
#include "AnalogInput.h"
#include "HubConfiguration.h"
#include "KomaHubPins.h"

extern uint8_t w_analog_reference;

static volatile uint8_t pin;
static volatile uint32_t sums[8];
static volatile uint32_t counts[8];
static uint16_t averages[8];

static const uint8_t PROGMEM adc_mapping[] = {
   0, 1, 4, 5, 6, 7, 13, 12, 11, 10, 9, 8, 10, 11, 12, 13, 7, 6, 5, 4, 1, 0, 8 
};

static void adc_start_measure(int pin) {
    int adc = pgm_read_byte(adc_mapping + pin);

    // Select ADC channel
    if (adc < 8) {
        ADMUX = w_analog_reference | (adc & 0x1F);
        ADCSRB = (1<<ADHSM);
    } else {
        ADMUX = w_analog_reference | ((adc-8) & 0x1F);
        ADCSRB = (1<<ADHSM) | (1 << MUX5);
    }

    // Enable ADC interrupt but no free-running mode; we'll trigger a new ADC operation
    // in the ISR
    ADCSRA = (1<<ADSC) | (1<<ADEN) | (1<<ADIE) | ADC_PRESCALER;
}

void AnalogInput::init(HubConfiguration* hubConfiguration) {
    analogReference(EXTERNAL);

    // Disable unused analog inputs
    DIDR0 = 0xF2; // F1, F4, F5, F6, F7
    DIDR2 = 0x30; // B5, B6

    pinMode(KomaHub::VSENSE, INPUT);
    pinMode(KomaHub::SENSE1, INPUT);
    pinMode(KomaHub::SENSE2, INPUT);
    pinMode(KomaHub::SENSE3, INPUT);
    pinMode(KomaHub::SENSE4, INPUT);
    pinMode(KomaHub::SENSE5, INPUT);
    pinMode(KomaHub::SENSE6, INPUT);

    memset((uint32_t*)sums, 0, 8*sizeof(uint32_t));
    memset((uint32_t*)counts, 0, 8*sizeof(uint32_t));
    memset((uint16_t*)averages, 0, 8*sizeof(uint16_t));
    pin = 1;

    cli();
    adc_start_measure(pin);
   	sei(); 
}

ISR(ADC_vect) {
    uint16_t val = ADC;

    sums[pin-1] += val;
    counts[pin-1]++;

    pin++;
    if (pin == 8) {
        // we read pins 1..8, reset the counter
        pin = 1;
    }
    adc_start_measure(pin);
}

void AnalogInput::resetAverageCollectingPeriod() {
    cli();
    for (int i = 0; i < 8; i++) {
        averages[i] = (uint16_t)(sums[i]/counts[i]);
        sums[i] = counts[i] = 0;
    }
    sei();
}

const uint16_t* AnalogInput::getAverageValues() {
    return averages;
}

void AnalogInput::loop() {
}
