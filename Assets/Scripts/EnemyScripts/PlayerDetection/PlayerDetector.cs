using System;
using UnityEngine;

namespace OrkWizard
{
    public class PlayerDetector
    {
        private Enemy enemy;
        private PlayerDetectorSO detectorSO;

        private Func<Collider2D> detectionFunction;

        public PlayerDetector(Enemy enemyObj, PlayerDetectorSO so)
        {
            enemy = enemyObj;
            detectorSO = so;

            switch (detectorSO.colliderType)
            {
                case ColliderType.Box:
                    detectionFunction = () => BoxColliderDetection();
                    break;

                case ColliderType.Sphere:
                    detectionFunction = () => SphereCollider();
                    break;

            }
        }

        public Collider2D Detect()
        {
            return detectionFunction();
        }
        public bool CanSeeGroung(LayerMask layers)
        {
            var hit = Physics2D.Raycast(enemy.transform.position, Vector2.down, detectorSO.size.x, layers);
            return hit;
        }

        private Collider2D BoxColliderDetection()
        {
            var direction = enemy.IsFacingLeft ? Vector2.left : Vector2.right;
            Vector2 origin = (new Vector2(enemy.transform.position.x + detectorSO.offset.x * direction.x, enemy.transform.position.y + detectorSO.offset.y));
            var playerInRange = Physics2D.OverlapBox(origin, detectorSO.size, detectorSO.angle, detectorSO.detectionLayer);

            if (playerInRange)
            {
                return CheckDirectVision(origin, playerInRange);
            }
            return null;
        }
        private Collider2D SphereCollider()
        {
            var direction = enemy.IsFacingLeft ? Vector2.left : Vector2.right;
            Vector2 origin = (new Vector2(enemy.transform.position.x + detectorSO.offset.x * direction.x, enemy.transform.position.y + detectorSO.offset.y));
            var playerInRange = Physics2D.OverlapCircle(origin, detectorSO.size.x, detectorSO.detectionLayer);

            if (playerInRange)
            {
                return CheckDirectVision(origin, playerInRange);
            }
            return null;
        }
        private Collider2D CheckDirectVision(Vector2 origin, Collider2D playerHit)
        {
            var direction = playerHit.transform.position - enemy.transform.position;
            direction = direction / direction.magnitude;

            var hit = Physics2D.Raycast(origin, direction, detectorSO.size.x);
            if (hit.collider == playerHit)
            {
                return playerHit;
            }

            return null;
        }
    }
}
