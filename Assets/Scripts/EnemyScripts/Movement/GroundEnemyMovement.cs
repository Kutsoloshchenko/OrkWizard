using System;
using UnityEngine;

namespace OrkWizard
{
    public class GroundEnemyMovement : MonoBehaviour
    {
        private Enemy enemy;

        [SerializeField]
        private EnemyMovementSO movementSO;

        private void Awake()
        {
            enemy = GetComponent<Enemy>();
        }

        private void Start()
        {
            enemy.RbController.UpdateXSpeed(movementSO.maxSpeed);
        }

        private void FixedUpdate()
        {
            CheckCollisionWithLayers();
            Move();
        }

        private void CheckCollisionWithLayers()
        {
            if (enemy.CollisionCheck(movementSO.collisionDistance, movementSO.collistionLayers))
            {
                enemy.Flip();
            }
        }

        private void Move()
        {
            var direction = enemy.IsFacingLeft ? -1 : 1;
            enemy.RbController.UpdateXSpeed(movementSO.maxSpeed * direction);
        }
    }
}
