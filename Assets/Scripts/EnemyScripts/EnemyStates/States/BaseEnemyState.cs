using UnityEngine;

namespace OrkWizard
{
    public abstract class BaseEnemyState : IState
    {
        public abstract void OnEnter(BaseStateManager stateManager);

        public abstract void OnExit(BaseStateManager stateManager);

        public virtual void OnFixedUpdate(BaseStateManager stateManager)
        {
            var raycastHit = stateManager.Enemy.PlayerDetector.Detect();
            if (raycastHit)
            {
                stateManager.Enemy.SetPlayerReference(raycastHit);
                return;
            }
            stateManager.Enemy.ForgetPlayer();
        }

        public virtual void OnUpdate(BaseStateManager stateManager)
        {
            if (stateManager.Enemy.PlayerReference != null)
            {
                if (stateManager.Enemy.CanAttack())
                {
                    stateManager.ChangeState(new AttackPlayerState());
                }
                else
                {
                    stateManager.ChangeState(new OffensiveManuversState());
                };
            }
        }
    }
}
