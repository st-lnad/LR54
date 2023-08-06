using CustomInput;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GrapplingHook
{
    public class Hook : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _player;
        [SerializeField] private Rope _rope;
        [SerializeField] private float _speed;
        [SerializeField] private DistanceJoint2D _joint;
        [SerializeField] private GrapplingHookRaycaster _raycatser;

        public static event Action<InputMasStates> MapStateChanged;
        public static event Action<bool> Hooked;

        private bool _hooked;
        private IInput _input = new DesktopInput();


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

        private void ShootHook()
        {
            var cursorPos = _input.CursorPos();
            Vector2 direction = new Vector3(cursorPos[0], cursorPos[1], 0) - _player.transform.position;
            Debug.DrawRay(_player.transform.position, direction, Color.red, _rope.MaxDistance);
            var hit = _raycatser.Raycast(_player.transform.position, direction, _rope.MaxDistance);

            if (hit)
            {
                if (_hooked)
                    ReturnHook();
                _rope.Initialize(_player.transform, hit.point);
                _joint.connectedBody = _rope.RotationPoint;
                _joint.distance = _rope.DistanceToNewRotationPoint;
                _joint.enabled = true;
                _hooked = true;
                Hooked?.Invoke(_hooked);
                MapStateChanged?.Invoke(InputMasStates.OnHook);
            }
        }

        private void ReturnHook()
        {
            _rope.Terminate();
            _joint.enabled = false;
            _hooked = false;
            Hooked?.Invoke(_hooked);
            MapStateChanged?.Invoke(InputMasStates.OnGround);
        }

        private void MoveOnHook()
        {
            var movementVertical = _speed * _input.GrapplingHookMove() * Time.deltaTime;
            _joint.distance = Mathf.Clamp(_joint.distance - movementVertical, 0.1f, _rope.MaxDistance);

            var ropeDirection = _rope.Direction;
            var inputHorizontal = Mathf.Ceil(_input.HorizontalMove());
            Vector2 rotatedRopeDirection = new Vector2(ropeDirection.y, -ropeDirection.x).normalized * inputHorizontal;
            float force = Mathf.Abs((ropeDirection.normalized - new Vector3(Mathf.Abs(inputHorizontal), 0, 0)).x * _speed * Time.deltaTime);
            if ((force * rotatedRopeDirection).magnitude != 0)
            {
                _player.AddForce(force * rotatedRopeDirection, ForceMode2D.Impulse);
            }
        }

        private void OnRotationPointChanged()
        {
            _joint.connectedBody = _rope.RotationPoint;
            _joint.distance = _rope.DistanceToNewRotationPoint;
        }
    }
}