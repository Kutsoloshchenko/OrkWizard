using System;
using UnityEngine;

namespace OrkWizard
{
    public class PlayerDetector
    {
        private Enemy enemy;
        private PlayerDetectorSO detectorSO;

        private Func<RaycastHit2D> detectionFunction;

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

        public RaycastHit2D Detect()
        {
            return detectionFunction();
        }

        private RaycastHit2D BoxColliderDetection()
        {
            var direction = enemy.IsFacingLeft ? Vector2.left : Vector2.right;
            Vector2 origin = (new Vector2(enemy.transform.position.x + detectorSO.offset.x*direction.x, enemy.transform.position.y + detectorSO.offset.y));
            return Physics2D.BoxCast(origin, detectorSO.size, detectorSO.angle, direction, 0, detectorSO.detectionLayer);
        }

        private RaycastHit2D SphereCollider()
        {
            var direction = enemy.IsFacingLeft ? Vector2.left : Vector2.right;
            Vector2 origin = (new Vector2(enemy.transform.position.x + detectorSO.offset.x * direction.x, enemy.transform.position.y + detectorSO.offset.y));
            return Physics2D.CircleCast(origin, detectorSO.size.x, direction, 0, detectorSO.detectionLayer);
        }
    }
}
