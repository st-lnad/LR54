using Source.CustomInput;
using UnityEngine;

namespace Source.OldHook
{
    namespace Source
    {
        public class HookShooter : MonoBehaviour
        {
            [SerializeField] private Hook _hook;
            [SerializeField] private LayerMask _layerMask;
            [SerializeField] private GameObject _shooter;


            private IInput _input = new DesktopInput();
            private void Update()
            {
                if (_input.Shoot())
                {
                    Debug.Log("Try Shoot!");
                    float[] cursorPos = _input.CursorPos();
                    Vector2 direction = new Vector3(cursorPos[0], cursorPos[1], 0) - _shooter.transform.position;
                    Debug.DrawRay(_shooter.transform.position, direction, Color.red, 10f);

                    var _hit = Physics2D.Raycast(_shooter.transform.position, direction, _hook.MaxDistance, _layerMask);

                    if (_hit)
                    {
                        Debug.Log($"Попали в {_hit.collider.gameObject.name}");
                        _hook.SetTarget(_hit.point);
                    }
                }
                if (_input.ReturnHook())
                {
                    Debug.Log("Try Return Hook!");
                    _hook.ResetTerget();
                }
            }
        }
    }
}