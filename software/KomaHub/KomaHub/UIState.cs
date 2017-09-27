using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomaHub
{
    public class UIState
    {
        public bool Connected { get; set; }
        public KomahubFactorySettings FactorySettings { get; set; }
        public KomahubStatus Status { get; set; }
        public KomahubOutput[] Outputs { get; set; }

        public string StatusText { get; set; }

        public UIState()
        {
            FactorySettings = new KomahubFactorySettings();
            Status = new KomahubStatus();
            Outputs = new KomahubOutput[6];
            for (int i = 0; i < 6; i++)
                Outputs[i] = new KomahubOutput();
        }
    }
}
