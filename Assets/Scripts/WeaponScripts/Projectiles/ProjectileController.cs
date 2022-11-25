using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OrkWizard
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class ProjectileController : BaseProjectile
    {
        private Rigidbody2D rb;
        private bool selfDestruct = true;

        protected override void Initialize()
        {
            base.Initialize();
            rb = GetComponent<Rigidbody2D>();
            Invoke("SelfDestruct", projectileSO.lifeTime);
        }

        public void ApplyInitialForce(Vector2 direction, Vector2 initialSpeed)
        {

            rb.velocity = initialSpeed;
            
            rb.gravityScale = projectileSO.gravityScale;
            rb.AddForce(direction * projectileSO.force, ForceMode2D.Impulse);
            rb.AddTorque(projectileSO.torque);

        }

        private void SelfDestruct()
        {
            if (selfDestruct)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            selfDestruct = false;

            if (projectileSO.impactDmg != 0 && (projectileSO.dmgTag == _allTag || collision.CompareTag(projectileSO.dmgTag)))
            {
                var dmgApplier = collision.gameObject.GetComponent<IDamagable>();

                if (dmgApplier != null)
                {
                    dmgApplier.ApplyDmg(projectileSO.impactDmg, DamageType.Physical);
                }
            }

            if (projectileSO.explosionRadius != 0)
            {
                StartCoroutine(BlowUp());
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private IEnumerator BlowUp()
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
            rb.isKinematic = true;

            transform.rotation = Quaternion.identity;

            ChangeAnimation(projectileSO.name + _explosionPostfix);

            yield return new WaitForSeconds(projectileSO.explosionAnimationClip.length);

            var hitByBlownUp = Physics2D.CircleCastAll(transform.position, projectileSO.explosionRadius, Vector2.up);

            foreach (var hit in hitByBlownUp)
            {

                if (projectileSO.dmgTag == _allTag || hit.collider.gameObject.CompareTag(projectileSO.dmgTag))
                {
                    var dmgApplier = hit.collider.gameObject.GetComponent<IDamagable>();

                    if (dmgApplier != null)
                    {
                        dmgApplier.ApplyDmg(projectileSO.dmgValue, projectileSO.damageType);
                    }
                }
            }

            Destroy(gameObject);
        }
    }
}
