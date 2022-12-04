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
        public virtual void OnUpdate(BaseStateManager stateManager)
        {
            if (stateManager.Character.Input.AttackBeingPressed && (stateManager.Character.WeaponController.enabled && stateManager.Character.WeaponController.CurrentWeapon.CanAttack()))
            {
                if (stateManager.Character.WeaponController.CurrentWeapon.IsThroable())
                {
                    stateManager.ChangeState(new ThrowAttackState());
                }
                else
                {
                    stateManager.ChangeState(new SpreaderAttackingState());
                }
                return;
            }
        }
    }
}
