using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementSpawner : MonoBehaviour
{
    public List<Transform> eleSpawns = new List<Transform>();
    public float[] cds;

    public Transform[] specialSpawns;

    public List<Element.elementEnum> weightedSpawn = new List<Element.elementEnum>();

    bool kink = false;
    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            eleSpawns.Add(transform.GetChild(i));
        }
        cds = new float[eleSpawns.Count];

        GameObject g = Instantiate(Resources.Load("Element") as GameObject, specialSpawns[Random.Range(0,2)]);
        g.GetComponent<ElementPickup>().element = (Element.elementEnum.Chaos);
    }

    float f = 5f;
    private void Update()
    {
        if (f > 0)
        {
            f -= Time.deltaTime;
        }
        else
        {
            for (int i = 0; i < eleSpawns.Count; i++)
            {
                if (eleSpawns[i].childCount == 0)
                {
                    GameObject g = Instantiate(Resources.Load("Element") as GameObject, eleSpawns[i]);
                    int rand = Random.Range(0, weightedSpawn.Count);
                    g.GetComponent<ElementPickup>().element = weightedSpawn[rand]; //(Element.elementEnum)Random.Range(1,4);
                    weightedSpawn.RemoveAt(rand);

                }
               
            }

            f = 5f;
        }

        if (!kink)
        {
            bool b = true;
            for (int i = 0; i < specialSpawns.Length; i++)
            {
                if (specialSpawns[i].childCount > 0)
                {
                    b = false;
                }
            }

            if (b == true)
            {
                Debug.Log("Chaos respawning");
                kink = true;
                StartCoroutine(CooldownToChaos(15));
            }
        }
    }

    IEnumerator CooldownToChaos(float f = 15)
    {
        yield return new WaitForSeconds(f);
        GameObject g = Instantiate(Resources.Load("Element") as GameObject, specialSpawns[Random.Range(0, 2)]);
        g.GetComponent<ElementPickup>().element = (Element.elementEnum.Chaos);
        kink = false;
        yield return null;
    }
}
