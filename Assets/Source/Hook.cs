using UnityEngine;

namespace Source
{
    public class Hook : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _body;
        [SerializeField] private float _speed;
        [SerializeField] private GameObject _target;
        [SerializeField] private float _maxDistance;
        public float MaxDistance => _maxDistance;

        private DistanceJoint2D _joint;
        private IInput _input = new DesktopInput();

        private void Start()
        {
            _joint = gameObject.GetComponent<DistanceJoint2D>();
        }

        private void Update()
        {
            //Vector2 direction = _target.position - _body.transform.position;
            //direction.Normalize();
            //Vector2 movement = direction * _speed * _input.GrapplingHookMove();
            //if (movement.magnitude > 0)
            //{
            //    _body.MovePosition(_body.position + movement);
            //}

            float movement = _speed * _input.GrapplingHookMove() * Time.deltaTime;
            _joint.distance = Mathf.Clamp(_joint.distance - movement, 0, _maxDistance);

        }

        public void SetTarget(Vector2 point) {
            _target.GetComponent<Transform>().position = point;
            _joint.distance = Vector2.Distance(point, _body.position);
            _joint.enabled = true;
        }

        public void ResetTerget()
        {
            _joint.enabled = false;
        }
    }
}
