using UnityEngine;

namespace OrkWizard
{
    public class BaseStateManager : MonoBehaviour//, IStateManager
    {
        private IState _currentState;

        public PlayerCharacter Character { get; protected set; }
        public Enemy Enemy { get; protected set; }

        public void ChangeState(IState state)
        {
            if (_currentState != null)
            {
                _currentState.OnExit(this);
            }

            _currentState = state;
            _currentState.OnEnter(this);
        }

        private void Update()
        {
            if (_currentState != null)
            {
                _currentState.OnUpdate(this);
            }
        }

        private void FixedUpdate()
        {
            _currentState.OnFixedUpdate(this);
        }

    }
}
