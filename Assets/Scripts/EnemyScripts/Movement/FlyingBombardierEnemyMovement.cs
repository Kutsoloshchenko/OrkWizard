using System;
using UnityEngine;

namespace OrkWizard
{
    public class FlyingBombardierEnemyMovement : BaseEnemyMovement, IEnemyMovement
    {
        private int horizontalMultiplier = 1;
        private int verticalMultiplier = 1;

        private void Start()
        {
            enemy.RbController.SetGravity(0);
            AdjustMovementFunction();
        }

        private void OnDisable()
        {
            enemy.RbController.UpdateSpeed(0, 0);
        }

        protected override void Patrol()
        {
            CheckCollisionWithLayers();
            ApplySpeed();
        }

        private void ApplySpeed()
        {
            var verticalSign = enemy.IsFacingLeft ? -1 : 1;
            enemy.RbController.UpdateSpeed(movementSO.maxSpeed * verticalSign * verticalMultiplier, movementSO.maxSpeed * horizontalMultiplier);
        }

        protected override void OffensiveManuvers()
        {
            // we want to stay above the player, in range of the weapon.
            // and then drop bombs on the player

            // Check if we are below the player, if so - lets move up
            if (enemy.PlayerReference)
            {
                if (enemy.PlayerReference.transform.position.y >= enemy.transform.position.y)
                {
                    horizontalMultiplier = 1;
                }
                else
                {
                    // If we are already higher then player, we dont need to move up and down
                    horizontalMultiplier = 0;
                }

                // It alligns itself right over the player to drop the bomb 
                // wanna face the player;

                if (Mathf.Abs(enemy.transform.position.x - enemy.PlayerReference.transform.position.x) >= 0.1)
                {
                    enemy.Flip(true);
                    verticalMultiplier = 1;
                }
                else
                {
                    verticalMultiplier = 0;
                }

                ApplySpeed();
            }
        }

        protected override void RunAway()
        {
            PlayerRelatedMovement(false);
        }

        private void PlayerRelatedMovement(bool towardsPlayer)
        {
            // Just in case we still did not find a player
            if (enemy.PlayerReference != null)
            {
                // Flip enemy character so it faces the player
                enemy.Flip(towardsPlayer);

                // check to see if we are above the player
                if (enemy.PlayerReference.transform.position.y >= enemy.transform.position.y)
                {
                    horizontalMultiplier = towardsPlayer ? 1 : -1;
                }
                else
                {
                    horizontalMultiplier = !towardsPlayer ? 1 : -1;
                }

                // Add speed so we go to the player
                ApplySpeed();
            }
        }

        private void CheckCollisionWithLayers()
        {
            if (enemy.CollisionCheck(movementSO.collisionDistance, movementSO.collistionLayers))
            {
                enemy.Flip();
            }

            var side = horizontalMultiplier > 0 ? Vector2.up : Vector2.down;
            if (enemy.CollisionCheck(movementSO.collisionDistance, movementSO.collistionLayers, side))
            {
                horizontalMultiplier *= -1;
            }

            if (!enemy.PlayerDetector.CanSeeGroung(movementSO.collistionLayers))
            {
                horizontalMultiplier = -1;
            }
        }
    }
}
