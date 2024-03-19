using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Thunderball : MonoBehaviour
{
    public float life = 15f;
    public float powerscale = 1;
    public int evo = 1;

    bool stopIt = false;

    public TrailRenderer tr;
    public EdgeCollider2D edge;

    private void Start()
    {
        GameObject g = Instantiate(Resources.Load("Thundertrail") as GameObject, Vector3.zero, Quaternion.identity);
        edge = g.GetComponent<EdgeCollider2D>();
        transform.GetChild(0).gameObject.SetActive(true);
    }
    private void Update()
    {
        if (life > 0)
        {
            life -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }

        if (evo == 2)
        {
            SetColliderPointsFromTrail(tr, edge);
        }

    }

    private void SetColliderPointsFromTrail(TrailRenderer tr, EdgeCollider2D edge)
    {
        List<Vector2> points = new List<Vector2>();
        for (int i = 0; i < tr.positionCount; i++)
        {
            points.Add(tr.GetPosition(i));
        }
        edge.SetPoints(points);
    }

    List<Collider2D> collected = new List<Collider2D>(); // WARNING !!!! RECURSIVE !!!!

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            GameObject.FindFirstObjectByType<CanvasScript>().TallyScore((int)collision.transform.GetComponent<Inputs>().zoop);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

        if (!stopIt)
        {
            Conduction(transform);

            if (evo == 2)
            {
                ThunderStrike();
                

            }

            Destroy(gameObject);
            stopIt = true;
        }

    }

    private void ThunderStrike()
    {
        LineRenderer lr = edge.GetComponent<LineRenderer>();

        List<Vector2> fuck = new List<Vector2>();
        for (int i = 0; i < edge.pointCount; i++)
        {
            Vector2 rando = new Vector2(Random.Range(0, 0.25f), Random.Range(0, 0.25f));
            fuck.Add(edge.points[i] + rando);
            //edge.points[0] = Vector2.zero; //edge.points[i] + rando; //SetPosition(i, lr.GetPosition(i) + new Vector3(rando.x, rando.y, 0));
        }
        edge.SetPoints(fuck);

        static Vector3 getv3(Vector2 v)
        {
            return new Vector3(v.x,v.y,0);
        }

        Vector3[] arr = System.Array.ConvertAll<Vector2, Vector3>(edge.points, getv3);

        lr.positionCount = edge.pointCount;

        

        lr.SetPositions(arr);

        

        edge.gameObject.layer = 0;
    }

    void Conduction(Transform thing)
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(thing.position, 1);

        foreach (var item in cols)
        {
            if (item.tag == "Water")
            {
                if (item.GetComponent<SpriteRenderer>() && !collected.Contains(item))
                {
                    collected.Add(item);
                    item.tag = "Electric";
                    item.transform.GetChild(0).tag = "Electric";
                    item.SendMessage("Electrify", 5f);
                    Conduction(item.transform);
                }
            }
        }
    }

    IEnumerator ShitFire(float f)
    {
        int health = 4;
        float v = f;
        Vector2 pos = transform.position;
        while (v > 0)
        {
            if (Vector3.Distance(transform.position, pos) > 1)
            {
                GameObject g = Instantiate(Resources.Load("Flame") as GameObject, transform.position, Quaternion.identity);
                g.GetComponent<FlameScript>().lifetime *= 3;
                health--;
                if (health <= 0)
                {
                    Destroy(gameObject);
                }
                pos = transform.position;
            }
            v -= Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
        yield return null;
    }
}
