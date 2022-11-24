using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OrkWizard
{
    public class ThrowAttackState : AttackSuperState, IState
    {
        public override void OnEnter(StateManager stateManager)
        {
            stateManager.Character.WeaponController.SetAttacking(true);
            stateManager.StartCoroutine(ThrowCoroutine(stateManager));
        }

        public override void OnExit(StateManager stateManager)
        {
            stateManager.Character.WeaponController.SetAttacking(false);
        }

        public override void OnFixedUpdate(StateManager stateManager)
        {
            return;
        }

        public override void OnUpdate(StateManager stateManager)
        {
            return;
        }

        private IEnumerator ThrowCoroutine(StateManager stateManager)
        {
            stateManager.Character.Animator.SetAttack(stateManager.Character.WeaponController.CurrentWeapon.GetAnimationName());
            yield return new WaitForSeconds(stateManager.Character.WeaponController.CurrentWeapon.GetAnimationLength());

            var direction = stateManager.Character.isFacingLeft ? Vector2.left : Vector2.right;
            var startingPossition = FindStartLocation(stateManager, direction);
            stateManager.Character.WeaponController.CurrentWeapon.Attack(startingPossition, direction, new Vector2(stateManager.Character.GetCurrentSpeed().x, 0));

            HandleChangeState(stateManager);
        }        
    }
}
