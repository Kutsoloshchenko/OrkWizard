using System.Collections;
using UnityEngine;

namespace OrkWizard
{
    public class EnemyHealthManager : MonoBehaviour, IDamagable
    {

        private EnemyHealthSO healthSO;

        private float currentHp;
        private bool applyDmgFromTick = false;
        private int ticksAfterStop = 0;

        private Enemy enemy;

        private void Awake()
        {
            currentHp = healthSO.maxHp;
            enemy = GetComponent<Enemy>();
        }

        public void ApplyDmg(float dmg)
        {
            ApplyDmg(dmg, Element.Physical);
        }

        public void ApplyDmg(float dmg, Element type)
        {

            if (type == healthSO.enemyElement)
            {
                // We are healing enemies with same element
            }
            else if (type == healthSO.weakElement)
            {
                // Apply critical dmg
            }
            else
            {
                // Apply regular dmg
            }
        }

        private IEnumerator TickDmgCorutine(float dmg, Element type, float tickTime)
        {
            applyDmgFromTick = true;
            while (applyDmgFromTick)
            {
                SubtractHealth(dmg);
                yield return new WaitForSeconds(tickTime);
            }

            while (ticksAfterStop > 0)
            {
                SubtractHealth(dmg);
                yield return new WaitForSeconds(tickTime);
                ticksAfterStop--;
            }
            // just in case
            ticksAfterStop = 0;
        }

        public void ApplyDmg(float dmg, Element type, float tickTime)
        {
            throw new System.NotImplementedException();
        }

        public void StopTickDmg(int ticksAfterStop = 0)
        {
            throw new System.NotImplementedException();
        }

        private void SubtractHealth(float number)
        {
            currentHp -= number;

            if (currentHp <= 0)
            {
                enemy.StateManager.ChangeState(new EnemyDyingState());
            }
        }
    }
}
