using UnityEngine;

namespace GrapplingHook
{
    public class GrapplingHookRaycaster : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;

        public RaycastHit2D Raycast(Vector3 origin, Vector3 direction, float distance)
        {
            return Physics2D.Raycast(origin, direction, distance, _layerMask);
        }
    }
}