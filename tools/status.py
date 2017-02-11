import sys
import TeensyRawhid

KOMAHUB_VID = 0x16C0
KOMAHUB_PID = 0x0470

def usage():
    print 'usage: status.py'

def cmdlineargs(args):
    if len(args) < 3:
        usage()
        sys.exit(1)
    return (int(sys.argv[1]), map(int, sys.argv[2:]))

def inttobytes(n):
    return ((n&0xFF000000)>>24, (n&0xFF0000)>>16, (n&0xFF00)>>8, n&0xFF)

def status():
    cmd = 2
    rh = TeensyRawhid.Rawhid()
    buffer = [ord('K'), 1, 2]
    while len(buffer) < 64:
        buffer.append(0)
    rh.open(vid=KOMAHUB_VID, pid=KOMAHUB_PID)
    rh.send("".join(map(lambda x:chr(x), buffer)), 1000)
    data = None
    try:
    	data = rh.recv(64, 500)
    except IOError:
	    pass
    rh.close()
    if data and len(data) > 0:
        return { 
            'firmwareMajor' : ord(data[0]),
            'firmwareMinor' : ord(data[1]),
            'numberOfOutputs' : ord(data[2]),
            'serial' : ord(data[3]) << 8 + ord(data[4]),
            'relayIsOpen' : ord(data[5]),
            'fuseIsBlown' : ord(data[6]),
            'relayIsPwm' : ord(data[7]),
            'pwmIsFast' : ord(data[8]),
            'pwmPercentages' : [ ord(data[9]), ord(data[10]), ord(data[11]), ord(data[12]), ord(data[13]), ord(data[14]) ],
            'inputVoltage' : float(ord(data[15])/10.0),
            'outputCurrents' : [ float(ord(data[16])/10.0), float(ord(data[17])/10.0), float(ord(data[18])/10.0), float(ord(data[19])/10.0), float(ord(data[20])/10.0), float(ord(data[21])/10.0) ], 
         }

if __name__ == '__main__':
    data = status()
    keys = data.keys()
    keys.sort()
    print 'KomaHub v%s.%s, serial #%05d' % (data['firmwareMajor'], data['firmwareMinor'], data['serial'])
    print 'Input Voltage: %1.1fV' % data['inputVoltage']
    for i in range(0, data['numberOfOutputs']):
        print 'Output %d: %s%s%s%s%s %1.1fA' % (i+1, 
            ('ACTIVE' if (data['relayIsOpen'] & (1 << i)) != 0 else '______'), 
            (' FUSE' if (data['fuseIsBlown'] & (1 << i)) != 0 else ''), 
            (' PWM' if (data['relayIsPwm'] & (1 << i)) != 0 else ''), 
            ('FAST' if (data['pwmIsFast'] & (1 << i)) != 0 else ''), 
            (' %d%%' % data['pwmPercentages'][i] if (data['relayIsPwm'] & (1 << i)) != 0 else ''), 
            data['outputCurrents'][i])
