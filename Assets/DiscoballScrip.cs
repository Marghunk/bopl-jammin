using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoballScrip : MonoBehaviour
{
    public float speed = 10f;


    private void Update()
    {
        transform.Rotate(Vector3.forward, speed * Time.deltaTime);
    }
}
