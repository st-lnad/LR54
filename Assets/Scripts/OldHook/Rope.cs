using System.Collections.Generic;
using UnityEngine;

namespace Source.OldHook
{
    public class Rope : MonoBehaviour
    {

        [SerializeField] private GameObject _start;
        [SerializeField] private GameObject _end;
        [SerializeField] private LayerMask _layers;
        [SerializeField] private Points _points;
        [SerializeField] private GameObject _anchorPrefab;
        private Ray _ray = new Ray();

        private void OnEnable()
        {
            _points = new Points(_start.transform, _end.transform);
        }

        private void Update()
        {
            if (_points.Length > 2)
            {
                if (checkStayOldLet(_points.Start.position, _points.PreviousRotationPoint.position) == false)
                {
                    _points.deleteRotationPoint();
                }
            }
            var mbHit = checkForNewLet(_points.Start.position, _points.RotationPoint.position);
            if (mbHit.Length > 0)
            {
                var position = mbHit[0].point;
                GameObject anchorClone = Instantiate(_anchorPrefab, position, Quaternion.AngleAxis(0, new Vector3(0, 0, 0)));
                _points.addPoint(anchorClone.transform);
                Debug.Log("New Anchor");
            }
        }
        private RaycastHit2D[] checkForNewLet(Vector2 start, Vector2 rotationPoint)
        {
            var direction = rotationPoint - start;
            _ray = new Ray(start, direction);
            var distanse = Vector2.Distance(rotationPoint, start);
            var hit = Physics2D.Raycast(_ray.origin, _ray.direction, distanse - 0.1f, _layers);
            if (hit)
            {
                return new RaycastHit2D[] { hit };
            }
            return new RaycastHit2D[] { };
        }
        private bool checkStayOldLet(Vector2 start, Vector2 oldPoint)
        {
            var direction = oldPoint - start;
            _ray = new Ray(start, direction);
            var distanse = Vector2.Distance(oldPoint, start);
            var hit = Physics2D.Raycast(_ray.origin, _ray.direction, distanse - 0.1f, _layers);
            if (hit)
            {
                return true;
            }
            return false;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(_ray);
            foreach (var point in _points.points)
            {
                Gizmos.DrawSphere(point.position, 0.1f);
            }
        }



    }

    class Points
    {
        public List<Transform> points = new List<Transform>();
        public Transform RotationPoint => points[1];
        public Transform PreviousRotationPoint
        {
            get { return ((points.Count >= 2) ? points[2] : null); }
        }
        public Transform Start => points[0];
        public int Length => points.Count;
        private Points() { }
        public Points(Transform start, Transform end)
        {
            points = new List<Transform>();
            points.Add(start);
            points.Add(end);
        }

        public void addPoint(Transform point)
        {
            points.Insert(1, point);
        }

        public void deleteRotationPoint()
        {
            points.RemoveAt(1);
        }
    }
}