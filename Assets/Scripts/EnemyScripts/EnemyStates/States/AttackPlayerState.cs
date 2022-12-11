using OrkWizard;
using System.Collections;
using UnityEngine;

namespace OrkWizard
{
    public class AttackPlayerState : IState
    {
        private IWeapon currentWeapon = null;
        private bool IsAttacking = false;

        public void OnEnter(BaseStateManager stateManager)
        {
            currentWeapon = stateManager.Enemy.WeaponController.FindAppropriateWeapon();
        }

        public void OnExit(BaseStateManager stateManager)
        {
            return;
        }

        public void OnFixedUpdate(BaseStateManager stateManager)
        {
            if (!IsAttacking)
            {
                if (currentWeapon != null && currentWeapon.CanAttack())
                {
                    // Currently its the only attack we have, might need to refactor this later to include more attacks
                    if (currentWeapon.IsThroable())
                    {
                        IsAttacking = true;
                        stateManager.StartCoroutine(ThrowCoroutine(currentWeapon, stateManager));
                    }
                }
            }
        }

        public void OnUpdate(BaseStateManager stateManager)
        {
            return;
        }

        private IEnumerator ThrowCoroutine(IWeapon weapon, BaseStateManager stateManager)
        {
            stateManager.Enemy.Flip(true);
            yield return new WaitForSeconds(0.1f);

            stateManager.Enemy.Animator.ChangeAnimation(weapon.GetAnimationName());
            yield return new WaitForSeconds(weapon.GetAnimationLength());

            var direction = stateManager.Enemy.IsFacingLeft ? Vector2.left : Vector2.right;
            var startingPossition = FindStartLocation(direction, weapon, stateManager.Enemy);
            weapon.Attack(startingPossition, direction, new Vector2(stateManager.Enemy.RbController.GetCurrentSpeed().x, 0));
            stateManager.Enemy.StartAttackCoolDown();
            stateManager.ChangeState(new EnemyIdleState());

        }

        protected Vector2 FindStartLocation(Vector2 direction, IWeapon weapon, Enemy enemy)
        {
            var weaponOffsetX = weapon.GetWeaponOffsetX();
            var weaponOffsetY = weapon.GetWeaponOffsetY();

            var colliderSize = enemy.GetColliderSize();

            float x = enemy.transform.position.x + (colliderSize.x * 0.5f + weaponOffsetX) * direction.x;
            float y = enemy.transform.position.y + weaponOffsetY;

            return new Vector2(x, y);
        }

    }
}
