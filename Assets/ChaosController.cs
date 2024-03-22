using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class ChaosController : MonoBehaviour
{
    public DiscoballScrip[] discoScripts;

    public DanceFloorScript[] dfScripts;

    public Color inactive, active;

    public Light[] spotLights;

    public Color discoColor, unDiscoColor;

    private void Start()
    {
        dfScripts = GameObject.FindObjectsOfType<DanceFloorScript>();
    }

    public void StartTheDisco(bool b)
    {
        if (b)
        {
            discoScripts[0].speed = 70;

            for (int i = 1; i < discoScripts.Length; i++)
            {
                discoScripts[i].speed = Mathf.Sign(discoScripts[i].speed) * 45;
            }

            for (int i = 0; i < dfScripts.Length; i++)
            {
                dfScripts[i].partyStarted = true;
                dfScripts[i].f = 0;
                dfScripts[i].cooldown = 1f;
            }

            for (int i = 0; i < spotLights.Length; i++)
            {
                spotLights[i].color = active;
            }

            Camera.main.backgroundColor = discoColor;
            StartCoroutine(CheckForFail());
        }
        else
        {
            discoScripts[0].speed = 2.5f;

            for (int i = 1; i < discoScripts.Length; i++)
            {
                discoScripts[i].speed = Mathf.Sign(discoScripts[i].speed) * 5;
            }

            for (int i = 0; i < dfScripts.Length; i++)
            {
                dfScripts[i].partyStarted = false;
                dfScripts[i].f = 0;
                dfScripts[i].cooldown = 3f;
            }

            for (int i = 0; i < spotLights.Length; i++)
            {
                spotLights[i].color = inactive;
            }

            Camera.main.backgroundColor = unDiscoColor;
        }
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            StartTheDisco(true);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartTheDisco(false);
        }
    }

    IEnumerator CheckForFail()
    {
        bool b = true;

        while (b)
        {
            float f = 1;
            while (f > 0)
            {
                f -= Time.deltaTime;
                yield return null;
            }
            b = false;
            foreach (var item in GameObject.FindObjectsOfType<PlayerSpellInfo>())
            {
                if (item.SetChaos == true)
                {
                    b = true;
                }
            }
            yield return null;
        }


        StartTheDisco(false);
        yield return null;
    }

}
