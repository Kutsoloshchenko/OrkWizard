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

        public override void OnEnter(StateManager stateManager)
        {
            stateManager.Character.Animator.ChangeAnimation(_idleAnimation);
        }

        public override void OnExit(StateManager stateManager)
        {
            return;
        }

        public override void OnUpdate(StateManager stateManager)
        {
            base.OnUpdate(stateManager);
            if (stateManager.Character.Input.HorizontalInput != Vector2.zero)
            {
                 stateManager.ChangeState(stateManager.MovingState);
            }
        }
    }
}
