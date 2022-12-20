using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrkWizard
{
    public interface IEnemyMovement
    {
        void SetCurrentMovementType(MovementType type);
        void Enable(bool value);
    }
}
