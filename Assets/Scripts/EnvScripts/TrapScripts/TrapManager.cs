using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OrkWizard
{
    public class TrapManager : AnimatorControllerBase
    {
        protected const string _playerTag = "Player";
        private const string _activePostfix = "_active";
        private const string _idlePostfix = "_idle";
        protected BoxCollider2D trapHitBox;

        [SerializeField]
        protected TrapSO trapSO;

        private bool needsToActivate;

        protected override void Initialize()
        {
            base.Initialize();
            trapHitBox = GetComponent<BoxCollider2D>();
            trapHitBox.enabled = false;
            ChangeAnimation(trapSO.name + _idlePostfix);
            needsToActivate = !trapSO.trigeredByPlayer;
        }

        private void FixedUpdate()
        {
            if (needsToActivate)
            {
                ActivateHitBox();
            }
        }

        private void ActivateHitBox()
        {
            trapHitBox.enabled = true;
            needsToActivate = false;
            ChangeAnimation(trapSO.name + _activePostfix);
            Invoke("Deactivate", trapSO.activeTime);
        }

        private void Deactivate()
        {
            trapHitBox.enabled = false;
            ChangeAnimation(trapSO.name + _idlePostfix);
            if (!trapSO.trigeredByPlayer)
            {
                Invoke("Activate", trapSO.downTime);
            }

        }

        public void Activate()
        {
            needsToActivate = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (trapSO.dealDmgOnlyToPlayer && !collision.CompareTag(_playerTag))
            {
                return;
            }

            Debug.Log($"I am dealing dmg right now to {collision.gameObject.name}");
            var dmgApplyer = collision.gameObject.GetComponent<IDamagable>();
            if (dmgApplyer != null)
            {

                if (trapSO.dmgOverTime)
                {
                    dmgApplyer.ApplyDmg(trapSO.damage, trapSO.dmgType, trapSO.dmgCooldown);
                }
                else
                {
                    dmgApplyer.ApplyDmg(trapSO.damage, trapSO.dmgType);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            var dmgApplyer = collision.gameObject.GetComponent<IDamagable>();
            if (dmgApplyer != null && trapSO.dmgOverTime)
            {
                dmgApplyer.StopTickDmg();
            }
        }
    }
}