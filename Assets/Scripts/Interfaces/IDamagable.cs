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
        void ApplyDmg(float dmg, DamageType type);
        void ApplyDmg(float dmg, DamageType type, float tickTime);
        void StopTickDmg();
    }
}
