using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrkWizard
{
    public interface IState
    {
        void OnEnter(BaseStateManager stateManager);
        void OnExit(BaseStateManager stateManager);
        void OnUpdate(BaseStateManager stateManager);
        void OnFixedUpdate(BaseStateManager stateManager);
    }
}
