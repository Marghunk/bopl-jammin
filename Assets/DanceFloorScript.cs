using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DanceFloorScript : MonoBehaviour
{
    public Color[] discoColors;
    public bool partyStarted = false;

    public float cooldown = 3;
    float f;

    private void Awake()
    {
        f = cooldown;
        GetComponent<Renderer>().material.SetColor("_EmissionColor", discoColors[Random.Range(0, discoColors.Length)] * 0.3f);
    }
    private void Update()
    {
        if (f > 0)
        {
            f -= Time.deltaTime;
        }
        else
        {
            GetComponent<Renderer>().material.SetColor("_EmissionColor", discoColors[Random.Range(0, discoColors.Length)] * 0.3f); ;
            f = cooldown;
        }
    }

}
