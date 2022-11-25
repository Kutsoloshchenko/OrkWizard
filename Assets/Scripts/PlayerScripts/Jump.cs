using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OrkWizard
{
    public class Jump : Abilities
    {
        private float jumpCountDown;
        private float jumpTime;

        protected override void Initialization()
        {
            base.Initialization();
            jumpCountDown = character.playerScriptableObject.buttonHoldTime;
        }

        private void Update()
        {
            CheckForJump();
        }

        private void FixedUpdate()
        {
            ApplyJump();
            GroundCheck();
        }

        private void CheckForJump()
        {
            if (character.Input.JumpPressed())
            {
                // no air jumps
                if (!character.isGrounded)
                {
                    character.isJumping = false;
                    return;
                }

                character.UpdateYSpeed(0);
                jumpCountDown = character.playerScriptableObject.timeToJump;
                jumpTime = 0;
                character.isJumping = true;
            }
        }

        private void ApplyJump()
        {
            if (character.isJumping)
            {
                // Jump countdown 
                jumpTime += Time.deltaTime;
                var speed = character.GetCurrentSpeed();
                var multiplier = Mathf.Abs(speed.x) >= character.playerScriptableObject.scatingSpeed ? 1 : 0.3f;
                //Apply initial speed
                if (speed.y == 0)
                {
                    character.UpdateYSpeed(character.playerScriptableObject.initialJumpSpeed * multiplier);
                }

                // Extend jumping time
                if (character.Input.JumpBeingPressed && (jumpTime < jumpCountDown + character.playerScriptableObject.buttonHoldTime))
                {
                    jumpCountDown += Time.deltaTime;

                    // Add additionall speed to jump

                    var currentSpeed = (character.playerScriptableObject.maxJumpSpeed / character.playerScriptableObject.buttonHoldTime) * jumpTime;
                    currentSpeed = Mathf.Clamp(currentSpeed, character.playerScriptableObject.initialJumpSpeed, character.playerScriptableObject.maxJumpSpeed);

                    character.UpdateYSpeed(currentSpeed);
                }
            }

            // Handle jump cancelation
            if (jumpTime > 0)
            {
                jumpCountDown -= Time.deltaTime;
                if (jumpCountDown <= 0)
                {
                    jumpCountDown = 0;
                    jumpTime = 0;
                    character.isJumping = false;
                    character.UpdateYSpeed(0);
                }
            }
        }

        private void GroundCheck()
        {
            if (character.CheckGroundRayCast() && !character.isJumping)
            {
                character.isGrounded = true;
            }
            else
            {
                if (character.Falling(0))
                {
                    var ySpeed = character.GetCurrentSpeed().y;
                    var newVelocity = ySpeed + Physics2D.gravity.y * 1.5f * Time.deltaTime;
                    newVelocity = ySpeed > character.playerScriptableObject.maxFallSpeed ? character.playerScriptableObject.maxFallSpeed : ySpeed;
                    character.UpdateYSpeed(newVelocity);
                }
                character.isGrounded = false;
            }
        }
    }
}
