using System;
using UnityEngine;

namespace OrkWizard
{
    public class MovingState : GroundedSuperState, IState
    {

        private const string _walking = "Walk";
        private const string _skating = "Skate";


        public override void OnEnter(BaseStateManager stateManager)
        {
            stateManager.Character.Animator.ChangeAnimation(SetAppropriateAnimation(stateManager.Character.rbController.GetCurrentSpeed().x, stateManager.Character.playerScriptableObject.walkSpeed));
        }

        public override void OnExit(BaseStateManager stateManager)
        {
            return;
        }

        public override void OnUpdate(BaseStateManager stateManager)
        {
            base.OnUpdate(stateManager);
            if (stateManager.Character.Input.HorizontalInput == Vector2.zero && stateManager.Character.rbController.GetCurrentSpeed().x == 0)
            {
                stateManager.ChangeState(new IdleState());
                return;
            }

            if (stateManager.Character.Input.Manual)
            {
                stateManager.ChangeState(new ManualState());
                return;
            }
        }

        public override void OnFixedUpdate(BaseStateManager stateManager)
        {
            base.OnFixedUpdate(stateManager);
            stateManager.Character.Animator.ChangeAnimation(SetAppropriateAnimation(stateManager.Character.rbController.GetCurrentSpeed().x, stateManager.Character.playerScriptableObject.walkSpeed));
        }

        private string SetAppropriateAnimation(float speed, float walkSpeed)
        {
            if (Math.Abs(speed) <= walkSpeed)
            {
                return _walking;
            }
            else
            {
                return _skating;
            }
        }
    }
}
