using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OrkWizard
{
    public abstract class InAirSuperState : TransitionToAttackSupperState, IState
    {
        protected const string _ollie = "Ollie";
        protected const string _fall = "Fall";

        public abstract void OnEnter(StateManager stateManager);
        public abstract void OnExit(StateManager stateManager);

        public override void OnFixedUpdate(StateManager stateManager)
        {
            if (stateManager.Character.CheckGroundRayCast())
            {
                if (stateManager.Character.PowerSliding())
                {
                    stateManager.ChangeState(stateManager.PowerSlide);
                }
                else if (stateManager.Character.rbController.GetCurrentSpeed().x != 0)
                {
                    stateManager.ChangeState(stateManager.MovingState);
                }
                else
                {
                    stateManager.ChangeState(stateManager.IdleState);
                }

                return;
            }
        }
    }
}
