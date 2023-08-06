using UnityEngine;
using Source;

namespace Source.GrapplingHook3
{
    public class Hook : MonoBehaviour
    {
        [SerializeField] Rigidbody2D _player;
        [SerializeField] Rope _rope;
        [SerializeField] private float _speed;
        [SerializeField] private DistanceJoint2D _joint;
        [SerializeField] private GrapplingHookRaycaster _raycatser;

        private bool _hooked;

        private IInput _input = new DesktopInput();
        private void ShootHook()
        {
            Debug.Log("Try Shoot!");
            float[] cursorPos = _input.CursorPos();
            Vector2 direction = new Vector3(cursorPos[0], cursorPos[1], 0) - _player.transform.position;
            Debug.DrawRay(_player.transform.position, direction, Color.red, _rope.MaxDistance);
            var _hit = _raycatser.Raycast(_player.transform.position, direction, _rope.MaxDistance);

            if (_hit)
            {
                if (_hooked)
                    ReturnHook();
                Debug.Log($"Попали в {_hit.collider.gameObject.name}");
                _rope.Initialize(_player.transform, _hit.point);
                _joint.connectedBody = _rope.RotationPoint;
                _joint.distance = _rope.DistanceToNewRotationPoint;
                _joint.enabled = true;
                _hooked = true;
            }
        }
        private void ReturnHook()
        {
            Debug.Log("Try Return Hook!");
            _rope.Terminate();
            _joint.enabled = false;
            _hooked = false;
        }
        private void MoveOnHook()
        {
            float movement = _speed * _input.GrapplingHookMove() * Time.deltaTime;
            if (movement != 0f)
                Debug.Log("Try Move!");
            _joint.distance = Mathf.Clamp(_joint.distance - movement, 0.1f, _rope.MaxDistance);
        }

        private void Update()
        {
            if (_input.Shoot()) ShootHook();
            if (_input.ReturnHook()) ReturnHook();
            if (_hooked) MoveOnHook();
        }
        private void OnEnable()
        {
            _rope.RotationPointChanged += OnRotationPointChanged;
        }
        private void OnDisable()
        {
            _rope.RotationPointChanged -= OnRotationPointChanged;
        }

        private void OnRotationPointChanged()
        {
            _joint.connectedBody = _rope.RotationPoint;
            _joint.distance = _rope.DistanceToNewRotationPoint;
        }
    }
}