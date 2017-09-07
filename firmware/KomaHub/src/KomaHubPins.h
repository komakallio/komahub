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

#ifndef KOMAHUBPINS_H
#define KOMAHUBPINS_H

#include <Arduino.h>

namespace KomaHub {

enum Pin {
    SS = 0, 
    SCLK = 1, 
    MOSI = 2, 
    MISO = 3,
    SCL = 5, 
    SDA = 6,
    VSENSE = A1,
    SENSE1 = A2, 
    SENSE2 = A3, 
    SENSE3 = A4, 
    SENSE4 = A5, 
    SENSE5 = A6, 
    SENSE6 = A7,
    P1 = 4, 
    P2 = 9, 
    P3 = 21, 
    P4 = 7, 
    P5 = 8, 
    P6 = 24,
    AUX1 = 9,
    AUX1_2 = 11,
    AUX2 = 13,
    LED = 12
};

}

#endif