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

        public override void OnEnter(BaseStateManager stateManager)
        {
            stateManager.Character.Animator.ChangeAnimation(InAirAnimation(stateManager.Character.rbController.GetCurrentSpeed().y));
        }

        public override void OnExit(BaseStateManager stateManager)
        {
            return;
        }

        public override void OnFixedUpdate(BaseStateManager stateManager)
        {
            base.OnFixedUpdate(stateManager);

            if (stateManager.Character.WallCheck())
            {
                stateManager.ChangeState(new WallTouchState());
                return;
            }

            if (stateManager.Character.PlatformSideCheck())
            {
                stateManager.ChangeState(new PlatformHangState());
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
