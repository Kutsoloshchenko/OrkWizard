using System.Collections;
using UnityEngine;
using UnityEditor;

namespace OrkWizard
{
    public class EnemyHealthManager : BaseHealthManager, IDamagable
    {
        [SerializeField]
        private EnemyHealthSO healthSO;

        private float currentHp;

        private Enemy enemy;

        protected override void Awake()
        {
            base.Awake();

            currentHp = healthSO.maxHp;
            enemy = GetComponent<Enemy>();
        }

        public override void ApplyDmg(float dmg, Element type)
        {

            if (type == healthSO.enemyElement)
            {
                // We are healing enemy if they are the same element 
                if (currentHp != healthSO.maxHp)
                {
                    currentHp = currentHp + dmg > healthSO.maxHp ? healthSO.maxHp : currentHp + dmg;
                }
            }
            else if (type == healthSO.weakElement) // Regular hit if its being critically hit already at this point
            {
                // Apply critical dmg
                // Knockback + bigger dmg + imune to critical hit for short while
                
                // always bigger dmg
                SubtractHealth(dmg * criticalHitMultipliler);

                if (!enemy.IsCriticallyHit && healthSO.canBeCritHit)
                {
                    enemy.StateManager.ChangeState(new EnemyCriticallyHit());
                }
            }
            else
            {
                SubtractHealth(dmg);
            }
        }

        public override void ApplyDmg(float dmg, Element type, float tickTime)
        {
            // apply tick dmg;
            if (!applyDmgFromTick && ticksAfterStop == 0)
            {
                StartCoroutine(TickDmgCorutine(dmg, type, tickTime));
                // StartCoroutine(TintFlickCorutine()); - we gonna implement it later
            }
        }

        protected override void SubtractHealth(float dmgValue)
        {
            TintOnDmgRecived();
            currentHp -= dmgValue;

            if (currentHp <= 0)
            {
                enemy.StateManager.ChangeState(new EnemyDyingState());
            }
        }

        private void TintOnDmgRecived()
        {
            SwapColor();
            Invoke("SwapColor", 1f);
        }

        //private void OnDrawGizmos()
        //{
        //    GUIStyle style = new GUIStyle();
        //    style.fontSize = 96;
        //    style.normal.textColor = Color.red;
        //    UnityEditor.Handles.Label(new Vector3(transform.position.x, transform.position.y + 7, transform.position.z), currentHp.ToString(), style);

        //}
    }
}
