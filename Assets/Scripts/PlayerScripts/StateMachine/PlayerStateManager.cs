using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OrkWizard
{
    public class PlayerStateManager : BaseStateManager
    {
        private void Awake()
        {
            Character = GetComponent<PlayerCharacter>();
            // Switch to state Idle
        }

        private void Start()
        {
            ChangeState(new IdleState());
        }
    }
}
