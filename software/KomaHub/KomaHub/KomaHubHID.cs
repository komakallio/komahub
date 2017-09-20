using System;
using System.Linq;
using System.Collections.Generic;
using Mighty.HID;

namespace KomaHub
{

    public class KomaHubHID
    {
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

        public class KomahubFactorySettings
        {
            public int FirmwareVersion;
            public int SerialNumber;
            public bool featureTempProbe;
            public bool featureSkyQuality;
            public bool featureAmbientPTH;
            public bool featureSkyTemperature;
        }

        public class KomahubStatus {
            public bool[] relayIsOpen;
            public bool[] fuseIsBlown;
            public byte[] pwmDuty;
            public float inputVoltage;
            public float[] outputCurrent;
        }

        public class KomahubOutputSettings
        {
            public enum OutputType
            {
                OFF = 0,
                DC = 1,
                PWM = 2,
                PWM_PID_HEAT = 3,
                PWM_PID_COOL = 4,
                PWM_FAST = 5
            }

            public OutputType[] outputTypes;
            public String[] outputNames;
            public float[] fuseCurrent;
        }

        private HIDDev device;
        private static bool attached;

        public KomaHubHID()
        {
        }

        public bool openDevice() 
        {
            HIDInfo hidInfo = HIDBrowse.Browse().Where(d => d.Vid == VENDORID && d.Pid == PRODUCTID).FirstOrDefault();
            if (hidInfo == null)
            {
                return false;
            }

            device = new HIDDev();
            bool success = device.Open(hidInfo);
            if (!success)
            {
                return false;
            }

            return true;
        }

        public void setRelay(int output, bool enabled)
        {
            byte[] report = new byte[65];
            report[0] = 0;
            report[1] = KOMAHUB_MAGIC;
            report[2] = 3;
            report[3] = Commands.SetRelay;
            report[4] = (byte)output;
            report[5] = enabled ? (byte)1 : (byte)0;
            device.Write(report);
        }

        public KomahubFactorySettings readFactorySettings()
        {
            byte[] report = new byte[65];
            report[0] = 0;
            report[1] = KOMAHUB_MAGIC;
            report[2] = 1;
            report[3] = Commands.GetFactorySettings;
            device.Write(report);

            device.Read(report);
            KomahubFactorySettings factorySettings = new KomahubFactorySettings();
            factorySettings.FirmwareVersion = report[0] << 8 + report[1];
            factorySettings.SerialNumber = report[2] << 8 + report[3];
            return factorySettings;
        }

        public KomahubStatus readStatus()
        {
            byte[] report = new byte[65];
            report[0] = 0;
            report[1] = KOMAHUB_MAGIC;
            report[2] = 1;
            report[3] = Commands.GetStatus;
            device.Write(report);

            device.Read(report);
            KomahubStatus status = new KomahubStatus();
            status.relayIsOpen = new bool[6];
            status.fuseIsBlown = new bool[6];
            status.pwmDuty = new byte[6];
            status.outputCurrent = new float[6];
            for (int output = 0; output < 6; output++) 
            {
                status.relayIsOpen[output] = (report[5] & (1 << output)) != 0;
                status.fuseIsBlown[output] = (report[6] & (1 << output)) != 0;
                status.pwmDuty[output] = report[7 + output];
                status.outputCurrent[output] = report[7 + 6 + 1 + output] / 10.0f;
            }
            status.inputVoltage = report[7 + 6] / 10.0f;

            return status;
        }
    }
}