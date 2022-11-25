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

        public void OnEnter(StateManager stateManager)
        {
            countDown = 0;
            stateManager.Character.Animator.ChangeAnimation(_dmgAnimation);
            stateManager.Character.SetHorizontalMovement(false);
            stateManager.Character.SetVerticalMovement(false);
            stateManager.Character.SetWeaponControls(false);
        }

        public void OnExit(StateManager stateManager)
        {
            stateManager.Character.rbController.UpdateSpeed(0,0);
            stateManager.Character.SetHorizontalMovement(true);
            stateManager.Character.SetVerticalMovement(true);
            stateManager.Character.SetWeaponControls(true);
            stateManager.Character.RecivedOneTimeDmg = false;
        }

        public void OnFixedUpdate(StateManager stateManager)
        {
            if (countDown < stateManager.Character.playerScriptableObject.dmgKnockbackDuration)
            {
                countDown += Time.deltaTime;
                return;
            }

            if (stateManager.Character.isGrounded)
            {
                stateManager.ChangeState(stateManager.IdleState);
            }
            else
            {
                stateManager.ChangeState(stateManager.InAirState);
            }
        }

        public void OnUpdate(StateManager stateManager)
        {
            return;
        }
    }
}
