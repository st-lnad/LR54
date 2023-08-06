using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEditor;
using UnityEngine.UIElements;

public class AliveInTime : MonoBehaviour
{
    [SerializeField] TimeStates[] _timeMask;
}

[System.Serializable]
public enum TimeStates
{
    Time1=0,
    Time2=1,
    Time3=2,
}
