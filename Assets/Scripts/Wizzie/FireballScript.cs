using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballScript : MonoBehaviour
{
    public float life = 15f;
    public float powerscale = 1;
    public int evo = 1;

    bool stopIt = false;

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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!stopIt)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            GameObject g = Instantiate(Resources.Load("Flame") as GameObject, transform.position, Quaternion.identity);
            if (evo == 2)
            {
                StartCoroutine(ShitFire(2));
                g.GetComponent<FlameScript>().lifetime *= 3;
            }
            else
            {
                Destroy(gameObject);
            }

            //Destroy(gameObject);
            stopIt = true;
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
