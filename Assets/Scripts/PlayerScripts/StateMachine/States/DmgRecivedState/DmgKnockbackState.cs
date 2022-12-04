using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OrkWizard
{
    public class DmgKnockbackState : IState
    {
        private const string _dmgAnimation = "Damaged";
        private float countDown = 0;

        public void OnEnter(BaseStateManager stateManager)
        {
            countDown = 0;
            stateManager.Character.Animator.ChangeAnimation(_dmgAnimation);
            stateManager.Character.SetHorizontalMovement(false);
            stateManager.Character.SetVerticalMovement(false);
            stateManager.Character.SetWeaponControls(false);
            stateManager.Character.SetRecivedDmg(true);
        }

        public void OnExit(BaseStateManager stateManager)
        {
            stateManager.Character.rbController.UpdateSpeed(0,0);
            stateManager.Character.SetHorizontalMovement(true);
            stateManager.Character.SetVerticalMovement(true);
            stateManager.Character.SetWeaponControls(true);
            stateManager.Character.SetRecivedDmg(false);
        }

        public void OnFixedUpdate(BaseStateManager stateManager)
        {
            if (countDown < stateManager.Character.playerScriptableObject.dmgKnockbackDuration)
            {
                countDown += Time.deltaTime;
                return;
            }

            if (stateManager.Character.isGrounded)
            {
                stateManager.ChangeState(new IdleState());
            }
            else
            {
                stateManager.ChangeState(new InAirState());
            }
        }

        public void OnUpdate(BaseStateManager stateManager)
        {
            return;
        }
    }
}
