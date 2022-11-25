using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OrkWizard
{
    public class StateManager : MonoBehaviour
    {

        private IState _currentState;


        #region GroundStates
        public IState IdleState { get; private set; } = new IdleState();
        public IState MovingState { get; private set; } = new MovingState();
        public IState Manual { get; private set; } = new ManualState();
        public IState PowerSlide { get; private set; } = new PowerSlideState();
        #endregion

        #region InAirStates
        public IState InAirState { get; private set; } = new InAirState();
        public IState WallTouchState { get; private set; } = new WallTouchState();
        public IState PlatformHangState { get; private set; } = new PlatformHangState();
        #endregion

        #region AttackingStates
        public IState ThrowingState { get; private set; } = new ThrowAttackState();
        public IState SpreaderState { get; private set; } = new SpreaderAttackingState();
        #endregion

        #region DmgRecivedState

        public IState DmgKnockbackState { get; private set; } = new DmgKnockbackState();
        public IState DyingState { get; private set; } = new DyingState();

        #endregion

        public Character Character { get; private set; }

        public void ChangeState(IState state)
        {
            if (_currentState != null)
            {
                _currentState.OnExit(this);
            }

            _currentState = state;
            _currentState.OnEnter(this);
        }

        private void Awake()
        {
            Character = GetComponent<Character>();
            // Switch to state Idle
        }

        private void Start()
        {
            ChangeState(IdleState);
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
