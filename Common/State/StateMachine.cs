using System;

namespace SelfishCoder.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class StateMachine
    {
        /// <summary>
        /// 
        /// </summary>
        protected readonly State initialState = default;

        /// <summary>
        /// 
        /// </summary>
        protected State currentState = default;

        /// <summary>
        /// 
        /// </summary>
        public State InitialState => initialState;

        /// <summary>
        /// 
        /// </summary>
        public State CurrentState => currentState;

        /// <summary>
        /// 
        /// </summary>
        public event Action<State, State> StateChanged;

        /// <summary>
        /// 
        /// </summary>
        private StateMachine()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="initialState"></param>
        public StateMachine(State initialState)
        {
            this.initialState = initialState;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Init()
        {
            currentState = initialState;
            currentState.Enter();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        public void ChangeState(State state)
        {
            State previousState = currentState;
            currentState.Exit();
            currentState = state;
            currentState.Enter();
            StateChanged?.Invoke(previousState, currentState);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Update() => currentState?.Update();

        /// <summary>
        /// 
        /// </summary>
        public void LateUpdate() => currentState?.FixedUpdate();

        /// <summary>
        /// 
        /// </summary>
        public void FixedUpdate() => currentState?.LateUpdate();
    }
}