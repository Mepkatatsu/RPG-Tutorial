using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FastCampus.AI
{
    public abstract class State_ymkim50<T>
    {
        protected int mecanimStateHash;
        protected StateMachine_ymkim50<T> stateMachine;
        protected T context;

        public State_ymkim50()
        {
        }

        /// <summary>
        /// Constructor that takes the mecanim state name as a string
        /// </summary>
        public State_ymkim50(string mecanimStateName) : this(Animator.StringToHash(mecanimStateName))
        {
        }

        /// <summary>
        /// Constructor that takes the mecanim state hash
        /// </summary>
        public State_ymkim50(int mecanimStateHash)
        {
            this.mecanimStateHash = mecanimStateHash;
        }

        internal void SetMachineAndContext(StateMachine_ymkim50<T> stateMachine, T context)
        {
            this.stateMachine = stateMachine;
            this.context = context;

            OnInitialized();
        }

        /// <summary>
        /// Called directly after the machine and context are set allowing the state to do any required setup
        /// </summary>
        public virtual void OnInitialized()
        { }

        public virtual void OnEnter()
        { }

        public virtual void PreUpdate()
        { }

        public abstract void Update(float deltaTime);

        public virtual void OnExit()
        { }
    }

    public sealed class StateMachine_ymkim50<T>
    {
        private T context;
        public event Action OnChangedState;

        private State_ymkim50<T> currentState;
        public State_ymkim50<T> CurrentState => currentState;

        private State_ymkim50<T> previousState;
        public State_ymkim50<T> PreviousState => previousState;

        private float elapsedTimeInState = 0.0f;
        public float ElapsedTimeInState => elapsedTimeInState;

        private Dictionary<System.Type, State_ymkim50<T>> states = new Dictionary<Type, State_ymkim50<T>>();

        public StateMachine_ymkim50(T context, State_ymkim50<T> initialState)
        {
            this.context = context;

            // Setup our initial state
            AddState(initialState);
            currentState = initialState;
            currentState.OnEnter();
        }

        /// <summary>
        /// Adds the state to the machine
        /// </summary>
        public void AddState(State_ymkim50<T> state)
        {
            state.SetMachineAndContext(this, context);
            states[state.GetType()] = state;
        }

        /// <summary>
        /// Tick the state machine with the provided delta time
        /// </summary>
        public void Update(float deltaTime)
        {
            elapsedTimeInState += deltaTime;

            currentState.PreUpdate();
            currentState.Update(deltaTime);
        }

        /// <summary>
        /// Changes the current state
        /// </summary>
        public R ChangeState<R>() where R : State_ymkim50<T>
        {
            // avoid changing to the same state
            var newType = typeof(R);
            if (currentState.GetType() == newType)
            {
                return currentState as R;
            }

            // only call end if we have a currentState
            if (currentState != null)
            {
                currentState.OnExit();
            }


#if UNITY_EDITOR
            if (!states.ContainsKey(newType))
            {
                var error = GetType() + ": state " + newType + " does not exist. Did you forget to add it by calling addState?";
                Debug.LogError("error");
                throw new Exception(error);
            }
#endif

            // swap states and call OnEnter
            previousState = currentState;
            currentState = states[newType];
            currentState.OnEnter();
            elapsedTimeInState = 0.0f;

            // Fire the changed event if we hav a listener
            if (OnChangedState != null)
            {
                OnChangedState();
            }

            return currentState as R;
        }

    }
}