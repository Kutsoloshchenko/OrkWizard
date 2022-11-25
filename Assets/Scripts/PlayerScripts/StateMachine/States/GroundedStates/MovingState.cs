using System;
using UnityEngine;

namespace OrkWizard
{
    public class MovingState : GroundedSuperState, IState
    {

        private const string _walking = "Walk";
        private const string _skating = "Skate";


        public override void OnEnter(StateManager stateManager)
        {
            stateManager.Character.Animator.ChangeAnimation(SetAppropriateAnimation(stateManager.Character.rbController.GetCurrentSpeed().x, stateManager.Character.playerScriptableObject.walkSpeed));
        }

        public override void OnExit(StateManager stateManager)
        {
            return;
        }

        public override void OnUpdate(StateManager stateManager)
        {
            base.OnUpdate(stateManager);
            if (stateManager.Character.Input.HorizontalInput == Vector2.zero && stateManager.Character.rbController.GetCurrentSpeed().x == 0)
            {
                stateManager.ChangeState(stateManager.IdleState);
                return;
            }

            if (stateManager.Character.Input.Manual)
            {
                stateManager.ChangeState(stateManager.Manual);
                return;
            }
        }

        public override void OnFixedUpdate(StateManager stateManager)
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
