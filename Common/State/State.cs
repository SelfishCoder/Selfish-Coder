using System;

namespace SelfishCoder.Common
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class State
    {
        /// <summary>
        /// 
        /// </summary>
        protected StateMachine stateMachine = default;

        /// <summary>
        /// 
        /// </summary>
        public event Action Entered;

        /// <summary>
        /// 
        /// </summary>
        public event Action Exited;

        /// <summary>
        /// 
        /// </summary>
        private State()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stateMachine"></param>
        protected State(StateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Enter()
        {
            OnEnter();
            Entered?.Invoke();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Exit()
        {
            OnExit();
            Exited?.Invoke();
        }
        
        /// <summary>
        /// 
        /// </summary>
        public void Update() => OnUpdate();

        /// <summary>
        /// 
        /// </summary>
        public void LateUpdate() => OnLateUpdate();

        /// <summary>
        /// 
        /// </summary>
        public void FixedUpdate() => OnFixedUpdate();

        /// <summary>
        /// 
        /// </summary>
        protected abstract void OnEnter();

        /// <summary>
        /// 
        /// </summary>
        protected virtual void OnUpdate(){}

        /// <summary>
        /// 
        /// </summary>
        protected virtual void OnLateUpdate(){}

        /// <summary>
        /// 
        /// </summary>
        protected virtual void OnFixedUpdate() {}

        /// <summary>
        /// 
        /// </summary>
        protected abstract void OnExit();
    }
}