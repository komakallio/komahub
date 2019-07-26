import sys
import TeensyRawhid

KOMAHUB_VID = 0x1209
KOMAHUB_PID = 0x4242

def usage():
    print 'usage: updatesettings.py <fuse delay> <sky quality offset> [<temp calibration offset>, ...]'
    print '   fuse delay - configurable fuse delay (ms) (default: 100)'
    print '   sky quality offset - mpas of completely dark sky (default: 22.0)'
    print '   temp calibration offsets - temperature offset to apply for sensor 1..4'

def cmdlineargs(args):
    if len(args) < 3:
        usage()
        sys.exit(1)

    return (int(sys.argv[1]), int(float(sys.argv[2])*10), map(lambda x: int(float(x)*10.0), sys.argv[3:]))

def inttobytes(n):
    return (n&0xFF, (n&0xFF00)>>8)

def updatesettings(fuseDelay, skyQualityOffset, tempCalibrationOffsets):
    rh = TeensyRawhid.Rawhid()
    buffer = [ord('K')]
    buffer.append(0x14)
    buffer.append(fuseDelay)
    buffer.append(skyQualityOffset)
    for i in range(0, 4):
        buffer.append(tempCalibrationOffsets[i] if len(tempCalibrationOffsets) >= (i+1) else 0)
    while len(buffer) < 64:
        buffer.append(0)
    print buffer
    rh.open(vid=KOMAHUB_VID, pid=KOMAHUB_PID)
    rh.send("".join(map(lambda x:chr(x), buffer)), 1000)
    rh.close()

if __name__ == '__main__':
    (fuseDelay, skyQualityOffset, tempCalibrationOffsets) = cmdlineargs(sys.argv)
    updatesettings(fuseDelay, skyQualityOffset, tempCalibrationOffsets)
