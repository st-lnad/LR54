using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    Vector3 temp;
    Vector3 temp2;
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
        temp = transform.localScale;
        temp.y = 1 + 3 * amount;
        transform.localScale = temp;
    }
}
