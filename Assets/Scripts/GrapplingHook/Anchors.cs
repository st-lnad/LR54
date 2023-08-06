using System.Collections.Generic;
using UnityEngine;

namespace Source.GrapplingHook3
{
    public class Anchors : MonoBehaviour
    {
        [SerializeField] private List<Transform> _points = new List<Transform>();
        [SerializeField] private GameObject _anchorPrefab;

        public Transform RotationPoint => _points[1];
        public Transform PreviousRotationPoint
        {
            get { return ((_points.Count >= 2) ? _points[2] : _points[1]); }
        }
        public Transform Start => _points[0];
        public int Length => _points.Count;
        
        public void addRotaionPoint(Vector2 point)
        {
            var newAnchor = Instantiate(_anchorPrefab);
            newAnchor.transform.position = point;
            _points.Insert(1, newAnchor.transform);
        }

        public void deleteRotationPoint()
        {
            Destroy(_points[1].gameObject);
            _points.RemoveAt(1);
        }

        public void Initialize(Transform start, Vector2 end)
        {
            _points.Add(start);
            addRotaionPoint(end);
        }

        public void Terminate()
        {
            for (int i = 1; i < _points.Count; i++)
            {
                Destroy(_points[i].gameObject);
            }
            _points.Clear();
        }

        public Vector3[] GetAnchorsPositions()
        {
            Vector3[] result = new Vector3[_points.Count];
            for (int i = 0; i < _points.Count; i++)
            {
                result[i] = _points[i].position;
            }
            return result;
        }
    }
}