using UnityEngine;

namespace OrkWizard
{
    public class EnemyRigidBodyController : BaseRigidbodyController
    {
        private Enemy enemy;

        public EnemyRigidBodyController(Rigidbody2D rb, Enemy enemyObject) : base(rb)
        {
            enemy = enemyObject;
        }
    }
}
