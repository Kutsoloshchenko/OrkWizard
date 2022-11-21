using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OrkWizard
{
    public class Jump : Abilities
    {
        private float wallJumpCountDown;
        private float jumpCountDown;
        private float jumpTime;

        private bool performWallKick;

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
            PerformWallKick();
            GroundCheck();
        }

        private void CheckForJump()
        {
            if (input.JumpPressed())
            {
                if (!character.isGrounded && character.WallCheck())
                {
                    // Performing Wall kick 
                    performWallKick = true;
                    wallJumpCountDown = character.playerScriptableObject.wallJumpTime;
                    return;
                }

                // no air jumps
                if (!character.isGrounded)
                {
                    character.isJumping = false;
                    return;
                }

                rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
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
                var multiplier = Mathf.Abs(rigidBody.velocity.x) >= character.playerScriptableObject.scatingSpeed ? 1 : 0.3f;
                //Apply initial speed
                if (rigidBody.velocity.y == 0)
                {
                    rigidBody.velocity = new Vector2(rigidBody.velocity.x, character.playerScriptableObject.initialJumpSpeed * multiplier);
                }

                // Extend jumping time
                if (input.JumpBeingPressed && (jumpTime < jumpCountDown + character.playerScriptableObject.buttonHoldTime))
                {
                    jumpCountDown += Time.deltaTime;

                    // Add additionall speed to jump

                    var currentSpeed = (character.playerScriptableObject.maxJumpSpeed / character.playerScriptableObject.buttonHoldTime) * jumpTime;
                    currentSpeed = Mathf.Clamp(currentSpeed, character.playerScriptableObject.initialJumpSpeed, character.playerScriptableObject.maxJumpSpeed);

                    rigidBody.velocity = new Vector2(rigidBody.velocity.x, currentSpeed);
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
                    rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
                }
            }
        }

        private void PerformWallKick()
        {
            if (performWallKick)
            {
                animator.SetWallTouch(false);
                character.Flip();
                var direction = character.isFacingLeft ? Vector2.left.x : Vector2.right.x;
                character.SetHorizontalMovement(false);
                rigidBody.velocity = new Vector2(direction * character.playerScriptableObject.maxSpeed, character.playerScriptableObject.maxJumpSpeed);
                performWallKick = false;
            }

            if (wallJumpCountDown > 0)
            {
                wallJumpCountDown -= Time.deltaTime;

                if (wallJumpCountDown <= 0)
                {
                    character.SetHorizontalMovement(true);
                    rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
                }
            }
        }

        private void GroundCheck()
        {
            if (character.CheckGroundRayCast() && !character.isJumping)
            {
                character.isGrounded = true;
                animator.SetGrounded(true);
            }
            else
            {
                if (character.Falling(0))
                {
                    var newVelocity = rigidBody.velocity.y + Physics2D.gravity.y * 1.5f * Time.deltaTime;
                    newVelocity = rigidBody.velocity.y > character.playerScriptableObject.maxFallSpeed ? character.playerScriptableObject.maxFallSpeed : rigidBody.velocity.y;

                    rigidBody.velocity = new Vector2(rigidBody.velocity.x, newVelocity);
                }
                character.isGrounded = false;
                animator.SetGrounded(false);
            }

            character.UpdateDebugSpeed(rigidBody.velocity.y, true);
            animator.SetVerticalSpeedValue(rigidBody.velocity.y);
        }
    }
}
