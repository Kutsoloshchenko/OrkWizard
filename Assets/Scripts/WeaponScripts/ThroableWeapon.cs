using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OrkWizard
{
    public class ThroableWeapon : WeaponBase, IPlayerWeapon
    {
        [SerializeField]
        private GameObject projectile;

        protected override void Initialize()
        {
            base.Initialize();
        }

        //public void Attack()
        //{
        //    canAttack = false;
        //    Invoke("ResetAtack", weaponSO.coolDownTime);
        //}

        //public void Attack(GameObject player)
        //{
        //    canAttack = false;
        //    Invoke("ResetAtack", weaponSO.coolDownTime);
        //}

        public void Attack(Vector2 projectileStartingPoint, Vector2 direction, Vector2 initialSpeed)
        {
            canAttack = false;
            var projectileObject = Instantiate(projectile, projectileStartingPoint, Quaternion.identity);
            var projController = projectileObject.GetComponent<ProjectileController>();
            if (projController != null)
            {
                projController.ApplyInitialForce(direction, initialSpeed);
            }
            Invoke("ResetAtack", weaponSO.coolDownTime);
        }
    }
}
