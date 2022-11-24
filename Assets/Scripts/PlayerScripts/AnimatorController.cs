using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OrkWizard
{
    public class AnimatorController : AnimatorControllerBase
    {
        private const string _movingPostFix = "_moving";

        public bool Moving { get; private set; }

        [SerializeField]
        private PlayerSO playerSO;

        protected override void Initialize()
        {
            base.Initialize();
        }

        public void SetMoving(bool value)
        {
            Moving = value;
        }

        public void SetAttack(string attackName)
        {
            if (Moving)
            {
                ChangeAnimation(attackName + _movingPostFix);
                return;
            }

            ChangeAnimation(attackName);
        }

    }
}