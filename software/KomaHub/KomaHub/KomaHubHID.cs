using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;

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
            report[1] = 3;
            report[2] = Commands.SetRelay;
            report[3] = (byte)output;
            report[4] = enabled ? (byte)1 : (byte)0;
            rawhid_send(0, report, 64, 100);
        }

        public KomahubFactorySettings readFactorySettings()
        {
            byte[] report = new byte[64];
            report[0] = KOMAHUB_MAGIC;
            report[1] = 1;
            report[2] = Commands.GetFactorySettings;
            rawhid_send(0, report, 64, 100);

            rawhid_recv(0, report, 64, 100);
            KomahubFactorySettings factorySettings = new KomahubFactorySettings();
            factorySettings.FirmwareVersion = (report[0] << 8) + report[1];
            factorySettings.SerialNumber = (report[2] << 8) + report[3];
            return factorySettings;
        }

        public KomahubStatus readStatus()
        {
            byte[] report = new byte[64];
            report[0] = KOMAHUB_MAGIC;
            report[1] = 1;
            report[2] = Commands.GetStatus;
            rawhid_send(0, report, 64, 100);

            rawhid_recv(0, report, 64, 100);
            KomahubStatus status = new KomahubStatus();
            status.relayIsOpen = new bool[6];
            status.fuseIsBlown = new bool[6];
            status.pwmDuty = new byte[6];
            status.outputCurrent = new float[6];
            for (int output = 0; output < 6; output++) 
            {
                status.relayIsOpen[output] = (report[0] & (1 << output)) != 0;
                status.fuseIsBlown[output] = (report[1] & (1 << output)) != 0;
                status.pwmDuty[output] = report[2 + output];
                status.outputCurrent[output] = report[2 + 6 + 1 + output] / 10.0f;
            }
            status.inputVoltage = report[2 + 6] / 10.0f;

            return status;
        }

        public KomahubOutput readOutput(int outputNumber)
        {
            byte[] report = new byte[64];
            report[0] = KOMAHUB_MAGIC;
            report[1] = 1;
            report[2] = Commands.GetOutputSettings;
            report[3] = (byte)outputNumber;
            rawhid_send(0, report, 64, 100);

            byte[] result = new byte[64];
            rawhid_recv(0, result, 64, 100);
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
            report[1] = 18;
            report[2] = Commands.ConfigureOutput;
            report[3] = (byte)outputNumber;
            report[4] = (byte)output.type;
            report[5] = (byte)(output.fuseCurrent*10);
            byte[] nameBytes = System.Text.Encoding.UTF8.GetBytes(output.name);
            for (int i = 0; i < nameBytes.Length; i++)
            {
                report[6 + i] = nameBytes[i];
            }
            rawhid_send(0, report, 64, 100);
        }

        public void setPwmDuty(int outputNumber, int duty)
        {
            byte[] report = new byte[64];
            report[0] = KOMAHUB_MAGIC;
            report[1] = 2;
            report[2] = Commands.SetPwmDuty;
            report[3] = (byte)outputNumber;
            report[4] = (byte)duty;
            rawhid_send(0, report, 64, 100);
        }
    }
}