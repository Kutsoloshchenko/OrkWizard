using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OrkWizard
{
    public class IdleState : GroundedSuperState, IState
    {
        private const string _idleAnimation = "Idle";

        public override void OnEnter(BaseStateManager stateManager)
        {
            stateManager.Character.Animator.ChangeAnimation(_idleAnimation);
        }

        public override void OnExit(BaseStateManager stateManager)
        {
            return;
        }

        public override void OnUpdate(BaseStateManager stateManager)
        {
            base.OnUpdate(stateManager);
            if (stateManager.Character.rbController.GetCurrentSpeed().x != 0)
            {
                 stateManager.ChangeState(new MovingState());
            }
        }
    }
}
