using UnityEngine;

namespace OrkWizard
{
    public class DisengageState : BaseMovementState, IState
    {
        public override void OnEnter(BaseStateManager stateManager)
        {
            movementType = MovementType.RunAway;
            base.OnEnter(stateManager);
        }

        public override void OnUpdate(BaseStateManager stateManager)
        {
            if (stateManager.Enemy.PlayerReference == null)
            {
                stateManager.ChangeState(new PatrolState());
            }
        }
    }
}
