using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerSpellInfo : MonoBehaviour
{
    public List<Element> heldElements = new List<Element>() { Element.FireElement() };

    public GameObject a, b, c;

    private void Update()
    {
        if (heldElements.Count == 0)
        {
            a.SetActive(false);
            b.SetActive(false);
            c.SetActive(false);
            
        }
        else if (heldElements.Count == 1)
        {
            a.SetActive(true);
            a.GetComponentInChildren<SpriteRenderer>().color = heldElements[0].elementalColor;
            b.SetActive(false);
            c.SetActive(false);
            
        }
        else if (heldElements.Count == 2)
        {
            a.SetActive(false);
            b.SetActive(true);
            c.SetActive(true);
            b.GetComponentInChildren<SpriteRenderer>().color = heldElements[0].elementalColor;
            c.GetComponentInChildren<SpriteRenderer>().color = heldElements[1].elementalColor;
        }
    }

    public void CastSpell(Vector2 dir)
    {
        heldElements.OrderBy(x => x.elementName);

        if (heldElements.Count == 1)
        {
            switch (heldElements[0].elementName)
            {
                case Element.elementEnum.Null:
                    break;
                case Element.elementEnum.Electric:
                    break;
                case Element.elementEnum.Fire:
                    Fireball(dir);
                    break;
                case Element.elementEnum.Nature:
                    break;
                case Element.elementEnum.Water:
                    Waterball(dir);
                    break;
                default:
                    break;
            }
            heldElements.RemoveAt(0);
        }
        else if (heldElements.Count == 2)
        {
            switch (heldElements[0].elementName)
            {
                case Element.elementEnum.Null:
                    break;
                case Element.elementEnum.Electric:
                    break;
                case Element.elementEnum.Fire:
                    switch (heldElements[1].elementName)
                    {
                        case Element.elementEnum.Electric:
                            break;
                        case Element.elementEnum.Fire:
                            Fireball(dir, 2);
                            break;
                        case Element.elementEnum.Nature:
                            break;
                        case Element.elementEnum.Water:
                            break;
                    }
                    break;
                case Element.elementEnum.Nature:
                    break;
                case Element.elementEnum.Water:
                    break;
                default:
                    break;
            }
            heldElements.RemoveAt(1);
            heldElements.RemoveAt(0);
        }
    }

    private void Waterball(Vector2 dir)
    {
        Vector2 up = -GetComponent<ShootScript>().gravity.DirectionToNearestGround();
        GameObject g = Instantiate(Resources.Load("Waterball") as GameObject, (transform.position + (Vector3)up * 0.3f) + ((Vector3)dir * 0.7f), Quaternion.identity);
        g.GetComponent<Rigidbody2D>().velocity = dir * 7.5f;
    }

    private void Fireball(Vector2 dir, int level = 1)
    {
        Vector2 up = -GetComponent<ShootScript>().gravity.DirectionToNearestGround(); 
        GameObject g = Instantiate(Resources.Load("Fireball") as GameObject, (transform.position+(Vector3)up * 0.3f) + ((Vector3)dir * 0.7f), Quaternion.identity);
        g.GetComponent<FireballScript>().evo = level;
        g.GetComponent<Rigidbody2D>().velocity = dir * 7.5f;
    }
}



[System.Serializable]
public class Element
{
    public enum elementEnum { Null, Electric, Fire, Nature, Water }
    public elementEnum elementName = elementEnum.Null;

    public Sprite GetIcon()
    {
        return Resources.Load<Sprite>(elementName.ToString());
    }

    public Color elementalColor;


    public Element(elementEnum ElementName, Color ElementalColor)
    {
        elementName = ElementName;
        elementalColor = ElementalColor;
    }

    public static Element FireElement()
    {
        return new Element(elementEnum.Fire, new Color(1, 0.2f, 0));
    }
    public static Element WaterElement()
    {
        return new Element(elementEnum.Water, new Color(0, 0.5803922f, 1));
    }
    public static Element NatureElement()
    {
        return new Element(elementEnum.Nature, new Color(0.5607843f, 0.4352941f, 0.227451f, 1));
    }
    public static Element ElectricElement()
    {
        return new Element(elementEnum.Electric, new Color(1, 0.9712542f, 0.5113207f, 1));
    }
}
