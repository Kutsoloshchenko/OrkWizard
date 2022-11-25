using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OrkWizard
{
    public abstract class GroundedSuperState : TransitionToAttackSupperState, IState
    {
        public abstract void OnEnter(StateManager stateManager);
        public abstract void OnExit(StateManager stateManager);

        public override void OnFixedUpdate(StateManager stateManager)
        {
            if (!stateManager.Character.CheckGroundRayCast())
            {
                stateManager.ChangeState(stateManager.InAirState);
            }

            if (stateManager.Character.PowerSliding())
            {
                stateManager.ChangeState(stateManager.PowerSlide);
            }
        }

    }
}
