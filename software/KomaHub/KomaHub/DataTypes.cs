using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomaHub
{
    public class KomahubFactorySettings
    {
        public int FirmwareVersion;
        public int SerialNumber;
        public int fuseDelay;
        public float sqmOffset;
        public bool featureTempProbe;
        public bool featureSkyQuality;
        public bool featureAmbientPTH;
        public bool featureSkyTemperature;
    }

    public class KomahubStatus
    {
        public bool[] relayIsOpen = new bool[6];
        public bool[] fuseIsBlown = new bool[6];
        public byte[] pwmDuty = new byte[6];
        public float inputVoltage;
        public float[] outputCurrent = new float[6];

        public float temperature;
        public float humidity;
        public float pressure;
        public float externalTemperature;
        public float skyTemperature;
        public float skyTemperatureAmbient;
        public float skyQuality;
    }

    public class KomahubOutput
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

        public OutputType type = OutputType.OFF;
        public String name = "";
        public float fuseCurrent = 0.0f;

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            KomahubOutput other = (KomahubOutput)obj;

            return type.Equals(other.type) && name.Equals(other.name) && fuseCurrent == other.fuseCurrent;
        }

        public override int GetHashCode()
        {
            return type.GetHashCode() + name.GetHashCode() + fuseCurrent.GetHashCode();
        }
    }
}
