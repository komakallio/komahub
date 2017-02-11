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

#define RINGBUFFER_SIZE 64

extern uint8_t w_analog_reference;

static volatile uint16_t ringbuffer[RINGBUFFER_SIZE*8];
static volatile uint16_t ringbufferHead;
static volatile uint8_t pin;

static const uint8_t PROGMEM adc_mapping[] = {
   0, 1, 4, 5, 6, 7, 13, 12, 11, 10, 9, 8, 10, 11, 12, 13, 7, 6, 5, 4, 1, 0, 8 
};

uint16_t AnalogInput::averages[8];

static void adc_start_measure(int pin) {
    int mux = pgm_read_byte(adc_mapping + pin);

	ADCSRA = (1<<ADEN) | ADC_PRESCALER;	// enable the ADC, interrupt disabled
	ADCSRB = (1<<ADHSM) | (mux & 0x20);
	ADMUX = w_analog_reference | (mux & 0x1F);		// configure mux and ref
	ADCSRA = (1<<ADSC) | (1<<ADEN) | (1<<ADATE) | (1<<ADIE) | ADC_PRESCALER;
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

    memset(averages, 0, 8*sizeof(uint16_t));
    ringbufferHead = 0;
    pin = 0;

/*
    cli();
    adc_start_measure(pin);
   	sei(); */
}
/*
ISR(ADC_vect) {
	uint16_t h = ringbufferHead;
	int16_t val = ADC;

    h = (h+1) & 0x1FF;
    
    ringbuffer[h] = val;	// put new data into buffer
    ringbufferHead = h;

    pin++;
    pin &= 0x7; // loop through 8 pins
    adc_start_measure(pin);
}
*/
void AnalogInput::getAverageValues(uint16_t* arr) {
    memcpy(arr, &averages[0], 8*sizeof(uint16_t));
}

void AnalogInput::loop() {
    memset(&averages[0], 0, 8*sizeof(uint16_t));
    averages[0] = analogRead(KomaHub::VSENSE);
    averages[1] = analogRead(KomaHub::SENSE1);
    averages[2] = analogRead(KomaHub::SENSE2);
    averages[3] = analogRead(KomaHub::SENSE3);
    averages[4] = analogRead(KomaHub::SENSE4);
    averages[5] = analogRead(KomaHub::SENSE5);
    averages[6] = analogRead(KomaHub::SENSE6);
    
/*
    // rewind 32 measurement rounds
    int pos = (ringbufferHead - 32*8) & 0x1F8;

    for (int i = 0; i < 32; i++) {
        volatile uint16_t *src = &ringbuffer[pos];
        for (int j = 0; j < 8; j++) {
            averages[j] += *src++;
        }
        pos += 8;
        pos &= 0x1F8;
    }
    for (int i = 0; i < 8; i++)  {
        // normalize to 0..1023, round correctly
        averages[i] = (averages[i] >> 5) + ((averages[i] & 0x10) >> 4);
    }*/
}