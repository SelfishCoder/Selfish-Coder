using System;
using UnityEngine;

namespace SelfishCoder.Util
{
    /// <summary>
    /// 
    /// </summary>
    public class Countdown
    {
        public InternalState State { get; private set; } = default;
        public bool IsPaused { get; private set; } = default;
        public float TotalTime { get; private set; } = default;
        public float StartTime { get; private set; } = default;
        public float StopTime { get; private set; } = default;
        public float ElapsedTime { get; private set; } = default;
        public float RemainingTime { get; private set; } = default;
        public float TimeOutTime { get; private set; } = default;
        public event Action Started;
        public event Action Stopped;
        public event Action TimeOut;
        
        private Countdown(){}

        public Countdown(float totalTime, bool autoStart = false)
        {
            this.TotalTime = totalTime;
            this.RemainingTime = totalTime;
            if(autoStart) Start();
        }

        public void Start()
        {
            this.State = InternalState.Active;
            this.StartTime = Time.time;
            Started?.Invoke();
        }

        public void Stop()
        {
            this.State = InternalState.Inactive;
            this.StopTime = Time.time;
            Stopped?.Invoke();
            Reset();
        }
        
        public void Pause() => IsPaused = true;
        
        public void Resume() => IsPaused = false;

        public void Reset()
        {
            this.State = InternalState.Inactive;
            this.IsPaused = false;
            this.StartTime = default;
            this.ElapsedTime = default;
            this.TimeOutTime = default;
            this.RemainingTime = TotalTime;
        }

        public void Restart()
        {
            this.State = InternalState.Inactive;
            Reset();
            Start();
        }
        
        public void Tick(float deltaTime)
        {
            if (State is InternalState.Inactive || IsPaused) return;
            
            this.TotalTime -= deltaTime;
            this.ElapsedTime += deltaTime;
            
            if (TotalTime > 0) return;
            
            this.TimeOutTime = Time.time;
            this.TotalTime = default;
            TimeOut?.Invoke();
            Reset();
        }
    }
}