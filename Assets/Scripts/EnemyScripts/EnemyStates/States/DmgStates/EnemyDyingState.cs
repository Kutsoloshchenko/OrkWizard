using System.Collections;
using UnityEngine;

namespace OrkWizard
{
    public class EnemyDyingState : IState
    {
        public void OnEnter(BaseStateManager stateManager)
        {
            stateManager.Enemy.SetMovement(false);
            stateManager.StartCoroutine(Death(stateManager));
        }

        public void OnExit(BaseStateManager stateManager)
        {
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

        private IEnumerator Death(BaseStateManager stateManager)
        {
            stateManager.Enemy.Animator.ChangeAnimation(stateManager.Enemy.EnemySO.deathAnimation.name);
            stateManager.Enemy.enabled = false;
            yield return new WaitForSeconds(stateManager.Enemy.EnemySO.deathAnimation.length);

            GameObject.Destroy(stateManager.gameObject);
        }

    }
}
