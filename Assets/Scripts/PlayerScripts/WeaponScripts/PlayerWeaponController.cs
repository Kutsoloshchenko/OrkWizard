using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OrkWizard
{
    public class PlayerWeaponController : Abilities
    {
        private bool needsToSwitchWeapons;

        private bool isAttacking;
        private bool attackHeld;

        [SerializeField]
        private GameObject[] playerWeaponsInitialObjects;

        // This would be the list of weapons that we get from gameObjects passed by unity editor
        private List<IPlayerWeapon> playerWeapons = new List<IPlayerWeapon>();

        private int currentWeaponIndex = 0;

        protected override void Initialization()
        {
            base.Initialization();

            foreach (var gameObj in playerWeaponsInitialObjects)
            {
                var weapon = gameObj.GetComponent<IPlayerWeapon>();

                if (weapon != null)
                {
                    playerWeapons.Add(weapon);
                    weapon.ResetAtack();
                }
            }

            // We dont need to store this anymore, right
            playerWeaponsInitialObjects = null;
            character.UpdateDebugWeapon(playerWeapons[currentWeaponIndex].GetAnimationName());
        }

        private void Update()
        {
            attackHeld = input.AttackBeingPressed;

            if (input.SwitchWeaponPressed() && !isAttacking)
            {
                needsToSwitchWeapons = true;
            }
        }

        private void FixedUpdate()
        {
            Attack();
            SwitchWeapons();
        }

        private void Attack()
        {
            if (attackHeld)
            {
                // need to check if we are attacking already
                if (!isAttacking)
                {
                    LaunchAtack();
                }

                // Nothing needs to be done in this case
                return;
            }
            else
            {
                // We are attacking already - we need to stop spreader weapon
                if (isAttacking && !playerWeapons[currentWeaponIndex].IsThroable())
                {
                    isAttacking = false;
                    animator.SetAttack(playerWeapons[currentWeaponIndex].GetAnimationName(), false);
                    character.CapHorizontalSpeed(character.playerScriptableObject.originalMaxSpeed);
                    playerWeapons[currentWeaponIndex].DisableNewAttacks();
                }
            }
        }

        private void LaunchAtack()
        {
            if (currentWeaponIndex >= 0 && playerWeapons[currentWeaponIndex].CanAttack())
            {
                if (playerWeapons[currentWeaponIndex].IsThroable())
                {
                    StartCoroutine(AtackCourutine());
                }
                else
                {
                    isAttacking = true;
                    character.CapHorizontalSpeed(playerWeapons[currentWeaponIndex].GetMaxHorizontalSpeed());
                    animator.SetAttack(playerWeapons[currentWeaponIndex].GetAnimationName(), true);
                    var direction = character.isFacingLeft ? Vector2.left : Vector2.right;
                    var possition = FindStartLocation(direction);
                    playerWeapons[currentWeaponIndex].Attack(possition, direction, new Vector2(rigidBody.velocity.x, 0));
                }
            }
        }

        private void SwitchWeapons()
        {
            if (needsToSwitchWeapons && playerWeapons.Count >= 2)
            {
                if (currentWeaponIndex >= playerWeapons.Count - 1)
                {
                    currentWeaponIndex = 0;
                }
                else
                {
                    currentWeaponIndex++;
                }
                // this will need to go somewhere else
                character.UpdateDebugWeapon(playerWeapons[currentWeaponIndex].GetAnimationName());

                needsToSwitchWeapons = false;
            }
        }

        private Vector2 FindStartLocation(Vector2 direction)
        {
            var weaponOffsetX = playerWeapons[currentWeaponIndex].GetWeaponOffsetX();
            var weaponOffsetY = playerWeapons[currentWeaponIndex].GetWeaponOffsetY();

            float x = gameObject.transform.position.x + (playerCollider.size.x * 0.5f + weaponOffsetX) * direction.x;
            float y = gameObject.transform.position.y + weaponOffsetY;

            return new Vector2(x, y);
        }

        private IEnumerator AtackCourutine()
        {
            isAttacking = true;
            animator.SetAttack(playerWeapons[currentWeaponIndex].GetAnimationName(), true);
            yield return new WaitForSeconds(playerWeapons[currentWeaponIndex].GetAnimationLength());
            animator.SetAttack(playerWeapons[currentWeaponIndex].GetAnimationName(), false);
            var direction = character.isFacingLeft ? Vector2.left : Vector2.right;
            var startingPossition = FindStartLocation(direction);
            playerWeapons[currentWeaponIndex].Attack(startingPossition, direction, new Vector2(rigidBody.velocity.x, 0));
            isAttacking = false;
        }
    }
}
