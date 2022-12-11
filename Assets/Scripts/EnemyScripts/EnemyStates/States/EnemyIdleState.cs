using UnityEngine;

namespace OrkWizard
{
    public class EnemyIdleState : BaseEnemyState, IState
    {
        public override void OnEnter(BaseStateManager stateManager)
        {
            stateManager.Enemy.Animator.ChangeAnimation("Idle");
        }

        public override void OnExit(BaseStateManager stateManager)
        {
            return;
        }

        public override void OnFixedUpdate(BaseStateManager stateManager)
        {
            base.OnFixedUpdate(stateManager);
            stateManager.ChangeState(new PatrolState());
        }
    }
}
