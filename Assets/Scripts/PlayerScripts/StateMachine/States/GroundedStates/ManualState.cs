using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrkWizard
{
    public class ManualState : GroundedSuperState, IState
    {
        private const string _manual = "Manual";

        public override void OnEnter(BaseStateManager stateManager)
        {
            stateManager.Character.Animator.ChangeAnimation(_manual);
            stateManager.Character.CapHorizontalSpeed(stateManager.Character.playerScriptableObject.maxSpeed * stateManager.Character.playerScriptableObject.manualSpeedMultiplier);
        }

        public override void OnExit(BaseStateManager stateManager)
        {
            stateManager.Character.CapHorizontalSpeed(stateManager.Character.playerScriptableObject.originalMaxSpeed);
        }

        public override void OnFixedUpdate(BaseStateManager stateManager)
        {
            base.OnFixedUpdate(stateManager);

            if (!stateManager.Character.Input.Manual)
            {
                stateManager.ChangeState(new IdleState());
            }
        }

    }
}
