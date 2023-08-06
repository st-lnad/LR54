using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorPresenter : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(gameObject.transform.position, 0.1f);
    }
}
