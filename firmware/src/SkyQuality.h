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

#ifndef SkyQuality_H
#define SkyQuality_H

class SkyQuality {
public:
    static void init(class HubConfiguration* hubConfiguration);
    static void loop();
    static void stop();

    static float getFrequencyHz();
    static float getSkyQuality();
    static bool isSensorPresent();

private:
    static void switchToCount();
    static void switchToMeasure();

private:
    static enum Mode {
        COUNT,
        MEASURE
    } mode;

    static float freq;
    static bool sensorPresent;
    static class HubConfiguration* hubConfiguration;
};

#endif