using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrkWizard
{
    public class PersueState : BaseEnemyState, IState
    {
        private const string _moveAnimation = "Move";

        public override void OnEnter(BaseStateManager stateManager)
        {
            stateManager.Enemy.Animator.ChangeAnimation(_moveAnimation);
            stateManager.Enemy.SetMovement(true);
            stateManager.Enemy.Movement.SetCurrentMovementType(MovementType.Patrol);
        }

        public override void OnExit(BaseStateManager stateManager)
        {
            stateManager.Enemy.SetMovement(false);
            stateManager.Enemy.Movement.SetCurrentMovementType(MovementType.NoMovement);
        }
    }
}


