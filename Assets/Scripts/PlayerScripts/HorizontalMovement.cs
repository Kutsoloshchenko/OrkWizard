using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OrkWizard
{
    public class HorizontalMovement : Abilities
    {
        private float currentSpeed;
        private float runTime;

        private void OnDisable()
        {
            currentSpeed = 0;
            runTime = 0;
            animator.SetHorizontalSpeedValue(currentSpeed);
            animator.SetMoving(false);
        }

        protected override void Initialization()
        {
            base.Initialization();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            var direction = character.isFacingLeft ? -1 : 1;
            if (character.PowerSliding())
            {
                PowerSlide(direction);
            }
            else
            {
                animator.SetPowerSlide(false);
                MoveVertically(direction);
            }

            if (character.WallCheck() || character.PlatformSideCheck())
            {
                currentSpeed = 0.1f * direction;
            }


            character.UpdateDebugSpeed(currentSpeed, false);
            rigidBody.velocity = new Vector2(currentSpeed, rigidBody.velocity.y);
            animator.SetHorizontalSpeedValue(currentSpeed);
        }

        private void PowerSlide(int direction)
        {
            currentSpeed =  character.playerScriptableObject.maxSpeed * 1.5f * direction;
            animator.SetPowerSlide(true);
        }

        private void MoveVertically(int direction)
        {
            var horizontalMovement = input.HorizontalInput.x;

            if (horizontalMovement != 0 && Mathf.Abs(currentSpeed) <= character.playerScriptableObject.maxSpeed)
            {
                CalculateSpeed(horizontalMovement);
                CheckDirection();
                animator.SetMoving(true);
            }
            else
            {
                CalculateDrag(direction);
            }
        }

        private void CalculateSpeed(float horizontalMovement)
        {
            runTime += Time.deltaTime;
            currentSpeed = (character.playerScriptableObject.maxSpeed / character.playerScriptableObject.timeTillMaxSpeed) * runTime;

            if (currentSpeed > character.playerScriptableObject.maxSpeed)
            {
                currentSpeed = character.playerScriptableObject.maxSpeed;
            }

            ApplySpeedMultiplier();
            currentSpeed *= horizontalMovement;
        }

        private void CalculateDrag(int direction)
        {
            if (currentSpeed != 0)
            {
                currentSpeed = Mathf.Abs(currentSpeed);
                if (currentSpeed > 0)
                {
                    currentSpeed -= Time.deltaTime * character.playerScriptableObject.dragMultiplier;
                    if (currentSpeed < 0)
                    {
                        currentSpeed = 0;
                        animator.SetMoving(false);
                        runTime = 0;
                        return;
                    }

                    currentSpeed *= direction;
                }
            } 
        }

        private void CheckDirection()
        {
            if ((currentSpeed < 0 && !character.isFacingLeft) ||
                 currentSpeed > 0 && character.isFacingLeft)
            {
                runTime = character.playerScriptableObject.timeTillMaxSpeed / 2;
                character.Flip();
            }
        }

        private void ApplySpeedMultiplier()
        {
            if (input.Walk)
            {
                currentSpeed = character.playerScriptableObject.walkSpeed;
                return;
            }


            if (input.Manual)
            {
                currentSpeed *= character.playerScriptableObject.manualSpeedMultiplier;
                animator.SetManual(true);
            }
            else
            {
                animator.SetManual(false);
            }
        }
    }


}
