using System.Collections;
using UnityEngine;

namespace OrkWizard
{
    public class EnemyCriticallyHit : IState
    {
        public void OnEnter(BaseStateManager stateManager)
        {
            stateManager.Enemy.IsCriticallyHit = true;
            stateManager.Enemy.SetMovement(false);
            stateManager.StartCoroutine(CriticalDmgKnockback(stateManager));
        }

        public void OnExit(BaseStateManager stateManager)
        {
            stateManager.Enemy.IsCriticallyHit = false;
            stateManager.StopAllCoroutines();
        }

        public void OnFixedUpdate(BaseStateManager stateManager)
        {
            return;
        }

        public void OnUpdate(BaseStateManager stateManager)
        {
            return;
        }

        private IEnumerator CriticalDmgKnockback(BaseStateManager stateManager)
        {
            var direction = stateManager.Enemy.IsFacingLeft ? Vector2.right : Vector2.left;
            stateManager.Enemy.RbController.ApplyForce(new Vector2(stateManager.Enemy.EnemySO.knockBackForce.x * direction.x, stateManager.Enemy.EnemySO.knockBackForce.y));
            stateManager.Enemy.Animator.ChangeAnimation(stateManager.Enemy.EnemySO.knockBackClip.name);
            yield return new WaitForSeconds(stateManager.Enemy.EnemySO.knockBackTime);

            stateManager.Enemy.RbController.UpdateSpeed(0,0);
            stateManager.ChangeState(new EnemyIdleState());
        }

    }
}
