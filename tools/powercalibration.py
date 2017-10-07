import serial, sys, TeensyRawhid, time, numpy as np
from pylab import *
from scipy.optimize import curve_fit

class LoadGenerator:
    def __init__(self, device):
        self.serial = serial.Serial(device, 115200)

    def setLoad(self, milliamps):
        self.serial.write('set %d\n' % milliamps)
        self.serial.readline()

class KomaHub:
    KOMAHUB_VID = 0x1209
    KOMAHUB_PID = 0x4242

    def __init__(self):
        self.komahub = TeensyRawhid.Rawhid()
        self.komahub.open(vid=self.KOMAHUB_VID, pid=self.KOMAHUB_PID)

    def measureCurrent(self, output):
        buffer = [ord('K'), 5]
        while len(buffer) < 64:
            buffer.append(0)
        self.komahub.send("".join(map(lambda x:chr(x), buffer)), 1000)
        data = self.komahub.recv(64, 500)
        return ord(data[((output)*2+1)]) * 256 + ord(data[(output)*2])


def usage():
    print 'usage: powercalibration.py <output>'
    print '   output - output to calibrate (1 to 6)'

def calibrate(komahub, loadGenerator, output):
    amps = []
    values = []
    for milliamps in range(0, 400, 25):
        loadGenerator.setLoad(milliamps)
        time.sleep(1)
        adcvalue = komahub.measureCurrent(output)
        if adcvalue > 0:
            print 'mA: %d adc: %d, k: %d' % (milliamps, adcvalue, int((milliamps/1000.0/adcvalue)*500000))
            values.append(milliamps)
        else:
            values.append(0)
        amps.append(adcvalue)

    for milliamps in range(400, 4500, 100):
        loadGenerator.setLoad(milliamps)
        time.sleep(1)
        adcvalue = komahub.measureCurrent(output)
        if adcvalue > 0:
            print 'mA: %d adc: %d, k: %d' % (milliamps, adcvalue, int((milliamps/1000.0/adcvalue)*500000))
            values.append(milliamps)
        else:
            values.append(0)
        amps.append(adcvalue)

    loadGenerator.setLoad(0)
    return (amps, values)

if __name__ == '__main__':
    if len(sys.argv) != 2:
        usage()
        sys.exit(1)

    (amps, values) = calibrate(KomaHub(), LoadGenerator('/dev/cu.usbserial-DA71J7S'), int(sys.argv[1]))
