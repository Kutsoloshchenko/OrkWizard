using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OrkWizard
{
    public abstract class TransitionToAttackSupperState
    {
        public virtual void OnUpdate(StateManager stateManager)
        {
            if (stateManager.Character.Input.AttackBeingPressed)
            {
                if (stateManager.Character.WeaponController.CurrentWeapon.IsThroable())
                {
                    stateManager.ChangeState(stateManager.ThrowingState);
                }
                else
                {
                    stateManager.ChangeState(stateManager.SpreaderState);
                }
                return;
            }
        }
    }
}
