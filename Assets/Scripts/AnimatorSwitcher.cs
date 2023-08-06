using UnityEngine;
using GrapplingHook;


public class AnimatorSwitcher : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private readonly string _animatorHookStateBoolName = "Hooked";

    private void OnEnable()
    {
        Hook.Hooked += OnHooked;
    }
    private void OnDisable()
    {
        Hook.Hooked -= OnHooked;
    }
    private void OnHooked(bool hooked)
    {
        _animator.SetBool(_animatorHookStateBoolName, hooked);
    }
}
