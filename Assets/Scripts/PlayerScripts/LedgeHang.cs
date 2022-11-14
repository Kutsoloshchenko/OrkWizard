using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OrkWizard
{
    public class LedgeHang : Abilities
    {
        private bool wallHang;
        private bool performPlatformClimb;
        private bool performDisengage;

        private IHange currentHangTarget;

        protected override void Initialization()
        {
            base.Initialization();
        }

        private void Update()
        {
            CheckInput();
        }

        private void FixedUpdate()
        {
            CheckPlatformHang();
            PerformPlatformClimb();
            PerformPlatformDisengage();
        }

        private void CheckPlatformHang()
        {
            if (currentHangTarget != null)
            {
                return;
            }

            var hit = character.PlatformSideCheck();
            if (hit)
            {
                var hangTarget = hit.collider.gameObject.GetComponent<IHange>();
                if (hangTarget != null)
                {
                    currentHangTarget = hangTarget;
                    var possitionForHanging = currentHangTarget.GetHangPossition(character.isFacingLeft);
                    var sign = character.isFacingLeft ? -1 : 1;
                    character.transform.position = new Vector2(possitionForHanging.x + ((playerCollider.size.x / 2) * sign), possitionForHanging.y - (playerCollider.size.y / 4));


                    animator.SetPlatformHang(true);
                    rigidBody.velocity = Vector2.zero;
                    rigidBody.gravityScale = 0;
                    wallHang = true;
                    character.SetHorizontalMovement(false);
                    character.SetVerticalMovement(false);
                    return;
                }
            }
        }

        private void CheckInput()
        {
            if (wallHang)
            {
                if (input.JumpPressed())
                {
                    performPlatformClimb = true;
                    return;
                }
                else if (input.DownPressed())
                {
                    performDisengage = true;
                }

            }
        }

        private void PerformPlatformClimb()
        {
            if (performPlatformClimb)
            {
                animator.SetPlatformHang(false);
                performPlatformClimb = false;

                var sign = character.isFacingLeft ? -1 : 1;
                var destination = currentHangTarget.GetHangPossition(character.isFacingLeft);
                destination = new Vector2(destination.x + ((playerCollider.size.x / 2) * sign), destination.y + (playerCollider.size.y / 2));
                StartCoroutine(ClimbLedge(destination));
            }
        }

        private void PerformPlatformDisengage()
        {
            if (performDisengage)
            {
                animator.SetPlatformHang(false);
                CleanUp();
                performDisengage = false;
                Invoke("EnableHang", character.playerScriptableObject.platformHangTimeOut);
            }
        }

        private IEnumerator ClimbLedge(Vector2 destination)
        {
            float time = 0;

            Vector2 startingPoint = transform.position;
            animator.SetPlatformClimb(true);
            while (time < character.playerScriptableObject.platformClimbTime)
            {
                transform.position = Vector2.Lerp(startingPoint, destination, time / character.playerScriptableObject.platformClimbTime);
                time += Time.deltaTime;
                yield return null;
            }

            CleanUp();
            EnableHang();
            animator.SetPlatformClimb(false);
        }

        private void CleanUp()
        {
            wallHang = false;
            character.SetHorizontalMovement(true);
            character.SetVerticalMovement(true);
            rigidBody.gravityScale = 1;
        }

        private void EnableHang()
        {
            currentHangTarget = null;
        }
    }
}