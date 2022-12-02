using System.Collections;
using UnityEngine;

namespace OrkWizard
{
    public class PlayerHealthManager : MonoBehaviour, IDamagable
    {
        private bool applyDmgFromTick = false;

        private Character character;
        private StateManager stateManager;
        private SpriteRenderer spriteRenderer;

        private int ticksAfterStop = 0;

        private Color currentColor = Color.white;
        private Color pink = new Color(1, 0.5f, 0.5f);

        [SerializeField]
        private PlayerHealthSO healthSO;

        private void Awake()
        {
            character = GetComponent<Character>();
            stateManager = GetComponent<StateManager>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            healthSO.currentHealth = healthSO.maxHealth;
            character.UpdateHp(healthSO.currentHealth);
        }

        private IEnumerator TickDmgCorutine(float dmg, DamageType type, float tickTime)
        {
            applyDmgFromTick = true;
            while (applyDmgFromTick)
            {
                SubtractPlayerHealth(dmg);
                yield return new WaitForSeconds(tickTime);
            }

            while (ticksAfterStop > 0)
            {
                SubtractPlayerHealth(dmg);
                yield return new WaitForSeconds(tickTime);
                ticksAfterStop--;
            }
            // just in case
            ticksAfterStop = 0;
        }

        private IEnumerator TintFlickCorutine()
        {
            while (applyDmgFromTick || ticksAfterStop != 0)
            {
                SwapColor();
                yield return new WaitForSeconds(character.playerScriptableObject.flickerFrequency);
            }

            if (currentColor != Color.white)
            {
                SwapColor();
            }
        }

        private void SwapColor()
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

        private void SubtractPlayerHealth(float number)
        {
            healthSO.currentHealth -= number;

            if (healthSO.currentHealth <= 0)
            {
                stateManager.ChangeState(stateManager.DyingState);
            }

            character.UpdateHp(healthSO.currentHealth);
        }

        private IEnumerator TurnOnInvul()
        {
            Physics2D.IgnoreLayerCollision((int)Layers.Player, (int)Layers.Trap, true);
            Physics2D.IgnoreLayerCollision((int)Layers.Player, (int)Layers.Enemy, true);

            yield return new WaitForSeconds(healthSO.invulDuration);

            Physics2D.IgnoreLayerCollision((int)Layers.Player, (int)Layers.Trap, false);
            Physics2D.IgnoreLayerCollision((int)Layers.Player, (int)Layers.Enemy, false);
        }

        public void ApplyDmg(float dmg)
        {
            ApplyDmg(dmg, DamageType.Physical);
        }

        public void ApplyDmg(float dmg, DamageType type)
        {
            // Apply dmg
            if (!character.RecivedOneTimeDmg)
            {
                character.rbController.UpdateSpeed(0, 0);
                stateManager.ChangeState(stateManager.DmgKnockbackState);
                StartCoroutine(TurnOnInvul());
                character.rbController.ApplyKnockBackForce();
                SubtractPlayerHealth(dmg);
            }
        }

        public void ApplyDmg(float dmg, DamageType type, float tickTime)
        {
            // apply tick dmg;
            if (!applyDmgFromTick && ticksAfterStop == 0)
            {
                StartCoroutine(TickDmgCorutine(dmg, type, tickTime));
                StartCoroutine(TintFlickCorutine());
            }
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
    }
}
