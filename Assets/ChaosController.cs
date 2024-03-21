using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosController : MonoBehaviour
{
    public DiscoballScrip[] discoScripts;

    DanceFloorScript[] dfScripts;

    public Color inactive, active;

    private void Start()
    {
        dfScripts = GameObject.FindObjectsOfType<DanceFloorScript>();
    }

    void StartTheDisco()
    {
        discoScripts[0].speed = discoScripts[0].speed * 400;

        for (int i = 1; i < discoScripts.Length; i++)
        {
            discoScripts[i].speed = discoScripts[i].speed * 200;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            StartTheDisco();
        }
    }

}
