using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace OrkWizard
{
    public class EnemyWeaponController
    {
        private IWeapon[] weapons;
        private Enemy enemy;

        private float attackCoolDown;
        private float currentTime;

        public EnemyWeaponController(Enemy enemyObs, IWeapon[] availibleWeapons)
        {
            weapons = availibleWeapons;
            enemy = enemyObs;
        }

        public bool CanAttack()
        {
            var weaponWithAvailibleAttack = weapons.FirstOrDefault(x => x.CanAttack());

            if (weaponWithAvailibleAttack != null)
            {
                return true;
            }

            return false;
        }

        public IWeapon FindAppropriateWeapon()
        {
            if (enemy.PlayerReference != null)
            {
                var distanceToPlayer = Vector2.Distance(enemy.transform.position, enemy.PlayerReference.transform.position);

                IWeapon weaponToAttackWith = null;

                foreach (var weapon in weapons)
                {
                    if (weapon.CanAttack() && weapon.GetWeaponDistance() < distanceToPlayer)
                    {
                        // We will revisit this logic when we have more weapons and decide how we want to proceed with this
                        weaponToAttackWith = weapon;
                        break;
                    }
                }

                return weaponToAttackWith;
            }

            return null;
        }

    }
}
