using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThundertrailScript : MonoBehaviour
{
    public bool cd = false;
    public float lifetime = 3f;


    private void Update()
    {
        if (cd)
        {
            if (lifetime > 0)
            {
                lifetime -= Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
