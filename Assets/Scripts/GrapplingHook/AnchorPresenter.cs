using UnityEngine;

namespace Source.GrapplingHook3
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