using System;
using UnityEngine;

namespace Manager
{
    public class TickSystemManager : MonoBehaviour
    {
        private float _nextTickStamp = 0;
        private float _tickDuration;
        
        public float tps; //ticks per second
        public static int tick;
        public static event System.Action TickEvent;

        private static void OnTickEvent()
        {
            TickEvent?.Invoke();
        }

        private void Start()
        {
            _tickDuration = 1 / tps;
        }

        private void Update()
        {
            if (Time.time >= _nextTickStamp)
            {
                _nextTickStamp = Time.time + _tickDuration;
                OnTickEvent();
            }       
        }
    }
}