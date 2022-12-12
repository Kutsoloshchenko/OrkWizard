using UnityEngine;

namespace OrkWizard
{
    public abstract class BaseMovementState : BaseEnemyState
    {
        private const string _moveAnimation = "Move";
        protected MovementType movementType = MovementType.NoMovement;

        public override void OnEnter(BaseStateManager stateManager)
        {
            stateManager.Enemy.Animator.ChangeAnimation(_moveAnimation);
            stateManager.Enemy.SetMovement(true);
            stateManager.Enemy.Movement.SetCurrentMovementType(movementType);
        }

        public override void OnExit(BaseStateManager stateManager)
        {
            stateManager.Enemy.SetMovement(false);
            stateManager.Enemy.Movement.SetCurrentMovementType(MovementType.NoMovement);
        }

    }
}
