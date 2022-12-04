using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace OrkWizard
{
    public class Enemy : Character
    {
        [SerializeField]
        public EnemySO EnemySO;
        [SerializeField]
        public PlayerDetectorSO detectorSO;
        public EnemyRigidBodyController RbController { get; private set; }
        public PlayerDetector PlayerDetector { get; private set; }

        private BoxCollider2D collider;

        private void Awake()
        {
            EnemySO.currentHp = EnemySO.maxHp;
            collider = GetComponent<BoxCollider2D>();
            RbController = new EnemyRigidBodyController(GetComponent<Rigidbody2D>(), this);
            PlayerDetector = new PlayerDetector(this, detectorSO);
        }

        private void FixedUpdate()
        {
            if (PlayerDetector.Detect())
            {
                Debug.Log("I see player");
            }
        }

        public bool CollisionCheck(float distance, LayerMask layers)
        {
            var checkSide = IsFacingLeft ? Vector2.left : Vector2.right;
            var hit = CollisionCheckRayCast(checkSide, transform.position, (collider.size.x / 2) + distance, layers);
            if (hit)
            {
                return true;
            }
            return false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            var direction = IsFacingLeft ? -1 : 1;
            Vector2 origin = (new Vector2(transform.position.x + detectorSO.offset.x * direction, transform.position.y + detectorSO.offset.y));

            Gizmos.DrawWireCube(origin, detectorSO.size);
            Gizmos.DrawWireSphere(origin, detectorSO.size.x);

        }
    }
}
