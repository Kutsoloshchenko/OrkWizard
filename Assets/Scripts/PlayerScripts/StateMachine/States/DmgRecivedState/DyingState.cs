using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrkWizard
{
    public class DyingState : IState
    {
        private const string _death = "Rip";

        public void OnEnter(StateManager stateManager)
        {
            if (stateManager.Character.IsFacingLeft)
            {
                stateManager.Character.Flip();
            }

            stateManager.Character.Animator.ChangeAnimation(_death);
            stateManager.Character.SetHorizontalMovement(false);
            stateManager.Character.SetVerticalMovement(false);
            stateManager.Character.SetWeaponControls(false);
        }

        public void OnExit(StateManager stateManager)
        {
            stateManager.Character.SetHorizontalMovement(true);
            stateManager.Character.SetVerticalMovement(true);
            stateManager.Character.SetWeaponControls(true);
            return;
        }

        public void OnFixedUpdate(StateManager stateManager)
        {
            return;
        }

        public void OnUpdate(StateManager stateManager)
        {
            return;
        }
    }

}
