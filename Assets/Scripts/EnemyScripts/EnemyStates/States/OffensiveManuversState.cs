using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrkWizard
{
    public class OffensiveManuversState : BaseMovementState, IState
    {

        public override void OnEnter(BaseStateManager stateManager)
        {
            movementType = MovementType.OffensiveManuvers;
            base.OnEnter(stateManager);
        }

        public override void OnUpdate(BaseStateManager stateManager)
        {
            if (stateManager.Enemy.PlayerReference != null)
            {
                if (stateManager.Enemy.CanAttack())
                {
                    stateManager.ChangeState(new AttackPlayerState());
                }
            }
            else
            {
                stateManager.ChangeState(new PatrolState());
            }

        }
    }
}


