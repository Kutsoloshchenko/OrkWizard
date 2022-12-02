using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OrkWizard
{
    public class PowerSlideState : GroundedSuperState, IState
    {
        private const string _powerSlide = "PowerSlide";

        public override void OnEnter(StateManager stateManager)
        {
            stateManager.Character.Animator.ChangeAnimation(_powerSlide);
            stateManager.Character.SetHorizontalMovement(false);
            stateManager.Character.SetWeaponControls(false);
        }

        public override void OnExit(StateManager stateManager)
        {
            stateManager.Character.SetHorizontalMovement(true);
            stateManager.Character.SetWeaponControls(true);
        }

        public override void OnUpdate(StateManager stateManager)
        {
            // we dont need to check the input for attack because attaking is not allowed on powerslide
            return;
        }

        public override void OnFixedUpdate(StateManager stateManager)
        {
            base.OnFixedUpdate(stateManager);

            // If we are still on the ground by the end of powerslide, we just transition to regular movement
            if (!stateManager.Character.IsPowerSliding)
            {
                stateManager.ChangeState(stateManager.MovingState);
            }

            PowerSlide(stateManager);
        }

        private void PowerSlide(StateManager stateManager)
        {
            var direction = stateManager.Character.IsFacingLeft ? -1 : 1;
            var currentSpeed = stateManager.Character.playerScriptableObject.maxSpeed * stateManager.Character.playerScriptableObject.powerSlideSpeedMultiplier * direction;
            stateManager.Character.rbController.UpdateXSpeed(currentSpeed);
        }
    }
}
