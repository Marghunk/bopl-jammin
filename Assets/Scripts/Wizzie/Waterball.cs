using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterball : MonoBehaviour
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Fire"))
        {
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Player")
        {
            Vector3 v = (collision.transform.position - transform.position).normalized;
            collision.transform.GetComponent<Rigidbody2D>().velocity = v * 10;
        }
        if (!stopIt)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            GameObject g = Instantiate(Resources.Load("Waterpack") as GameObject, transform.position, Quaternion.identity);
            if (evo == 2)
            {
                gameObject.layer = 8;
                StartCoroutine(ShitWater(2));
                //g.GetComponent<FlameScript>().lifetime *= 3;
            }
            else
            {
                Destroy(gameObject);
            }

            //Destroy(gameObject);
            stopIt = true;
        }

    }

    IEnumerator ShitWater(float f)
    {
        int health = 5;
        float v = f;
        Vector2 pos = transform.position;
        for (int i = 0; i < health; i++)
        {
            GameObject g = Instantiate(Resources.Load("Waterpack2") as GameObject, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.3f);
        }
        Destroy(gameObject);
        yield return null;
    }
}
