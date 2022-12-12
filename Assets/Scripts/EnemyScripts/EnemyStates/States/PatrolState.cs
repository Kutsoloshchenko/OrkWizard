using UnityEngine;

namespace OrkWizard
{
    public class PatrolState : BaseMovementState, IState
    {

        public override void OnEnter(BaseStateManager stateManager)
        {
            movementType = MovementType.Patrol;
            base.OnEnter(stateManager);
        }
    }
}
