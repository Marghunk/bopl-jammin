using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSpellInfo : MonoBehaviour
{
    public List<Element> heldElements = new List<Element>() { Element.FireElement() };

    public GameObject a, b, c;

    [SerializeField]
    CanvasScript canvasS;

    [SerializeField ]
    bool chaos = false;

    public bool SetChaos
    {
        get { return chaos; }
        set { chaos = value;
            StartCoroutine(ChaosTime(3f));
        }
    }

    IEnumerator ChaosTime(float f)
    {
        while (f > 0)
        {
            f -= Time.deltaTime;
            yield return null;
        }
        chaos = false;
        yield return null;
    }

    private void Start()
    {
        canvasS = GameObject.FindFirstObjectByType<CanvasScript>();
    }

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
                case Element.elementEnum.Chaos:
                    break;
                case Element.elementEnum.Electric:
                    Thunderbolt(dir);
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
                case Element.elementEnum.Electric:
                    switch (heldElements[1].elementName)
                    {
                        case Element.elementEnum.Electric:
                            Thunderbolt(dir, 2);
                            break;
                        case Element.elementEnum.Fire:
                            break;
                        case Element.elementEnum.Nature:
                            break;
                        case Element.elementEnum.Water:
                            break;
                    }
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
                    switch (heldElements[1].elementName)
                    {
                        case Element.elementEnum.Electric:
                            break;
                        case Element.elementEnum.Fire:
                            break;
                        case Element.elementEnum.Nature:
                            break;
                        case Element.elementEnum.Water:                            
                            break;
                    }
                    break;
                case Element.elementEnum.Water:
                    switch (heldElements[1].elementName)
                    {
                        case Element.elementEnum.Electric:
                            break;
                        case Element.elementEnum.Fire:
                            break;
                        case Element.elementEnum.Nature:
                            break;
                        case Element.elementEnum.Water:
                            Waterball(dir, 2);
                            break;
                    }
                    break;
                default:
                    break;
            }
            heldElements.RemoveAt(1);
            heldElements.RemoveAt(0);
        }
    }

    private void Thunderbolt(Vector2 dir, int level = 1)
    {
        Vector2 up = -GetComponent<ShootScript>().gravity.DirectionToNearestGround();
        GameObject g = Instantiate(Resources.Load("Thunderball") as GameObject, (transform.position + (Vector3)up * 0.3f) + ((Vector3)dir * 0.7f), Quaternion.identity);
        g.GetComponent<Thunderball>().evo = level;
        g.GetComponent<Rigidbody2D>().velocity = dir * 7.5f;
    }

    private void Waterball(Vector2 dir, int level = 1)
    {
        Vector2 up = -GetComponent<ShootScript>().gravity.DirectionToNearestGround();
        GameObject g = Instantiate(Resources.Load("Waterball") as GameObject, (transform.position + (Vector3)up * 0.3f) + ((Vector3)dir * 0.7f), Quaternion.identity);
        g.GetComponent<Waterball>().evo = level;
        g.GetComponent<Rigidbody2D>().velocity = dir * 7.5f;

    }

    private void Fireball(Vector2 dir, int level = 1)
    {
        Vector2 up = -GetComponent<ShootScript>().gravity.DirectionToNearestGround(); 
        GameObject g = Instantiate(Resources.Load("Fireball") as GameObject, (transform.position+(Vector3)up * 0.3f) + ((Vector3)dir * 0.7f), Quaternion.identity);
        g.GetComponent<FireballScript>().evo = level;
        g.GetComponent<Rigidbody2D>().velocity = dir * 7.5f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Electric")
        {
            Debug.Log(collision.name);
            //Debug.Log();
            canvasS.TallyScore(transform.GetComponent<Inputs>().zoop);
            Debug.Log("ZAP!");
            Destroy(gameObject);
        }
    }
}



[System.Serializable]
public class Element
{
    public enum elementEnum { Chaos, Electric, Fire, Water, Nature }
    public elementEnum elementName = elementEnum.Fire;

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
