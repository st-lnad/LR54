using System;
using UnityEngine;

namespace GrapplingHook
{
    public class Rope : MonoBehaviour
    {
        [SerializeField] private float _maxDistance;
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private GrapplingHookRaycaster _raycatser;
        [SerializeField] private Anchors _anchors;
        private bool _initialized;


        private Transform _player;
        private float _ropeLength;
        private Vector3 _target;

        public Vector3 Direction => _anchors.RotationPoint.position - _anchors.Start.position; 
        public float MaxDistance => _maxDistance;
        public Rigidbody2D RotationPoint => _anchors.RotationPoint.gameObject.GetComponent<Rigidbody2D>();

        public float DistanceToNewRotationPoint =>
            Vector3.Magnitude(_anchors.Start.position - _anchors.RotationPoint.position);

        private void FixedUpdate()
        {
            if (!_initialized)
                return;

            if (_anchors.Length > 2)
                if (CheckStayOldLet(_anchors.Start.position, _anchors.PreviousRotationPoint.position) == false)
                {
                    _anchors.DeleteRotationPoint();
                    RotationPointChanged?.Invoke();
                }

            var mbHit = CheckForNewLet(_anchors.Start.position, _anchors.RotationPoint.position);
            if (mbHit.Length > 0)
            {
                var position = mbHit[0].point;
                _anchors.AddRotationPoint(position);
                RotationPointChanged?.Invoke();
            }

            Render();
            ComputeLength();
        }

        public event Action RotationPointChanged;
        public static event Action<float> RopeLengthChanged;


        public void Initialize(Transform player, Vector3 targetPoint)
        {
            _player = player;
            _target = targetPoint;
            _anchors.Initialize(player, targetPoint);
            _initialized = true;
        }

        public void Terminate()
        {
            _initialized = false;
            _anchors.Terminate();
            _ropeLength = 0;
            RopeLengthChanged?.Invoke(_ropeLength);
            _lineRenderer.positionCount = 0;
        }

        private void Render()
        {
            var point = _anchors.GetAnchorsPositions();
            _lineRenderer.positionCount = point.Length;
            _lineRenderer.endColor = Color.HSVToRGB(5, 58, 40);
            for (var i = 0; i < point.Length; i++)
                _lineRenderer.SetPosition(i, point[i]);
        }

        private RaycastHit2D[] CheckForNewLet(Vector2 start, Vector2 rotationPoint)
        {
            var direction = rotationPoint - start;
            var distance = Vector2.Distance(rotationPoint, start);
            var hit = _raycatser.Raycast(start, direction, distance - 0.1f);
            if (hit) return new[] { hit };
            return new RaycastHit2D[] { };
        }

        private bool CheckStayOldLet(Vector2 start, Vector2 oldPoint)
        {
            var direction = oldPoint - start;
            var distance = Vector2.Distance(oldPoint, start);
            var hit = _raycatser.Raycast(start, direction, distance - 0.1f);
            return hit;
        }

        private void ComputeLength()
        {
            var elapsedLength = 0f;
            var positions = _anchors.GetAnchorsPositions();
            for (var i = 1; i < positions.Length; i++)
                elapsedLength += Vector3.Magnitude(positions[i] - positions[i - 1]);
            if (elapsedLength != _ropeLength)
                RopeLengthChanged?.Invoke(elapsedLength);
            _ropeLength = elapsedLength;
        }
    }
}