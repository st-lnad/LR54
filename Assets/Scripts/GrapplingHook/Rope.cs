using System;
using UnityEngine;

namespace Source.GrapplingHook3
{
    public class Rope : MonoBehaviour
    {
        [SerializeField] private float _maxDistance;
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private GrapplingHookRaycaster _raycatser;
        [SerializeField] private Anchors _anchors;

        public float MaxDistance => _maxDistance;
        public Rigidbody2D RotationPoint => _anchors.RotationPoint.gameObject.GetComponent<Rigidbody2D>(); //TODO property
        public float DistanceToNewRotationPoint => Vector3.Magnitude(_anchors.Start.position - _anchors.RotationPoint.position);

        public event Action RotationPointChanged;
        public static event Action<float> RopeLengthChanged;


        private Transform _player;
        private Vector3 _target;
        private bool _initialized;
        private float _ropeLength;

        
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
        private void Render() {
            var point = _anchors.GetAnchorsPositions();
            _lineRenderer.positionCount = point.Length;
            _lineRenderer.endColor = Color.HSVToRGB(5, 58, 40);
            for (int i  = 0; i < point.Length; i++) {
                if (i > 1) Debug.Log($"Set point {i}");
                _lineRenderer.SetPosition(i, point[i]);
            }
        }

        private RaycastHit2D[] checkForNewLet(Vector2 start, Vector2 rotationPoint) {
            var direction = rotationPoint - start;
            var distanse = Vector2.Distance(rotationPoint, start);
            var hit = _raycatser.Raycast(start, direction, distanse - 0.1f);
            if (hit)
            {
                return new RaycastHit2D[] { hit };
            }
            return new RaycastHit2D[] { };
        }
        private bool checkStayOldLet(Vector2 start, Vector2 oldPoint) {
            var direction = oldPoint - start;
            var distanse = Vector2.Distance(oldPoint, start);
            var hit = _raycatser.Raycast(start, direction, distanse - 0.1f);
            return hit;
        }
        private void FixedUpdate()
        {
            if (!_initialized)
                return;

            if (_anchors.Length > 2)
            {
                if (checkStayOldLet(_anchors.Start.position, _anchors.PreviousRotationPoint.position) == false)
                {
                    _anchors.deleteRotationPoint();
                    RotationPointChanged?.Invoke();
                }
            }
            var mbHit = checkForNewLet(_anchors.Start.position, _anchors.RotationPoint.position);
            if (mbHit.Length > 0)
            {
                var position = mbHit[0].point;
                _anchors.addRotaionPoint(position);
                RotationPointChanged?.Invoke();
            }
            Render();
            ComputeLength();
        }
        private void ComputeLength()
        {
            float elapsedLength = 0f;
            var positions = _anchors.GetAnchorsPositions();
            for (int i = 1; i < positions.Length; i++)
            {
                elapsedLength += Vector3.Magnitude(positions[i] - positions[i - 1]);
            }
            if (elapsedLength != _ropeLength)
                RopeLengthChanged?.Invoke(elapsedLength);
            _ropeLength = elapsedLength;

        }

         

    }
}