using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CustomInput
{
    public class InputMapChanger : MonoBehaviour
    {
        [SerializeField] private PlayerInput _playerInput;
        private readonly Dictionary<InputMasStates, string> stateNamePairs = new Dictionary<InputMasStates, string>
        {
            { InputMasStates.OnHook , "OnHook" },
            { InputMasStates.OnGround , "OnGround" },
        };
        private void OnEnable()
        {
            GrapplingHook.Hook.MapStateChanged += OnMapStateChanged;
        }

        private void OnDisable()
        {
            GrapplingHook.Hook.MapStateChanged -= OnMapStateChanged;
        }

        private void OnMapStateChanged(InputMasStates newMap)
        {
            _playerInput.SwitchCurrentActionMap(stateNamePairs[newMap]);
        }
    }

    [Serializable]
    public enum InputMasStates
    {
        OnHook,
        OnGround
    }
}