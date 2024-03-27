using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ElementPickup : MonoBehaviour
{
    public Element.elementEnum element = Element.elementEnum.Fire;
    Color c;
    Color cAlt;

    public GameObject outerCircle, innerCircle;


    public Color[] colors;
    public Color[] colorsAlt;

    private void Start()
    {
        switch (element)
        {
            case Element.elementEnum.Chaos:
                c = colors[0];
                cAlt = colorsAlt[0];
                break;
            case Element.elementEnum.Electric:
                c = colors[1];
                cAlt = colorsAlt[1];
                break;
            case Element.elementEnum.Fire:
                c = colors[2];
                cAlt = colorsAlt[2];
                break;
            case Element.elementEnum.Water:
                c = colors[3];
                cAlt = colorsAlt[3];
                break;
            case Element.elementEnum.Nature:
                c = colors[4];
                cAlt = colorsAlt[4];
                break;
            default:
                break;
        }
        Color d = cAlt;
        //d.a = 0.1f;

        innerCircle.GetComponent<Renderer>().material.SetColor("_EmissionColor", c);
        innerCircle.GetComponent<Renderer>().material.color = c;
        
        outerCircle.GetComponent<Renderer>().material.SetColor("_EmissionColor", d);
        outerCircle.GetComponent<Renderer>().material.color = d;
        
        //transform.GetChild(0).GetComponent<Renderer>().material.color = d;
        //transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material.color = c;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (element == Element.elementEnum.Chaos)
            {
                collision.transform.GetComponent<PlayerSpellInfo>().SetChaos = true;
                Destroy(gameObject);
            }
            else
            if (collision.transform.GetComponent<PlayerSpellInfo>().heldElements.Count < 2)
            {
                if (collision.transform.GetComponent<PlayerSpellInfo>().heldElements.Count == 0 || collision.transform.GetComponent<PlayerSpellInfo>().heldElements[0].elementName == element)
                {
                    collision.transform.GetComponent<PlayerSpellInfo>().heldElements.Add(new Element(element, c));
                    GameObject.FindObjectOfType<ElementSpawner>().weightedSpawn.Add(element);
                    Destroy(gameObject);
                }
            }
        }
    }
}
