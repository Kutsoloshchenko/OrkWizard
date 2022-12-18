using System.Collections;
using UnityEngine;

namespace OrkWizard
{
    public class PlayerHealthManager : BaseHealthManager, IDamagable
    {
        private PlayerCharacter character;
        private PlayerStateManager stateManager;

        [SerializeField]
        private PlayerHealthSO healthSO;

        protected override void Awake()
        {
            base.Awake();

            character = GetComponent<PlayerCharacter>();
            stateManager = GetComponent<PlayerStateManager>();
            healthSO.currentHealth = healthSO.maxHealth;
            character.UpdateHp(healthSO.currentHealth);
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

        private IEnumerator TurnOnInvul()
        {
            Physics2D.IgnoreLayerCollision((int)Layers.Player, (int)Layers.Trap, true);
            Physics2D.IgnoreLayerCollision((int)Layers.Player, (int)Layers.Enemy, true);
            Physics2D.IgnoreLayerCollision((int)Layers.Player, (int)Layers.Projectile, true);

            yield return new WaitForSeconds(healthSO.invulDuration);

            Physics2D.IgnoreLayerCollision((int)Layers.Player, (int)Layers.Trap, false);
            Physics2D.IgnoreLayerCollision((int)Layers.Player, (int)Layers.Enemy, false);
            Physics2D.IgnoreLayerCollision((int)Layers.Player, (int)Layers.Projectile, true);
        }

        public override void ApplyDmg(float dmg, Element type)
        {
            // Apply dmg
            if (!character.RecivedOneTimeDmg)
            {
                character.rbController.UpdateSpeed(0, 0);
                stateManager.ChangeState(new DmgKnockbackState());
                StartCoroutine(TurnOnInvul());
                character.rbController.ApplyKnockBackForce();
                SubtractHealth(dmg);
            }
        }

        public override void ApplyDmg(float dmg, Element type, float tickTime)
        {
            // apply tick dmg;
            if (!applyDmgFromTick && ticksAfterStop == 0)
            {
                StartCoroutine(TickDmgCorutine(dmg, type, tickTime));
                StartCoroutine(TintFlickCorutine());
            }
        }

        protected override void SubtractHealth(float number)
        {
            healthSO.currentHealth -= number;

            if (healthSO.currentHealth <= 0)
            {
                stateManager.ChangeState(new DyingState());
            }

            character.UpdateHp(healthSO.currentHealth);
        }
    }
}
