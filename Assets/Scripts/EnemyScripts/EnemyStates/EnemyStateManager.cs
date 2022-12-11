using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OrkWizard
{

    public class EnemyStateManager : BaseStateManager
    {
        private void Awake()
        {
            Enemy = GetComponent<Enemy>();
        }

        private void Start()
        {
            ChangeState(new EnemyIdleState());
        }
    }
}