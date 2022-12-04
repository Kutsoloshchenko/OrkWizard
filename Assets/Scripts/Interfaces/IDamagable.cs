using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrkWizard
{
    public interface IDamagable
    {
        void ApplyDmg(float dmg);
        void ApplyDmg(float dmg, Element type);
        void ApplyDmg(float dmg, Element type, float tickTime);
        void StopTickDmg(int ticksAfterStop = 0);
    }
}
