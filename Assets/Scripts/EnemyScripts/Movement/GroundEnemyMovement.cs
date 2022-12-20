using System;
using UnityEngine;

namespace OrkWizard
{
    public class GroundEnemyMovement : BaseEnemyMovement, IEnemyMovement
    {

        private void Start()
        {
            enemy.RbController.UpdateXSpeed(movementSO.maxSpeed);
            AdjustMovementFunction();
        }

        private void OnDisable()
        {
            enemy.RbController.UpdateXSpeed(0);
        }

        private void CheckCollisionWithLayers()
        {
            if (enemy.CollisionCheck(movementSO.collisionDistance, movementSO.collistionLayers))
            {
                enemy.Flip();
            }
        }

        private void PlayerRelatedMovement(bool towardsPlayer)
        {
            // Just in case we still did not find a player
            if (enemy.PlayerReference != null)
            {
                // Flip enemy character so i faces the player
                enemy.Flip(towardsPlayer);

                // Ideally here i need to check is player is below or above the enemy, and implement way to jump
                // if (player.Above()) => Jump()

                // Add speed so we go to the player
                ApplySpeed();
            }
        }

        private void ApplySpeed()
        {
            var direction = enemy.IsFacingLeft ? -1 : 1;
            enemy.RbController.UpdateXSpeed(movementSO.maxSpeed * direction);
        }

        protected override void Patrol()
        {
            CheckCollisionWithLayers();
            ApplySpeed();
        }

        protected override void OffensiveManuvers()
        {
            PlayerRelatedMovement(true);
        }

        protected override void RunAway()
        {
            PlayerRelatedMovement(false);
        }
    }
}
