using UnityEngine;

namespace OrkWizard
{
    public class ThroableWeapon : WeaponBase, IWeapon
    {
        [SerializeField]
        private GameObject projectile;

        protected override void Initialize()
        {
            base.Initialize();
            var projController = projectile.GetComponent<ProjectileController>();
            weaponDistance = projController.GetProjectileMaxDistance();
        }

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
