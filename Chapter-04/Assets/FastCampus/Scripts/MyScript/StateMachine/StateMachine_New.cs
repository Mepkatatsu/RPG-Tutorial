using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

namespace FastCampus.AI
{
    public abstract class State<T>
    {
        protected StateMachine_New<T> _stateMachine;
        protected T _context;

        public State()
        {

        }

        internal void SetStateMachineAndContect(StateMachine_New<T> stateMachine, T context)
        {
            _stateMachine = stateMachine;
            _context = context;

            OnInitialized();
        }

        public virtual void OnInitialized()
        {

        }

        public virtual void OnEnter()
        {

        }

        public abstract void Update(float deltaTime);

        public virtual void OnExit()
        {

        }
    }
    public sealed class StateMachine_New<T>
    {
        private T _context;

        private State<T> _currentState;
        public State<T> _CurrentState => _currentState;

        private State<T> _priviousState;
        public State<T> _PriviousState => _priviousState;

        private float _elapsedTimeInState = 0.0f;
        public float _ElapsedTimeInState => _elapsedTimeInState;

        private Dictionary<System.Type, State<T>> _states = new Dictionary<System.Type, State<T>>();

        public StateMachine_New(T context, State<T> initialState)
        {
            _context = context;

            // Setup our initial state
            AddState(initialState);
            _currentState = initialState;
            _currentState.OnEnter();
        }

        public void AddState(State<T> state)
        {
            state.SetStateMachineAndContect(this, _context);
            _states[state.GetType()] = state;
        }

        public void Update(float deltaTime)
        {
            _elapsedTimeInState += deltaTime;

            _currentState.Update(deltaTime);
        }

        public R ChangeState<R>() where R : State<T>
        {
            var newType = typeof(R);
            if (_currentState.GetType() == newType)
            {
                return _currentState as R;
            }

            if (_currentState != null)
            {
                _currentState.OnExit();
            }

            _priviousState = _currentState;
            _currentState = _states[newType];
            _currentState.OnEnter();
            _elapsedTimeInState = 0.0f;

            return _currentState as R;
        }
    }
}