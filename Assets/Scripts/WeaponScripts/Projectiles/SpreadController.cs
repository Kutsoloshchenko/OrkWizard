using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OrkWizard
{
    public class SpreadController : BaseProjectile
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (projectileSO.dmgTag == _allTag || collision.CompareTag(projectileSO.dmgTag))
            {
                var dmgAplier = collision.GetComponent<IDamagable>();

                if (dmgAplier != null)
                {
                    dmgAplier.ApplyDmg(projectileSO.dmgValue, projectileSO.damageType, projectileSO.lifeTime);
                }
            
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (projectileSO.dmgTag == _allTag || collision.CompareTag(projectileSO.dmgTag))
            {
                var dmgAplier = collision.GetComponent<IDamagable>();

                if (dmgAplier != null)
                {
                    dmgAplier.StopTickDmg();
                }
            }
        }
    }
}
