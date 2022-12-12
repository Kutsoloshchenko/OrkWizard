using System.Collections;
using UnityEngine;

namespace OrkWizard
{
    public class GrappleState : IState
    {
        public void OnEnter(BaseStateManager stateManager)
        {
            stateManager.Enemy.Flip(true);
            stateManager.StartCoroutine(GrappleCoroutine(stateManager));
        }

        public void OnExit(BaseStateManager stateManager)
        {
            return;
        }

        public void OnFixedUpdate(BaseStateManager stateManager)
        {
            return;
        }

        public void OnUpdate(BaseStateManager stateManager)
        {
            return;
        }

        private IEnumerator GrappleCoroutine(BaseStateManager stateManager)
        {
            var dmgAplier = stateManager.Enemy.PlayerReference.GetComponent<IDamagable>();
            stateManager.Enemy.PlayerReference.ChangePlayerActiveStatus(false);
            stateManager.Enemy.Animator.ChangeAnimation(stateManager.Enemy.EnemySO.grappleClip.name);

            yield return new WaitForSeconds(stateManager.Enemy.EnemySO.grappleClip.length);

            if (stateManager.Enemy.PlayerReference == null)
            {
                stateManager.Enemy.SetPlayerReference(GameObject.FindGameObjectWithTag(Enemy.playerTag).GetComponent<PlayerCharacter>());
            }
            stateManager.Enemy.PlayerReference.ChangePlayerActiveStatus(true);
            dmgAplier.ApplyDmg(stateManager.Enemy.EnemySO.grappleDmg, Element.Physical);

            stateManager.ChangeState(new DisengageState());
    }
    }
}
