import sys
import TeensyRawhid

KOMAHUB_VID = 0x1209
KOMAHUB_PID = 0x4242

def usage():
    print 'usage: factoryreset.py <serialnumber> <r6ohms> <r7ohms>'
    print '   serialnumber - serial number of the device'
    print '   r6ohms - measured value of the r6 resistor (ohms)'
    print '   r7ohms - measured value of the r7 resistor (ohms)'

def cmdlineargs(args):
    if len(args) != 4:
        usage()
        sys.exit(1)

    return (int(sys.argv[1]), int(sys.argv[2]), int(sys.argv[3]))

def inttobytes(n):
    return (n&0xFF, (n&0xFF00)>>8)

def factoryreset(serial, r6ohms, r7ohms):
    rh = TeensyRawhid.Rawhid()
    buffer = [ord('K'), 0]
    buffer.append(0xFA)
    for b in inttobytes(serial): buffer.append(b)
    for b in inttobytes(r6ohms): buffer.append(b)
    for b in inttobytes(r7ohms): buffer.append(b)
    buffer[1] = len(buffer)-2
    while len(buffer) < 64:
        buffer.append(0)
    print buffer
    rh.open(vid=KOMAHUB_VID, pid=KOMAHUB_PID)
    rh.send("".join(map(lambda x:chr(x), buffer)), 1000)
    rh.close()

if __name__ == '__main__':
    (serial, r6ohms, r7ohms) = cmdlineargs(sys.argv)
    factoryreset(serial, r6ohms, r7ohms)
