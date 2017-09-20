using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomaHub
{
    public interface ISettingsReceiver
    {
        void ApplySettings(float voltageRatio, float sqmOffset);
    }
}
