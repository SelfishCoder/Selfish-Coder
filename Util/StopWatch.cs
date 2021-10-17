using System;
using UnityEngine;

namespace SelfishCoder.Util
{
    /// <summary>
    /// 
    /// </summary>
    public class StopWatch
    {
        public InternalState State { get; private set; } = default;
        public bool IsPaused { get; private set; } = default;
        public float StartTime { get; private set; } = default;
        public float StopTime { get; private set; } = default;
        public float ElapsedTime { get; private set; } = default;

        public event Action Started; 
        public event Action Stopped;
        
        public StopWatch(bool autoStart)
        {
            if (autoStart) Start();
        }
        
        public void Start()
        {
            State = InternalState.Active;
            StartTime = Time.time;
            Started?.Invoke();
        }

        public void Tick(float deltaTime)
        {
            if(State is InternalState.Inactive || IsPaused) return;
            ElapsedTime += deltaTime;
        }
  
        public void Resume() => IsPaused = false;
     
        public void Pause() => IsPaused = true;
   
        public void Stop()
        {
            if(State is InternalState.Inactive) return;
            State = InternalState.Inactive;
            StopTime = Time.time;
            Stopped?.Invoke();
            Reset();
        }

        public void Reset()
        {
            State = InternalState.Inactive;
            IsPaused = false;
            ElapsedTime = default;
            StartTime = default;
            StopTime = default;
        }
    }
}