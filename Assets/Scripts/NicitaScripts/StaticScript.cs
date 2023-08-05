using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class StaticScript
{
    public static float time;
    public static event Action<float> timeChanged;
    public static PhasingScript[] PhasingThings;
    static StaticScript() 
    {
        PhasingThings = GameObject.FindObjectsOfType<PhasingScript>();
        Debug.Log(PhasingThings);
    }

    public static void ChangeTime(float amount) 
    {
        time += amount;
        if (time < 0) { time = 0; }
        if (time >= 3) {time = 2.99f; }
        timeChanged?.Invoke(time);
        OnTimeChanged(time);
        
    }

    static private void OnTimeChanged(float amount)
    {
        foreach (PhasingScript q in PhasingThings)
        if ((int)amount == q.Phase)
        {
            q.gameObject.SetActive(true);
        }
        else
        {
            q.gameObject.SetActive(false);
        }
    }

}
