using UnityEngine;

namespace GrapplingHook
{
    public class AnchorPresenter : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(gameObject.transform.position, 0.1f);
        }
    }
}