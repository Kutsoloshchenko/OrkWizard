using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrkWizard
{
    public interface IState
    {
        void OnEnter(StateManager stateManager);
        void OnExit(StateManager stateManager);
        void OnUpdate(StateManager stateManager);
        void OnFixedUpdate(StateManager stateManager);
    }
}
