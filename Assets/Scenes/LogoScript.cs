using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogoScript : MonoBehaviour
{
    public RawImage vid;
    public float countdownA = 1;
    public float countdownB = 3;

    bool a = true, b = true;


    public TMP_Text textbox;
    public GameObject fadeimage;

    private void Update()
    {
        if(countdownA > 0 || countdownB > 0)
        {
            countdownA -= Time.deltaTime;
            countdownB -= Time.deltaTime;
        }

        if(countdownA < 0 && a)
        {
            StartCoroutine(Fadeobject(textbox.gameObject));
            //StartCoroutine(Fadeobject(textbox.gameObject, true));
            a = false;
        }
        if(countdownB < 0 && b)
        {
            StartCoroutine(Fadeobject(fadeimage.gameObject, true));
            b = false;
        }
    }


    IEnumerator Fadeobject(GameObject a, bool geddout = false)
    {
        CanvasGroup cg = a.GetComponent<CanvasGroup>();

        if (true)
        {
            while (cg.alpha < 1)
            {
                cg.alpha += Time.deltaTime;
                yield return null;
            }
        } else
        {
            //while (cg.alpha > 0)
            //{
            //    cg.alpha -= Time.deltaTime;
            //    yield return null;
            //}
        }

        if (geddout)
        {
            SceneManager.LoadSceneAsync("Menu");
        }

        yield return null;
    }

}
