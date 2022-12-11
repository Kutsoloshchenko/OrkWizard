using System;
using UnityEngine;

namespace OrkWizard
{
    public class GroundEnemyMovement : MonoBehaviour
    {
        private Enemy enemy;

        private MovementType currentMovementType;
        private Action moveFunction;

        [SerializeField]
        private EnemyMovementSO movementSO;

        private void Awake()
        {
            enemy = GetComponent<Enemy>();
        }

        private void Start()
        {
            enemy.RbController.UpdateXSpeed(movementSO.maxSpeed);
            AdjustMovementFunction();
        }

        private void OnDisable()
        {
            enemy.RbController.UpdateXSpeed(0);
        }

        private void FixedUpdate()
        {
            moveFunction();
        }

        private void CheckCollisionWithLayers()
        {
            if (enemy.CollisionCheck(movementSO.collisionDistance, movementSO.collistionLayers))
            {
                enemy.Flip();
            }
        }

        private void AdjustMovementFunction()
        {
            switch (currentMovementType)
            {

                case MovementType.Patrol:
                    moveFunction = Patrol;
                    break;

                case MovementType.Pursue:
                    moveFunction = Pursue;
                    break;

                case MovementType.RunAway:
                    moveFunction = RunAway;
                    break;

                case MovementType.NoMovement:
                default:
                    moveFunction = () => { return; };
                    break;
            }
        }

        private void Patrol()
        {
            CheckCollisionWithLayers();
            ApplySpeed();
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

        private void Pursue()
        {
            PlayerRelatedMovement(true);
        }

        private void RunAway()
        {
            PlayerRelatedMovement(false);
        }

        private void ApplySpeed()
        {
            var direction = enemy.IsFacingLeft ? -1 : 1;
            enemy.RbController.UpdateXSpeed(movementSO.maxSpeed * direction);
        }

        public void SetCurrentMovementType(MovementType type)
        {
            currentMovementType = type;
            AdjustMovementFunction();
        }
    }
}
