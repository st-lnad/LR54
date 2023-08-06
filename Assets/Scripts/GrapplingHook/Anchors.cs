using System.Collections.Generic;
using UnityEngine;

namespace GrapplingHook
{
    public class Anchors : MonoBehaviour
    {
        [SerializeField] private List<Transform> _points = new();
        [SerializeField] private GameObject _anchorPrefab;

        public Transform RotationPoint => _points[1];

        public Transform PreviousRotationPoint => _points.Count >= 2 ? _points[2] : _points[1];

        public Transform Start => _points[0];
        public int Length => _points.Count;

        public void AddRotationPoint(Vector2 point)
        {
            var newAnchor = Instantiate(_anchorPrefab);
            newAnchor.transform.position = point;
            _points.Insert(1, newAnchor.transform);
        }

        public void DeleteRotationPoint()
        {
            Destroy(_points[1].gameObject);
            _points.RemoveAt(1);
        }

        public void Initialize(Transform start, Vector2 end)
        {
            _points.Add(start);
            AddRotationPoint(end);
        }

        public void Terminate()
        {
            for (var i = 1; i < _points.Count; i++) Destroy(_points[i].gameObject);
            _points.Clear();
        }

        public Vector3[] GetAnchorsPositions()
        {
            var result = new Vector3[_points.Count];
            for (var i = 0; i < _points.Count; i++) result[i] = _points[i].position;
            return result;
        }
    }
}