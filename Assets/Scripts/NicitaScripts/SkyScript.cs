using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkyScript : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public SpriteRenderer rend;

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
        rend.color = gradient.Evaluate(amount / 3);
    }
}
