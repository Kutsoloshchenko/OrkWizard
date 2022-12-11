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

        [SerializeField]
        private GameObject[] playerWeaponsInitialObjects;

        // This would be the list of weapons that we get from gameObjects passed by unity editor
        private List<IWeapon> playerWeapons = new List<IWeapon>();
        private int currentWeaponIndex = 0;

        public IWeapon CurrentWeapon { get { return playerWeapons[currentWeaponIndex]; } }

        protected override void Initialization()
        {
            base.Initialization();

            foreach (var gameObj in playerWeaponsInitialObjects)
            {
                var weapon = gameObj.GetComponent<IWeapon>();

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
            if (character.Input.SwitchWeaponPressed() && !isAttacking)
            {
                needsToSwitchWeapons = true;
            }
        }

        private void FixedUpdate()
        {
            SwitchWeapons();
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

        public void SetAttacking(bool value)
        {
            isAttacking = value;
        }

    }
}
