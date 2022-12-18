using System.Collections;
using UnityEngine;

namespace OrkWizard
{
    public abstract class BaseHealthManager : MonoBehaviour, IDamagable
    {

        protected bool applyDmgFromTick = false;
        protected int ticksAfterStop = 0;

        protected Color currentColor = Color.white;
        protected Color pink = new Color(1, 0.5f, 0.5f);
        protected SpriteRenderer spriteRenderer;

        protected const float criticalHitMultipliler = 1.5f;

        protected virtual void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public abstract void ApplyDmg(float dmg, Element type);
        public abstract void ApplyDmg(float dmg, Element type, float tickTime);

        public void ApplyDmg(float dmg)
        {
            ApplyDmg(dmg, Element.Physical);
        }

        public void StopTickDmg(int ticks)
        {
            if (applyDmgFromTick == true)
            {
                ticksAfterStop = ticks;
                applyDmgFromTick = false;
            }
            else
            {
                ticksAfterStop = 0;
                applyDmgFromTick = false;
            }
        }

        protected virtual IEnumerator TickDmgCorutine(float dmg, Element type, float tickTime)
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

        protected abstract void SubtractHealth(float number);

        protected void SwapColor()
        {
            if (currentColor == Color.white)
            {
                currentColor = pink;
            }
            else
            {
                currentColor = Color.white;
            }

            spriteRenderer.material.SetColor("_Color", currentColor);
        }
    }
}
