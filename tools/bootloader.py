import sys
import TeensyRawhid

KOMAHUB_VID = 0x1209
KOMAHUB_PID = 0x4242

def bootloader():
    rh = TeensyRawhid.Rawhid()
    buffer = [ord('K')]
    buffer.append(0xFB)
    while len(buffer) < 64:
        buffer.append(0)
    print buffer
    rh.open(vid=KOMAHUB_VID, pid=KOMAHUB_PID)
    rh.send("".join(map(lambda x:chr(x), buffer)), 1000)
    rh.close()

if __name__ == '__main__':
    bootloader()
