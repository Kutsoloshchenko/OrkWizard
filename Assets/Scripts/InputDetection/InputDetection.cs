using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace OrkWizard
{
    public class InputDetection : MonoBehaviour
    {
        // HorizontalInputs
        public Vector2 HorizontalInput { get; private set; }


        public bool Manual { get; private set; }
        public bool Walk { get; private set; }
        public bool JumpBeingPressed { get; private set; }
        public bool AttackBeingPressed { get; private set; }

        private const string _weaponPressed = "ChangeWeapon";
        private const string _downPressed = "Down";
        private const string _jump = "Jump";

        private PlayerInput playerInput;
        protected InputMapper mapper = new InputMapper();

        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
        }

        public void OnHorizontalAxisChange(InputAction.CallbackContext context)
        {
            if (!context.canceled)
            {
                HorizontalInput = context.ReadValue<Vector2>();
            }
            else
            {
                HorizontalInput = Vector2.zero;
            }
        }

        public void OnWalkingHeld(InputAction.CallbackContext context)
        {
            if (context.started || context.performed)
            {
                Walk = true;
            }
            else
            {
                Walk = false;
            }

        }

        public void OnManualHeld(InputAction.CallbackContext context)
        {
            if (context.started || context.performed)
            {
                Manual = true;
            }
            else
            {
                Manual = false;
            }

        }

        public void OnJumpHeld(InputAction.CallbackContext context)
        {
            if (context.started || context.performed)
            {
                JumpBeingPressed = true;
            }
            else
            {
                JumpBeingPressed = false;
            }
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.started || context.performed)
            {
                AttackBeingPressed = true;
            }
            else
            {
                AttackBeingPressed = false;
            }
        }

        public void OnWeaponSwitch(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                AttackBeingPressed = true;
            }

        }

        //public bool WalkingHeld()
        //{
        //    return Input.GetKey(mapper.WalkKeyCode);
        //}

        //public bool ManualHeld()
        //{
        //    return Input.GetKey(mapper.ManualKeyCode);
        //}

        //public float GetHorizontalMovement()
        //{
        //    return Input.GetAxis(_horizontalAxis);
        //}

        //public bool JumpHeld()
        //{
        //    return Input.GetKey(mapper.JumpKeyCode);
        //}

        //internal bool AttackHeld()
        //{
        //    return Input.GetKey(mapper.AttackKeyCode);
        //}

        //public bool JumpPressed()
        //{
        //    return Input.GetKeyDown(mapper.JumpKeyCode);
        //}

        public bool SwitchWeaponPressed()
        {
            return playerInput.currentActionMap.FindAction(_weaponPressed).WasPressedThisFrame();
        }

        public bool JumpPressed()
        {
            return playerInput.currentActionMap.FindAction(_jump).WasPressedThisFrame();
        }

        public bool DownPressed()
        {
            return playerInput.currentActionMap.FindAction(_downPressed).WasPressedThisFrame();
        }

        public bool RestartPressed()
        {
            return playerInput.currentActionMap.FindAction("Restart").WasPressedThisFrame();
        }

    }
}


