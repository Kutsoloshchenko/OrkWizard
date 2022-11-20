using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OrkWizard
{
    public class AnimatorController : AnimatorControllerBase
    {
        private const string _playerIdle = "Idle";
        private const string _movingPostFix = "_moving";

        // Ground movement animations
        private const string _manual = "Manual";
        private const string _powerSlide = "PowerSlide";
        private const string _walking = "Walk";
        private const string _skating = "Skate";

        // Air based animations
        private const string _wallTouch = "WallTouch";
        private const string _platfomrHang = "PlatformHang";
        private const string _platformClimb = "PlatformClimb";
        private const string _ollie = "Ollie";
        private const string _fall = "Fall";

        private bool moving;
        private bool grounded;
        private bool wallTouch;
        private bool platformHang;
        private bool platformClimb;
        private bool manual;
        private bool powerSlide;
        private bool attacking;

        private float horizontalSpeed;
        private float verticalSpeed;

        private string currentAttack;

        [SerializeField]
        private PlayerSO playerSO;

        protected override void Initialize()
        {
            base.Initialize();
            ChangeState(_playerIdle);
        }

        private void ChangeAnimation()
        {
            // We want attacking to be the higest priority

            if (attacking)
            {
                var postFix = moving ? _movingPostFix : string.Empty;
                ChangeState(currentAttack + postFix);
                return;
            }

            if (grounded)
            {
                GroundedAnimations();
                return;
            }
            else
            {
                JumpingAnimations();
            }
        }

        private void GroundedAnimations()
        {
            if (!moving)
            {
                ChangeState(_playerIdle);
            }
            else
            {
                if (powerSlide)
                {
                    ChangeState(_powerSlide);
                    return;
                }
                else if (manual)
                {
                    ChangeState(_manual);
                    return;
                }

                // Regular walk, lets check speeds
                if (Math.Abs(horizontalSpeed) <= playerSO.walkSpeed)
                {
                    ChangeState(_walking);
                }
                else
                {
                    ChangeState(_skating);
                }
            }
        }

        private void JumpingAnimations()
        {

            // A bit not pretty, but it works out in a small solution like this
            if (wallTouch)
            {
                ChangeState(_wallTouch);
                return;
            }
            else if (platformClimb)
            {
                ChangeState(_platformClimb);
                return;
            }
            else if (platformHang)
            {
                ChangeState(_platfomrHang);
                return;
            }

            if (verticalSpeed > 0)
            {
                ChangeState(_ollie);
            }
            else
            {
                ChangeState(_fall);
            }
        }

        public void SetMoving(bool value)
        {
            moving = value;
            ChangeAnimation();
        }

        public void SetHorizontalSpeedValue(float speed)
        {
            horizontalSpeed = speed;
            ChangeAnimation();
        }

        public void SetGrounded(bool value)
        {
            grounded = value;
            ChangeAnimation();
            //animator.SetBool(_groundedStatus, value);
        }

        public void SetVerticalSpeedValue(float speed)
        {
            verticalSpeed = speed;
            ChangeAnimation();
            //animator.SetFloat(_currentVerticalSpeedValue, speed);
        }

        public void SetWallTouch(bool value)
        {
            wallTouch = value;
            ChangeAnimation();
            //animator.SetBool(_wallTouching, value);
        }

        public void SetPlatformHang(bool value)
        {
            platformHang = value;
            ChangeAnimation();
            //animator.SetBool(_platformHang, value);
        }

        public void SetPlatformClimb(bool value)
        {
            platformClimb = value;
            ChangeAnimation();
            //animator.SetBool(_platformClimb, value);
        }

        public void SetManual(bool value)
        {
            manual = value;
            ChangeAnimation();
            //animator.SetBool(_manual, value);
        }

        public void SetPowerSlide(bool value)
        {
            powerSlide = value;
            ChangeAnimation();
            //animator.SetBool(_powerSlide, value);
        }

        internal void SetAttack(string attackName, bool value)
        {
            attacking = value;
            currentAttack = value ? attackName : string.Empty;
            ChangeAnimation();
            //animator.SetBool(_attacking, value);
            //animator.SetBool(attackName, value);
        }


    }
}