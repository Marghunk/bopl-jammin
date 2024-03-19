using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class WaterScript : MonoBehaviour
{
    bool b;

    public void Electrify(float t)
    {
        GetComponent<SpriteRenderer>().color = Color.yellow;
        StartCoroutine(DoElectrify(t));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Fire")
        {
            //GameObject.FindFirstObjectByType<CanvasScript>().TallyScore((int)collision.transform.GetComponent<Inputs>().gamepadIndex);
            Destroy(collision.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Fire" && collision.transform.GetComponent<FireballScript>()?.evo == 2)
        {
            //GameObject.FindFirstObjectByType<CanvasScript>().TallyScore((int)collision.transform.GetComponent<Inputs>().gamepadIndex);
            Destroy(gameObject);
        }
    }

    IEnumerator DoElectrify(float t)
    {
        while (t > 0)
        {
            t -= Time.deltaTime;

            foreach (var item in Physics2D.OverlapCircleAll(transform.position, 1))
            {
                if (item.tag == "Water")
                {
                    if (item.GetComponent<SpriteRenderer>())
                    {
                        item.tag = "Electric";
                        item.transform.GetChild(0).tag = "Electric";                        
                        item.SendMessage("Electrify", 5f);
                    }
                }
            }

            yield return null;
        }
        Destroy(gameObject);
        yield return null;
    }
}
