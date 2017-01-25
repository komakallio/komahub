import sys
import TeensyRawhid

KOMAHUB_VID = 0x16C0
KOMAHUB_PID = 0x0470

def usage():
    print 'usage: factoryreset.py <serialnumber> <r6ohms> <r7ohms> <features> <sqmzeropoint> <fusespeed>'
    print '   serialnumber - serial number of the device'
    print '   r6ohms - measured value of the r6 resistor (ohms)'
    print '   r7ohms - measured value of the r7 resistor (ohms)'
    print '   features - uint8 bitmask (:::::bme280:sqm:onewire); 255 = all enabled'
    print '   sqmzeropoint - the "A" constant for the SQM formula (e.g. 22.0)'
    print '   fusespeed - minimum time for overload condition until the fuse is tripped (milliseconds)'

def cmdlineargs(args):
    if len(args) != 7:
        usage()
        sys.exit(1)

    return (int(sys.argv[1]), int(sys.argv[2]), int(sys.argv[3]), int(sys.argv[4]), int(float(sys.argv[5])*10), int(sys.argv[6]))

def inttobytes(n):
    return ((n&0xFF00)>>8, n&0xFF)

def factoryreset(serial, outputs, r6ohms, r7ohms, features, sqmzero, fusespeed):
    rh = TeensyRawhid.Rawhid()
    buffer = [ord('K'), 0]
    buffer.append(0xFA)
    for b in inttobytes(serial): buffer.append(b)
    buffer.append(outputs)
    for b in inttobytes(r6ohms): buffer.append(b)
    for b in inttobytes(r7ohms): buffer.append(b)
    buffer.append(features)
    buffer.append(sqmzero)
    buffer.append(fusespeed)
    buffer[1] = len(buffer)-2
    while len(buffer) < 64:
        buffer.append(0)
    print buffer
    rh.open(vid=KOMAHUB_VID, pid=KOMAHUB_PID)
    rh.send("".join(map(lambda x:chr(x), buffer)), 1000)
    rh.close()

if __name__ == '__main__':
    (serial, r6ohms, r7ohms, features, sqmzero, fusespeed) = cmdlineargs(sys.argv)
    factoryreset(serial, 6, r6ohms, r7ohms, features, sqmzero, fusespeed)
