using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VintScript : MonoBehaviour
{
    private void OnEnable()
    {
        StaticScript.timeChanged += OnTimeChanged;
    }

    private void OnDisable()
    {
        StaticScript.timeChanged -= OnTimeChanged;
    }
    private void OnTimeChanged(float amount)
    {
        transform.rotation = Quaternion.Euler(0, 0, 90*amount);
    }
}
