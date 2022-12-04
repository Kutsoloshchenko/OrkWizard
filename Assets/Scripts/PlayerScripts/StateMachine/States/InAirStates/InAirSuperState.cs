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

        public abstract void OnEnter(BaseStateManager stateManager);
        public abstract void OnExit(BaseStateManager stateManager);

        public virtual void OnFixedUpdate(BaseStateManager stateManager)
        {
            if (stateManager.Character.CheckGroundRayCast())
            {
                if (stateManager.Character.PowerSliding())
                {
                    stateManager.ChangeState(new PowerSlideState());
                }
                else if (stateManager.Character.rbController.GetCurrentSpeed().x != 0)
                {
                    stateManager.ChangeState(new MovingState());
                }
                else
                {
                    stateManager.ChangeState(new IdleState());
                }

                return;
            }
        }
    }
}
