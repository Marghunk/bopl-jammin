using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DanceFloorScript : MonoBehaviour
{
    public Color[] discoColors;
    public bool partyStarted = false;

    public float cooldown = 3;
    public float f;

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
            if (partyStarted)
            {
                switch (Random.Range(0,2))
                {
                    case 0:
                        GetComponent<Renderer>().material.SetColor("_EmissionColor", discoColors[Random.Range(0, discoColors.Length)] * 1f);
                        break;
                    case 1:
                        GetComponent<Renderer>().material.SetColor("_EmissionColor", discoColors[Random.Range(0, discoColors.Length)] * 0.3f);
                        break;
                }                
            }
            else
            {
                GetComponent<Renderer>().material.SetColor("_EmissionColor", discoColors[Random.Range(0, discoColors.Length)] * 0.3f);
            }
            
            f = cooldown;
        }
    }

}
