using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ElementPickup : MonoBehaviour
{
    public Element.elementEnum element = Element.elementEnum.Fire;
    Color c;

    private void Start()
    {
        switch (element)
        {
            case Element.elementEnum.Null:
                break;
            case Element.elementEnum.Electric:
                c = Color.yellow;
                break;
            case Element.elementEnum.Fire:
                c = Color.red;
                break;
            case Element.elementEnum.Nature:
                c = Color.green;
                break;
            case Element.elementEnum.Water:
                c = Color.blue;
                break;
            default:
                break;
        }
        Color d = c;
        d.a = 0.33f;
        transform.GetChild(0).GetComponent<Renderer>().material.color = d;
        //transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material.color = c;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (collision.transform.GetComponent<PlayerSpellInfo>().heldElements.Count < 2)
            {
                collision.transform.GetComponent<PlayerSpellInfo>().heldElements.Add(new Element(element, c));
            }
            
        }
    }
}
