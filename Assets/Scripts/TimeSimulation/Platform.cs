using UnityEngine;
using System.Collections.Generic;


namespace Source.TimeSimulation
{
    public class Platform : MonoBehaviour, ITimeDependenible
    {

        public void TimeGoOn()
        {
            Debug.Log("»пать, врем€ прошло!");
        }
    }
}

