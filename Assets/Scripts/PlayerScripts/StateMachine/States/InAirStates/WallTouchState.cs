using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OrkWizard
{
    public class WallTouchState : InAirSuperState, IState
    {

        private const string _wallTouch = "WallTouch";
        private float wallJumpCountDown = 0;
        private bool needsToWallJump = false;

        public override void OnEnter(BaseStateManager stateManager)
        {
            stateManager.Character.Animator.ChangeAnimation(_wallTouch);
        }

        public override void OnExit(BaseStateManager stateManager)
        {
            stateManager.Character.SetHorizontalMovement(true);
            return;
        }

        public override void OnUpdate(BaseStateManager stateManager)
        {
            base.OnUpdate(stateManager);

            if (stateManager.Character.Input.JumpPressed() && wallJumpCountDown <= 0)
            {
                needsToWallJump = true;
                wallJumpCountDown = stateManager.Character.playerScriptableObject.wallJumpTime;
            }
        }

        public override void OnFixedUpdate(BaseStateManager stateManager)
        {
            base.OnFixedUpdate(stateManager);

            if (needsToWallJump || wallJumpCountDown > 0)
            {
                PerformWallKick(stateManager);
                return;
            }

            if (!stateManager.Character.WallCheck())
            {
                stateManager.ChangeState(new InAirState());
            }
            else
            {
                stateManager.Character.Animator.ChangeAnimation(_wallTouch);
            }
        }

        private void PerformWallKick(BaseStateManager stateManager)
        {
            if (needsToWallJump)
            {
                stateManager.Character.Animator.ChangeAnimation(_ollie);

                stateManager.Character.Flip();
                var direction = stateManager.Character.IsFacingLeft ? Vector2.left.x : Vector2.right.x;
                stateManager.Character.rbController.UpdateSpeed(direction * stateManager.Character.playerScriptableObject.maxSpeed, stateManager.Character.playerScriptableObject.maxJumpSpeed);
                stateManager.Character.SetHorizontalMovement(false);

                needsToWallJump = false;
            }

            if (wallJumpCountDown > 0)
            {
                wallJumpCountDown -= Time.deltaTime;

                if (wallJumpCountDown <= 0)
                {
                    stateManager.Character.rbController.UpdateYSpeed(0);
                    stateManager.Character.SetHorizontalMovement(true);
                    wallJumpCountDown = 0;
                }
            }
        }


    }
}
