using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OrkWizard
{
    public class SpreaderAttackingState : AttackSuperState, IState
    {
        public override void OnEnter(StateManager stateManager)
        {
            stateManager.Character.WeaponController.SetAttacking(true);
            stateManager.Character.Animator.SetAttack(stateManager.Character.WeaponController.CurrentWeapon.GetAnimationName());
            var direction = stateManager.Character.isFacingLeft ? Vector2.left : Vector2.right;
            var possition = FindStartLocation(stateManager, direction);
            stateManager.Character.WeaponController.CurrentWeapon.Attack(possition, direction, new Vector2(stateManager.Character.GetCurrentSpeed().x, 0));
        }

        public override void OnExit(StateManager stateManager)
        {
            stateManager.Character.WeaponController.SetAttacking(false);
            stateManager.Character.WeaponController.CurrentWeapon.DisableNewAttacks();
            stateManager.Character.CapHorizontalSpeed(stateManager.Character.playerScriptableObject.originalMaxSpeed);
        }

        public override void OnFixedUpdate(StateManager stateManager)
        {
            stateManager.Character.Animator.SetAttack(stateManager.Character.WeaponController.CurrentWeapon.GetAnimationName());
            return;
        }

        public override void OnUpdate(StateManager stateManager)
        {
            if (!stateManager.Character.Input.AttackBeingPressed)
            {
                HandleChangeState(stateManager);
            }
        }
    }
}
