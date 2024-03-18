using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Code for the triangle connected to the player.
/// </summary>
public class StupidTriangle : MonoBehaviour
{
    private void Update()
    {
        Vector3 look = transform.InverseTransformPoint(transform.parent.position);
        float angle = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg +  90;

        transform.Rotate(0, 0, angle);
    }
}
