using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementSpawner : MonoBehaviour
{
    public List<Transform> eleSpawns = new List<Transform>();
    public float[] cds;

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            eleSpawns.Add(transform.GetChild(i));
        }
        cds = new float[eleSpawns.Count];
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
                    g.GetComponent<ElementPickup>().element = (Element.elementEnum)Random.Range(1,4);

                }
               
            }

            f = 5f;
        }

        
    }
}
