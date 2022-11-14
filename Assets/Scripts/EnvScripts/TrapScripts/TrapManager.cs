using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OrkWizard
{
    public class TrapManager : MonoBehaviour
    {
        protected const string _playerTag = "Player";
        private const string _activeAnimationBool = "Active";
        protected BoxCollider2D trapHitBox;
        protected Animator animator;

        [SerializeField]
        protected TrapSO trapSO;

        private bool needsToActivate;
        private bool needsToApplyDmg = true;

        // Start is called before the first frame update
        void Start()
        {
            Initialize();
        }

        protected virtual void Initialize()
        {

            trapHitBox = GetComponent<BoxCollider2D>();
            trapHitBox.enabled = false;
            animator = GetComponent<Animator>();

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
            animator.SetBool(_activeAnimationBool, true);
            Invoke("Deactivate", trapSO.activeTime);
        }

        private void Deactivate()
        {
            trapHitBox.enabled = false;
            animator.SetBool(_activeAnimationBool, false);
            if (!trapSO.trigeredByPlayer)
            {
                Invoke("Activate", trapSO.downTime);
            }

        }

        public void Activate()
        {
            needsToActivate = true;
        }

        private void ResetDmgTick()
        {
            needsToApplyDmg = true;
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (trapSO.dealDmgOnlyToPlayer && !collision.CompareTag(_playerTag))
            {
                return;
            }

            if (!needsToApplyDmg)
            {
                return;
            }

            Debug.Log($"I am dealing dmg right now to {collision.gameObject.name}");
            var dmgApplyer = collision.gameObject.GetComponent<IDamagable>();
            if (dmgApplyer != null)
            {
                dmgApplyer.ApplyDmg(trapSO.damage);
                if (trapSO.dmgCooldown > 0)
                {
                    needsToApplyDmg = false;
                    Invoke("ResetDmgTick", trapSO.dmgCooldown);
                }
            }
        }
    }
}