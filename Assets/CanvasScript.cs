using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEditor.Progress;

public class CanvasScript : MonoBehaviour
{
    public List<CharData> characters = new List<CharData>();
    public List<GameObject> avatarz = new List<GameObject>();

    public GameObject avatarUI;


    public void TallyScore(int gamepadIndex, bool KillorDie = false)
    {
        int j;
        for (int i = 0; i < characters.Count; i++)
        {
            if (characters[i].id == gamepadIndex)
            {
                characters[i].deaths++;

                avatarz[i].GetComponentInChildren<TMP_Text>().text = $"{characters[i].deaths} deaths";
            }
        }
        

        
        Debug.Log(gamepadIndex);
    }

    public void CreateCharAvatar(int playerId, GameObject player)
    {               

        foreach (var item in characters)
        {
            if (item.id == playerId)
            {
                return;
            }
        }
        GameObject g = (Instantiate(avatarUI, transform));
        g.transform.GetComponent<RectTransform>().localPosition += (Vector3.up * 70) * avatarz.Count;
        characters.Add(new CharData(player.GetComponent<Renderer>().material.color, playerId));
        avatarz.Add(g);


        g.transform.GetChild(0).GetComponent<Image>().color = characters[characters.Count -1].charColor;
    }

    [System.Serializable]
    public class CharData
    {
        public Color charColor;
        public int id;
        public int kills, deaths;
        public CharData(Color c, int Id)
        {
            charColor = c;
            id = Id;
        }
    }
}
