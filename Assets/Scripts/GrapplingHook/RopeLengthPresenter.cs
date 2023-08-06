using UnityEngine;

namespace GrapplingHook
{
    public class RopeLengthPresenter : MonoBehaviour
    {
        [SerializeField] private float _ropeLength;

        private void OnEnable()
        {
            Rope.RopeLengthChanged += OnRopeLengthChanged;
        }

        private void OnDisable()
        {
            Rope.RopeLengthChanged += OnRopeLengthChanged;
        }

        private void OnRopeLengthChanged(float ropeLength)
        {
            _ropeLength = ropeLength;
        }
    }
}