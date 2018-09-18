import serial, sys, TeensyRawhid, time, numpy as np
from scipy.optimize import curve_fit

class OutputMeasurementError(BaseException):
    pass

class CalibrationError(BaseException):
    pass

class HardwareError(BaseException):
    pass

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

    def close(self):
        self.komahub.close()

    def send(self, buffer):
        while len(buffer) < 64:
            buffer.append(0)
        self.komahub.send("".join(map(lambda x:chr(x), buffer)), 1000)

    def recv(self):
        data = self.komahub.recv(64, 500)
        return map(lambda x:ord(x), data)

    def configureRelay(self, output):
        self.send([ord('K'), 19, output, 1, 50, ord('O'), ord('u'), ord('t'), ord('p'), ord('u'), ord('t') , ord(' '), ord('0') + output+1]) # configure output to defaults
        self.send([ord('K'), 18, output]) # reset fuse
        self.send([ord('K'), 17, output, 100]) # set pwm to 100

    def setRelay(self, output, enabled):
        self.send([ord('K'), 16, output, 1 if enabled else 0])

    def measureCurrent(self, output):
        self.send([ord('K'), 5])
        data = self.recv()
        return data[((output+1)*2+1)] * 256 + data[(output+1)*2]

    def _signedIntToByte(self, value):
        if (value < 0):
            return 256+value
        else:
            return value

    def storeOutputCalibration(self, output, coeffs):
        self.send([ord('K'), 0xFC, output, self._signedIntToByte(coeffs[0]), coeffs[1], coeffs[2]])

def usage():
    print 'usage: powercalibration.py <output>'
    print '   output - output to calibrate (1 to 6)'

def testoutput(komahub, loadGenerator, output):
    try:
        loadGenerator.setLoad(1000)
        komahub.setRelay(output, True)
        time.sleep(1)
        current = komahub.measureCurrent(output)
        if current == 0:
            raise OutputMeasurementError('1000mA current could not be detected!')

        komahub.setRelay(output, False)
        time.sleep(1)
        current = komahub.measureCurrent(output)
        if current != 0:
            raise HardwareError('Output did not turn off (current=%d)!' % current)
    finally:
        loadGenerator.setLoad(0)

def measure(komahub, loadGenerator, output):
    adc = []
    amps = []

    try:
        loadGenerator.setLoad(0)
        komahub.setRelay(output, True)

        load = 0
        while load <= 4500:
            loadGenerator.setLoad(load)
            time.sleep(1)
            current = komahub.measureCurrent(output)
            amps.append(load)
            adc.append(current)

            if current == 0 and load >= 400:
                raise OutputMeasurementError("Output measurement current not detected. Are you measuring the correct output?")

            load += 10 if load < 700 else 100
            print load, current
#            sys.stdout.write('.')
#            sys.stdout.flush()
        komahub.setRelay(output, False)
    finally:
        print
        loadGenerator.setLoad(0)
    return (adc, amps)

def fit(adc, amps):
    p = np.polyfit(adc, amps, 2)
    coeffs = [ int(p[0]*1000), int(p[1]), int(p[2]) ]
    print 'coefficients:', coeffs
    if coeffs[0] < -128 or coeffs[0] > 127 or coeffs[1] < 0 or coeffs[1] > 255 or coeffs[2] < 0 or coeffs[2] > 255:
        raise CalibrationError("Calibration coefficient(s) out of range (-128..127, 0..255, 0..255)")
    return coeffs

if __name__ == '__main__':
    if len(sys.argv) != 2:
        usage()
        sys.exit(1)

    komahub = KomaHub()
    output = int(sys.argv[1])-1     # output number is internally 0..5
    loadgenerator = LoadGenerator('/dev/cu.usbserial-DA71J7S')
    komahub.configureRelay(output)
#    testoutput(komahub, loadgenerator, output)
    (adc, amps) = measure(komahub, loadgenerator, output)
    komahub.storeOutputCalibration(output, fit(adc, amps))
