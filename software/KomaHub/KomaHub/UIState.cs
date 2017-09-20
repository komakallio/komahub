using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomaHub
{
    public class UIState
    {
        public class Output
        {
            public bool Enabled { get; set; }
            public bool Active { get; set; }
            public bool IsPWM { get; set; }
            public int PWMDuty { get; set; }
            public float Current { get; set; }
            public bool isOverload { get; set; }

            public Output()
            {
                Enabled = true;
                Active = false;
                IsPWM = false;
                PWMDuty = 100;
                Current = 0.0f;
                isOverload = false;
            }
        };

        public bool Connected { get; set; }
        public int FirmwareVersion { get; set; }
        public int SerialNumber { get; set; }
        public Output[] Outputs { get; set; }

        public float Temperature { get; set; }
        public float Humidity { get; set; }
        public float Pressure { get; set; }
        public float ExternalTemperature { get; set; }
        public float SkyTemperature { get; set; }
        public float SkyTemperatureAmbient { get; set; }
        public float SkyTemperatureDelta 
        { 
            get {
                return SkyTemperature - SkyTemperatureAmbient;
            }
        }
        public float SQM { get; set; }

        public float InputVoltage { get; set; }

        public string StatusText { get; set; }

        public HashSet<string> EnabledSensors { get; set; } 

        public UIState()
        {
            EnabledSensors = new HashSet<string>();
            Outputs = new Output[6];
            for (int i = 0; i < 6; i++)
                Outputs[i] = new Output();
        }

    }
}
