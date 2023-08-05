using System.Collections.Generic;
using UnityEngine;
using Source.TimeSimulation;

namespace Source.TimeSimulation
{
    class TimeObserver : MonoBehaviour
    {
        [SerializeField] private List<ITimeDependenible> _timeDependenibles = new List<ITimeDependenible>();

        public void changeTime()
        {
            foreach (var item in _timeDependenibles)
            {
                item.TimeGoOn();
            }
        }

    }
}