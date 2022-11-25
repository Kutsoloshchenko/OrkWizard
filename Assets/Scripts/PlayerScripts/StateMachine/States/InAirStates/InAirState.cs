using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OrkWizard
{
    public class InAirState : InAirSuperState, IState
    {

        public override void OnEnter(StateManager stateManager)
        {
            stateManager.Character.Animator.ChangeAnimation(InAirAnimation(stateManager.Character.rbController.GetCurrentSpeed().y));
        }

        public override void OnExit(StateManager stateManager)
        {
            return;
        }

        public override void OnFixedUpdate(StateManager stateManager)
        {
            base.OnFixedUpdate(stateManager);

            if (stateManager.Character.WallCheck())
            {
                stateManager.ChangeState(stateManager.WallTouchState);
                return;
            }

            if (stateManager.Character.PlatformSideCheck())
            {
                stateManager.ChangeState(stateManager.PlatformHangState);
                return;
            }

            stateManager.Character.Animator.ChangeAnimation(InAirAnimation(stateManager.Character.rbController.GetCurrentSpeed().y));
        }

        private string InAirAnimation(float verticalSpeed)
        {
            if (verticalSpeed >= 0)
            {
                return _ollie;
            }
            return _fall;
        }
    }
}
