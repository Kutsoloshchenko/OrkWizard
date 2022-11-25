using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OrkWizard
{
    public class PlatformHangState : InAirSuperState, IState
    {
        private const string _platfomrHang = "PlatformHang";
        private const string _platformClimb = "PlatformClimb";

        private bool performPlatformClimb;
        private bool performDisengage;

        private IHange currentHangTarget;

        public override void OnEnter(StateManager stateManager)
        {
            var hit = stateManager.Character.PlatformSideCheck();
            currentHangTarget = hit.collider.GetComponent<IHange>();

            if (currentHangTarget != null)
            {
                var sign = stateManager.Character.isFacingLeft ? -1 : 1;
                var colliderSize = stateManager.Character.GetColliderSize();
                var platformHangPos = currentHangTarget.GetHangPossition(stateManager.Character.isFacingLeft);
                stateManager.Character.transform.position = new Vector2(platformHangPos.x + ((colliderSize.x / 2) * sign), platformHangPos.y - (colliderSize.y / 4));

                stateManager.Character.SetGravity(0f);
                stateManager.Character.UpdateYSpeed(0);
                stateManager.Character.UpdateXSpeed(0);
                stateManager.Character.Animator.ChangeAnimation(_platfomrHang);
                stateManager.Character.SetVerticalMovement(false);
                stateManager.Character.SetHorizontalMovement(false);
                stateManager.Character.SetWeaponControls(false);
            }
        }

        public override void OnExit(StateManager stateManager)
        {
            stateManager.Character.SetGravity(1);
            stateManager.Character.SetVerticalMovement(true);
            stateManager.Character.SetHorizontalMovement(true);
            stateManager.Character.SetWeaponControls(true);
        }

        public override void OnUpdate(StateManager stateManager)
        {
            // Bassically need to check if they try to get up, or disingage 
            if (stateManager.Character.Input.JumpPressed())
            {
                performPlatformClimb = true;
            }
            else if (stateManager.Character.Input.DownPressed())
            {
                performDisengage = true;
            }
        }

        public override void OnFixedUpdate(StateManager stateManager)
        {
            PerformPlatformClimb(stateManager);
            PerformPlatformDisengage(stateManager);
        }

        private void PerformPlatformClimb(StateManager stateManager)
        {
            if (currentHangTarget != null && performPlatformClimb)
            {
                performPlatformClimb = false;

                var sign = stateManager.Character.isFacingLeft ? -1 : 1;
                var destination = currentHangTarget.GetHangPossition(stateManager.Character.isFacingLeft);
                var colliderSize = stateManager.Character.GetColliderSize();
                destination = new Vector2(destination.x + ((colliderSize.x / 2) * sign), destination.y + (colliderSize.y / 2));
                stateManager.StartCoroutine(ClimbLedge(stateManager, destination));
            }
        }

        private void PerformPlatformDisengage(StateManager stateManager)
        {
            if (currentHangTarget != null && performDisengage)
            {
                performDisengage = false;
                stateManager.Character.Animator.ChangeAnimation(_fall);
                currentHangTarget = null;
                stateManager.StartCoroutine(SwitchToInAir(stateManager));
            }
        }

        private IEnumerator ClimbLedge(StateManager stateManager, Vector2 destination)
        {
            float time = 0;
            Vector2 startingPoint = stateManager.Character.transform.position;

            stateManager.Character.Animator.ChangeAnimation(_platformClimb);
            while (time < stateManager.Character.playerScriptableObject.platformClimbTime)
            {
                stateManager.Character.transform.position = Vector2.Lerp(startingPoint, destination, time / stateManager.Character.playerScriptableObject.platformClimbTime);
                time += Time.deltaTime;
                yield return null;
            }

            stateManager.ChangeState(stateManager.IdleState);
        }

        private IEnumerator SwitchToInAir(StateManager stateManager)
        {
            yield return new WaitForSeconds(stateManager.Character.playerScriptableObject.platformHangTimeOut);
            stateManager.ChangeState(stateManager.InAirState);
        }
    }
}
