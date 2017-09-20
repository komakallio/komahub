import sys
import TeensyRawhid

KOMAHUB_VID = 0x16C0
KOMAHUB_PID = 0x0470

def usage():
    print 'usage: komahubsend.py <cmd> <byte> <byte> <byte>'

def cmdlineargs(args):
    if len(args) < 3:
        usage()
        sys.exit(1)
    return (int(sys.argv[1]), map(int, sys.argv[2:]))

def inttobytes(n):
    return ((n&0xFF000000)>>24, (n&0xFF0000)>>16, (n&0xFF00)>>8, n&0xFF)

def send(cmd, data):
    rh = TeensyRawhid.Rawhid()
    buffer = [ord('K'), 0]
    buffer.append(cmd)
    for b in data: 
        buffer.append(b)
    buffer[1] = len(buffer)-2
    while len(buffer) < 64:
        buffer.append(0)
    print 'Sending',
    print bytes(buffer)
    rh.open(vid=KOMAHUB_VID, pid=KOMAHUB_PID)
    rh.send("".join(map(lambda x:chr(x), buffer)), 1000)
    data = None
    try:
	data = rh.recv(64, 500)
    except IOError:
	pass
    if data and len(data) > 0:
        print 'Received',
        print "".join(map(lambda x: hex(ord(x))[2:]+' ', data))
    rh.close()

if __name__ == '__main__':
    (cmd, data) = cmdlineargs(sys.argv)
    send(cmd, data)
