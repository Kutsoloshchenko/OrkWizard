using System.Collections.Generic;
using UnityEngine;

namespace OrkWizard
{
    public class EnemyWeaponController
    {
        private WeaponSO[] weapons;
        private Enemy enemy;

        public EnemyWeaponController(Enemy enemyObs, WeaponSO[] availibleWeapons)
        {
            weapons = availibleWeapons;
            enemy = enemyObs;
        }

        public void Attack()
        {
            
        }



    }
}
