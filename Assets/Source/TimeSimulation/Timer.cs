using UnityEngine;
using System;

namespace Source.TimeSimulation
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private float _time = 0f;
        [SerializeField] private float _timePerSecond = 0.5f;
        [SerializeField] private float _eventDuration = 0.5f;
        [SerializeField] private TimeObserver _timeObserver;
        private float _lastTimeEvent = 0;

    

        private void Update()
        {
            _time += _timePerSecond * Time.deltaTime;
            if (_time >= _lastTimeEvent + _eventDuration)
            {
                _timeObserver.changeTime();
                _lastTimeEvent = _time;
            }
        
        }
    }

    public interface ITimeDependenible
    {
        public void TimeGoOn();
    }
}
