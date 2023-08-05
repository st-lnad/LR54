using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrlScript : MonoBehaviour
{
    private void FixedUpdate()
    {
        if (Input.GetKey("e")) 
        {
            StaticScript.ChangeTime(Time.deltaTime);
        }
        if (Input.GetKey("q"))
        {
            StaticScript.ChangeTime(-Time.deltaTime);
        }
    }
}
