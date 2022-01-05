
using System;
using System.Diagnostics;

namespace Color_Breaker
{
    public sealed class Tween
    {
        private float _startValue;
        private float _endValue;
        private float _duration;
        private float _time;
        private float _delay;
        private object debug;

        public bool isRunning { get; private set; }

        public Tween()
        {
            _time = 0f;
        }

        public void Start(float startValue, float endValue, float duration, float delay = 0.0f)
        {
            _time = 0f; // t
            _delay = delay;
            _startValue = startValue; // b
            _endValue = endValue - _startValue; // c
            _duration = duration; // d
            _delay = delay;
            isRunning = true;
        }

        private void Update(float deltaTime)
        {
            
            if (!isRunning) return;
            
            if (_delay > 0f)
            {
                _delay -= deltaTime;
                return;
            }
            
            if (_time <= _duration)
            {
                Debug.WriteLine(_time);
                _time += deltaTime;
            }else
            {
                isRunning = false;
            }
        }

        public float Linear(float deltaTime)
        {
            Update(deltaTime);
            return _endValue * _time / _duration + _startValue;
        }

        public float easeInOutSine(float deltaTime)
        {
            Update(deltaTime);
            return -_endValue / 2 * ((float)Math.Cos(Math.PI * _time / _duration) - 1) + _startValue;
        }
    }
}
