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

#include "VersionSpecifics.h"

int VersionSpecifics::s_boardRevision = 0x10;

void VersionSpecifics::setBoardRevision(int revision) {
    s_boardRevision = revision;
}

float VersionSpecifics::getPowerOutputACoefficientDivisor() {
    switch (s_boardRevision) {
        case 0x11:
            return 500.0f;
        default:
            return 1000.0f;
    }
}

int8_t VersionSpecifics::getDefaultPowerOutputCoefficientA() {
    switch (s_boardRevision) {
        case 0x11:
            return -93;
        default:
            return -18;
    }
}

uint8_t VersionSpecifics::getDefaultPowerOutputCoefficientB() {
    switch (s_boardRevision) {
        case 0x11:
            return 69;
        default:
            return 23;
    }
}

uint8_t VersionSpecifics::getDefaultPowerOutputCoefficientC() {
    switch (s_boardRevision) {
        case 0x11:
            return 191;
        default:
            return 95;
    }
}
