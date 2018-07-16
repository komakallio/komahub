using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.ComponentModel;

namespace KomaHub
{

    public class KomaHubHID
    {
        [DllImport(@"teensyhidlib.dll", CallingConvention = CallingConvention.Cdecl) ]
        static extern int rawhid_open(int max, int vid, int pid, int usage_page, int usage);
        [DllImport(@"teensyhidlib.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern int rawhid_recv(int num, [MarshalAs(UnmanagedType.LPArray)]byte[] buf, int len, int timeout);
        [DllImport(@"teensyhidlib.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern int rawhid_send(int num, [MarshalAs(UnmanagedType.LPArray)]byte[] buf, int len, int timeout);
        [DllImport(@"teensyhidlib.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern void rawhid_close(int num);
        
        private const int VENDORID = 0x1209;
        private const int PRODUCTID = 0x4242;
        private const byte KOMAHUB_MAGIC = (byte)'K';
        private readonly object hubLock = new object();

        private static class Commands
        {
            public const byte Identify = 0x01;
            public const byte GetFactorySettings = 0x02;
            public const byte GetOutputSettings = 0x03;
            public const byte GetStatus = 0x04;
            public const byte SetRelay = 0x10;
            public const byte SetPwmDuty = 0x11;
            public const byte ResetFuse = 0x12;
            public const byte ConfigureOutput = 0x13;
        }

        public bool Connected { get; set; }

        public KomaHubHID()
        {
        }

        public bool openDevice() 
        {
            if (Connected)
                return true;

            Connected = rawhid_open(1, 0x1209, 0x4242, -1, -1) != 0;
            return Connected;
        }

        public void closeDevice()
        {
            if (Connected)
            {
                rawhid_close(0);
                Connected = false;
            }
        }

        public void setRelay(int output, bool enabled)
        {
            byte[] report = new byte[64];
            report[0] = KOMAHUB_MAGIC;
            report[1] = Commands.SetRelay;
            report[2] = (byte)output;
            report[3] = enabled ? (byte)1 : (byte)0;

            lock (hubLock)
            {
                send(report);
            }
        }

        public void resetFuse(int output)
        {
            byte[] report = new byte[64];
            report[0] = KOMAHUB_MAGIC;
            report[1] = Commands.ResetFuse;
            report[2] = (byte)output;

            lock (hubLock)
            {
                send(report);
            }
        }

        public KomahubFactorySettings readFactorySettings()
        {
            byte[] report = new byte[64];
            byte[] result = new byte[64];
            report[0] = KOMAHUB_MAGIC;
            report[1] = Commands.GetFactorySettings;
            lock (hubLock)
            {
                send(report);
                recv(result);
            }
            KomahubFactorySettings factorySettings = new KomahubFactorySettings();
            factorySettings.FirmwareVersion = (result[0] << 8) + result[1];
            factorySettings.SerialNumber = (result[3] << 8) + result[2];
            return factorySettings;
        }

        private void send(byte[] buffer)
        {
            int success = rawhid_send(0, buffer, 64, 100);
            switch (success) {
                case 0:
                    MessageBox.Show("Communications Timeout");
                    break;
                case -1:
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                default:
                    break;
            }
        }

        private void recv(byte[] buffer)
        {
            int success = rawhid_recv(0, buffer, 64, 500);
            switch (success)
            {
                case 0:
                    MessageBox.Show("Communications Timeout");
                    break;
                case -1:
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                default:
                    break;
            }

        }

        private Int16 toInt16(byte a, byte b)
        {
            int value = a + 256 * b;
            if (value >= 32768)
                value = -(65536 - value);
            return (Int16)value;
        }

        public KomahubStatus readStatus()
        {
            byte[] report = new byte[64];
            byte[] result = new byte[64];
            report[0] = KOMAHUB_MAGIC;
            report[1] = Commands.GetStatus;

            lock (hubLock)
            {
                send(report);
                recv(result);
            }
            KomahubStatus status = new KomahubStatus();
            status.relayIsOpen = new bool[6];
            status.fuseIsBlown = new bool[6];
            status.pwmDuty = new byte[6];
            status.outputCurrent = new float[6];
            for (int output = 0; output < 6; output++) 
            {
                status.relayIsOpen[output] = (result[0] & (1 << output)) != 0;
                status.fuseIsBlown[output] = (result[1] & (1 << output)) != 0;
                status.pwmDuty[output] = result[2 + output];
                status.outputCurrent[output] = result[2 + 6 + 1 + output] / 10.0f;
            }
            status.inputVoltage = result[2 + 6] / 10.0f;

            status.numberOfTemperatures = result[2 + 6 + 1 + 6];
            for (int sensor = 0; sensor < status.numberOfTemperatures; sensor++)
            {
                status.temperatures[sensor] = toInt16(result[2 + 6 + 1 + 6 + 1 + sensor * 2 + 0], result[2 + 6 + 1 + 6 + 1 + sensor * 2 + 1]) / 10.0f;
            }

            return status;
        }

        public KomahubOutput readOutput(int outputNumber)
        {
            byte[] report = new byte[64];
            byte[] result = new byte[64];
            report[0] = KOMAHUB_MAGIC;
            report[1] = Commands.GetOutputSettings;
            report[2] = (byte)outputNumber;

            lock (hubLock)
            {
                send(report);
                recv(result);
            }
            KomahubOutput output = new KomahubOutput();

            byte[] name = new byte[16];
            Array.Copy(result, name, 16);
            output.name = System.Text.Encoding.UTF8.GetString(name).TrimEnd('\0');
            output.fuseCurrent = result[16] / 10.0f;
            output.type = (KomahubOutput.OutputType)result[17];
            return output;
        }

        public void configureOutput(int outputNumber, KomahubOutput output)
        {
            byte[] report = new byte[64];
            report[0] = KOMAHUB_MAGIC;
            report[1] = Commands.ConfigureOutput;
            report[2] = (byte)outputNumber;
            report[3] = (byte)output.type;
            report[4] = (byte)(output.fuseCurrent*10);
            byte[] nameBytes = System.Text.Encoding.UTF8.GetBytes(output.name);
            for (int i = 0; i < 16; i++)
            {
                report[5 + i] = (i < nameBytes.Length ? nameBytes[i] : (byte)0);
            }

            lock (hubLock) 
            {
                send(report);
            }
        }

        public void setPwmDuty(int outputNumber, int duty)
        {
            byte[] report = new byte[64];
            report[0] = KOMAHUB_MAGIC;
            report[1] = Commands.SetPwmDuty;
            report[2] = (byte)outputNumber;
            report[3] = (byte)duty;
            lock (hubLock)
            {
                send(report);
            }
        }
    }
}