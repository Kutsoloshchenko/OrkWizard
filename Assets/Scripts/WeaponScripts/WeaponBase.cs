using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OrkWizard
{
    public class WeaponBase : MonoBehaviour
    {
        protected const string _playerTag = "Player";

        protected bool canAttack = true;

        [SerializeField]
        protected PlayerWeaponSO weaponSO;

        private void Start()
        {
            Initialize();
        }

        protected virtual void Initialize()
        {
            canAttack = true;
        }

        public bool CanAttack()
        {
            return canAttack;
        }

        public float GetAnimationLength()
        {
            return weaponSO.animationClip.length;
        }

        public float GetMaxHorizontalSpeed()
        {
            return weaponSO.speedAllowed;
        }

        public string GetAnimationName()
        {
            return weaponSO.animationClip.name;
        }

        public void ResetAtack()
        {
            canAttack = true;
        }

        public virtual void DisableNewAttacks()
        {
            canAttack = false;
        }

        public float GetWeaponOffsetX()
        {
            return weaponSO.offsetX;
        }

        public float GetWeaponOffsetY()
        {
            return weaponSO.offsetY;
        }

        public bool IsThroable()
        {
            return weaponSO.throwable;
        }
    }
}
