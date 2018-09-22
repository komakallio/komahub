import sys
import TeensyRawhid

KOMAHUB_VID = 0x1209
KOMAHUB_PID = 0x4242

def usage():
    print 'usage: status.py'

def cmdlineargs(args):
    if len(args) < 3:
        usage()
        sys.exit(1)
    return (int(sys.argv[1]), map(int, sys.argv[2:]))

def inttobytes(n):
    return ((n&0xFF000000)>>24, (n&0xFF0000)>>16, (n&0xFF00)>>8, n&0xFF)

def query(cmd):
    rh = TeensyRawhid.Rawhid()
    buffer = [ord('K'), 1, cmd]
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
    return data

def factorysettings():
    data = query(0x02)
    if data and len(data) > 0:
        return {
            'firmwareMajor' : ord(data[1]),
            'firmwareMinor' : ord(data[0]),
#            'numberOfOutputs' : ord(data[2]),
            'serial' : ord(data[3]) * 256 + ord(data[2]),
            'boardRevision' : (ord(data[7]) >> 4) + (ord(data[7]) & 0x3)/10.0
#            'relayIsOpen' : ord(data[5]),
#            'fuseIsBlown' : ord(data[6]),
#            'relayIsPwm' : ord(data[7]),
#            'pwmIsFast' : ord(data[8]),
#            'pwmPercentages' : [ ord(data[9]), ord(data[10]), ord(data[11]), ord(data[12]), ord(data[13]), ord(data[14]) ],
#            'inputVoltage' : float(ord(data[15])/10.0),
#            'outputCurrents' : [ float(ord(data[16])/10.0), float(ord(data[17])/10.0), float(ord(data[18])/10.0), float(ord(data[19])/10.0), float(ord(data[20])/10.0), float(ord(data[21])/10.0) ],
         }
    else:
        raise IOError("No data received")

def toint16(a, b):
    value = ord(a) + ord(b)*256
    if value >= 32768:
        value = -(65536-value)
    return value/10.0

def status():
    data = query(0x04)
    if data and len(data) > 0:
        return {
            'relayIsOpen' : ord(data[0]),
            'fuseIsBlown' : ord(data[1]),
            'pwmPercentages' : [ ord(data[2]), ord(data[3]), ord(data[4]), ord(data[5]), ord(data[6]), ord(data[7]) ],
            'inputVoltage' : float(ord(data[8])/10.0),
            'outputCurrents' : [ float(ord(data[9])/10.0), float(ord(data[10])/10.0), float(ord(data[11])/10.0), float(ord(data[12])/10.0), float(ord(data[13])/10.0), float(ord(data[14])/10.0) ],
            'numTemperatureSensors' : ord(data[15]),
            'temperatures' : [ toint16(data[16], data[17]), toint16(data[18], data[19]), toint16(data[20], data[21]), toint16(data[22], data[23]) ],
            'temperature' : toint16(data[24], data[25]),
            'dewpoint' : toint16(data[26], data[27]),
            'humidity' : ord(data[28]),
            'pressure' : toint16(data[29], data[30]),
            'skyquality' : ord(data[31])
         }
    else:
        raise IOError("No data received")

if __name__ == '__main__':
    settings = factorysettings()
    data = status()
    print 'KomaHub v%s, firmware v%s.%s, serial #%05d' % (settings['boardRevision'], settings['firmwareMajor'], settings['firmwareMinor'], settings['serial'])
    print 'Input Voltage: %1.1fV' % data['inputVoltage']
    for i in range(0, 6):
        print 'Output %d: %s%s%s %1.1fA' % (i+1,
            ('ACTIVE' if (data['relayIsOpen'] & (1 << i)) != 0 else '______'),
            (' FUSE' if (data['fuseIsBlown'] & (1 << i)) != 0 else ''),
            (' %d%%' % data['pwmPercentages'][i]),
            data['outputCurrents'][i])
    print 'Temperature: %1.1f C' % (data['temperature'])
    print 'Dewpoint: %1.1f C' % (data['dewpoint'])
    print 'Humidity: %1.1f%%' % data['humidity']
    print 'Pressure: %1.1f hPa' % (data['humidity'])
    print 'External temperature sensors: %d' % data['numTemperatureSensors']
    for i in range(0, data['numTemperatureSensors']):
        print 'Temperature %d: %1.1f C' % ((i+1), data['temperatures'][i])
    print 'Sky quality: %1.1f mag/arcsec^2' % (data['skyquality'])
