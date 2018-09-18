import serial, sys, TeensyRawhid, time

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
        print 'send', buffer
        while len(buffer) < 64:
            buffer.append(0)
        self.komahub.send("".join(map(lambda x:chr(x), buffer)), 1000)

    def recv(self):
        data = self.komahub.recv(64, 500)
        return map(lambda x:ord(x), data)

    def setRelay(self, output, enabled):
        self.send([ord('K'), 16, output, 1 if enabled else 0])

    def measureCurrent(self, output):
        self.send([ord('K'), 5])
        data = self.recv()
        return data[((output+1)*2+1)] * 256 + data[(output+1)*2]

def usage():
    print 'usage: selftest.py <output>'
    print '   output - output to test (1 to 6)'

def testoutput(komahub, loadGenerator, output):
    try:
        komahub.setRelay(output, True)
        loadGenerator.setLoad(1000)
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

if __name__ == '__main__':
    if len(sys.argv) != 2:
        usage()
        sys.exit(1)

    komahub = KomaHub()
    output = int(sys.argv[1])-1     # output number is internally 0..5
    loadgenerator = LoadGenerator('/dev/cu.usbserial-DA71J7S')
    testoutput(komahub, loadgenerator, output)
    print 'Output %d ok' % (output+1)
