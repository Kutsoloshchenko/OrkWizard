using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OrkWizard
{
    public class InputDetection : MonoBehaviour
    {
        private const string _horizontalAxis = "Horizontal";
        protected InputMapper mapper = new InputMapper();


        public bool WalkingHeld()
        {
            return Input.GetKey(mapper.WalkKeyCode);
        }

        public bool ManualHeld()
        {
            return Input.GetKey(mapper.ManualKeyCode);
        }

        public float GetHorizontalMovement()
        {
            return Input.GetAxis(_horizontalAxis);
        }

        public bool JumpHeld()
        {
            return Input.GetKey(mapper.JumpKeyCode);
        }

        public bool JumpPressed()
        {
            return Input.GetKeyDown(mapper.JumpKeyCode);
        }

        public bool DownPressed()
        {
            return Input.GetKeyDown(mapper.Down);
        }

        public bool RestartPressed()
        {
            return Input.GetKeyDown(mapper.Restart);
        }

    }
}


