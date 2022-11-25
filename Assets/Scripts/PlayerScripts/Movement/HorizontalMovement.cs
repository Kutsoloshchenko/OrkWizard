using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OrkWizard
{
    public class HorizontalMovement : Abilities
    {
        public float CurrentSpeed { get; private set; }
        private float runTime;

        private void OnDisable()
        {
            CurrentSpeed = 0;
            runTime = 0;
        }

        private void OnEnable()
        {
            if (character.rbController != null)
            {
                CurrentSpeed = character.rbController.GetCurrentSpeed().x;
            }
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
            var direction = character.IsFacingLeft ? -1 : 1;
            MoveVertically(direction);

            if (character.WallCheck() || character.PlatformSideCheck())
            {
                CurrentSpeed = 0.1f * direction;
            }

            character.rbController.UpdateXSpeed(CurrentSpeed);
        }

        private void MoveVertically(int direction)
        {
            var horizontalMovement = character.Input.HorizontalInput.x;

            if (horizontalMovement != 0 && Mathf.Abs(CurrentSpeed) <= character.playerScriptableObject.maxSpeed)
            {
                CalculateSpeed(horizontalMovement);
                CheckDirection();
                character.Animator.SetMoving(true);
            }
            else
            {
                CalculateDrag(direction);
            }
        }

        private void CalculateSpeed(float horizontalMovement)
        {
            runTime += Time.deltaTime;
            CurrentSpeed = (character.playerScriptableObject.maxSpeed / character.playerScriptableObject.timeTillMaxSpeed) * runTime;

            if (CurrentSpeed > character.playerScriptableObject.maxSpeed)
            {
                CurrentSpeed = character.playerScriptableObject.maxSpeed;
            }

            ApplySpeedMultiplier();
            CurrentSpeed *= horizontalMovement;
        }

        private void CalculateDrag(int direction)
        {
            if (CurrentSpeed != 0)
            {
                var originalDirection = CurrentSpeed > 0 ? 1 : -1;
                CurrentSpeed = Mathf.Abs(CurrentSpeed);
                CurrentSpeed -= Time.deltaTime * character.playerScriptableObject.dragMultiplier;
                if (CurrentSpeed < 0)
                {
                    CurrentSpeed = 0;
                    character.Animator.SetMoving(false);
                    runTime = 0;
                    return;
                }

                CurrentSpeed *= originalDirection;
            }
        }

        private void CheckDirection()
        {
            if ((CurrentSpeed < 0 && !character.IsFacingLeft) ||
                 CurrentSpeed > 0 && character.IsFacingLeft)
            {
                runTime = character.playerScriptableObject.timeTillMaxSpeed / 2;
                character.Flip();
            }
        }

        private void ApplySpeedMultiplier()
        {
            if (character.Input.Walk)
            {
                CurrentSpeed = character.playerScriptableObject.walkSpeed;
                return;
            }

            //if (input.Manual)
            //{
            //    CurrentSpeed *= character.playerScriptableObject.manualSpeedMultiplier;
            //    animator.SetManual(true);
            //}
            //else
            //{
            //    animator.SetManual(false);
            //}
        }
    }


}
