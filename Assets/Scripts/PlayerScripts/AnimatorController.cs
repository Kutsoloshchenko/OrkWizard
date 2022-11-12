using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OrkWizard
{
    public class AnimatorController : MonoBehaviour
    {
        private const string _movingBool = "Moving";
        private const string _currentHorizontalSpeedValue = "HorizontalSpeed";
        private const string _currentVerticalSpeedValue = "VerticalSpeed";
        private const string _groundedStatus = "Grounded";
        private const string _wallTouching = "TouchingWall";
        private const string _platformHang = "PlatformHang";
        private const string _platformClimb = "PlatformClimb";
        private const string _manual = "Manual";
        private const string _powerSlide = "PowerSlide";
        protected Animator animator;

        void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void SetHorizontalAnimation(bool value)
        {
            animator.SetBool(_movingBool, value);
        }

        public void SetHorizontalSpeedValue(float speed)
        {
            animator.SetFloat(_currentHorizontalSpeedValue, speed);
        }

        public void SetGrounded(bool value)
        {
            animator.SetBool(_groundedStatus, value);
        }

        public void SetVerticalSpeedValue(float speed)
        {
            animator.SetFloat(_currentVerticalSpeedValue, speed);
        }

        public void SetWallTouch(bool value)
        {
            animator.SetBool(_wallTouching, value);
        }

        public void SetPlatformHang(bool value)
        {
            animator.SetBool(_platformHang, value);
        }

        public void SetPlatformClimb(bool value)
        {
            animator.SetBool(_platformClimb, value);
        }

        public void SetManual(bool value)
        {
            animator.SetBool(_manual, value);
        }

        public void SetPowerSlide(bool value)
        {
            animator.SetBool(_powerSlide, value);
        }
    }
}