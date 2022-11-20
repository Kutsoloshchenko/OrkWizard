using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OrkWizard 
{
    public class SpreadWeapon : WeaponBase, IPlayerWeapon
    {

        [SerializeField]
        private GameObject projectile;

        private GameObject projectileObject;

        protected override void Initialize()
        {
            base.Initialize();
        }

        public void Attack(Vector2 originalPossition, Vector2 direction, Vector2 initialSpeed)
        {
            if (projectileObject == null)
            {
                projectileObject = Instantiate(projectile, originalPossition, Quaternion.identity);
                projectileObject.transform.localScale = new Vector2(direction.x, projectileObject.transform.localScale.y);
                var playerGameObj = GameObject.FindGameObjectWithTag(_playerTag);
                projectileObject.transform.parent = playerGameObj.transform;
            }
        }

        public override void DisableNewAttacks()
        {
            if (projectileObject != null)
            {
                Destroy(projectileObject);
            }
        }
    }
}
