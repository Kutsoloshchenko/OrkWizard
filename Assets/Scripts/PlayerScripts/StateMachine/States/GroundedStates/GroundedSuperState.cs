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
        public abstract void OnEnter(BaseStateManager stateManager);
        public abstract void OnExit(BaseStateManager stateManager);

        public virtual void OnFixedUpdate(BaseStateManager stateManager)
        {
            if (!stateManager.Character.CheckGroundRayCast())
            {
                stateManager.ChangeState(new InAirState());
            }

            if (stateManager.Character.PowerSliding())
            {
                stateManager.ChangeState(new PowerSlideState());
            }
        }

    }
}
