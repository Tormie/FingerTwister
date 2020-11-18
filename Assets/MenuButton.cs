using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public char keyLetter;
    Text text;
    Color player1color = Color.red;
    Color player2color = Color.blue;
    Color player3color = Color.green;
    Color player4color = Color.yellow;

    int playerAmount;
    public Color keyColor;
    SpriteRenderer sr;
    GameObject DataStore;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<Text>();
        DataStore = GameObject.Find("DataStorage");
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "" + keyLetter;
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && hit.collider.name == "Key1Players")
            {
                DataStore.GetComponent<DataStore>().playerAmount = 1;
                Debug.Log("HIT");
            }
            if (hit.collider != null && hit.collider.name == "Key2Players")
            {
                DataStore.GetComponent<DataStore>().playerAmount = 2;
                Debug.Log("HIT");
            }
            if (hit.collider != null && hit.collider.name == "Key3Players")
            {
                DataStore.GetComponent<DataStore>().playerAmount = 3;
                Debug.Log("HIT");
            }
            if (hit.collider != null && hit.collider.name == "Key4Players")
            {
                DataStore.GetComponent<DataStore>().playerAmount = 4;
                Debug.Log("HIT");
            }

        }
        playerAmount = DataStore.GetComponent<DataStore>().playerAmount;
        keyColor = Color.white;
        if (gameObject.name == "Key1Players" && playerAmount == 1)
        {
            keyColor = player1color;
        }
        if (gameObject.name == "Key2Players" && playerAmount == 2)
        {
            keyColor = player2color;
        }
        if (gameObject.name == "Key3Players" && playerAmount == 3)
        {
            keyColor = player3color;
        }
        if (gameObject.name == "Key4Players" && playerAmount == 4)
        {
            keyColor = player4color;
        }
        sr.color = keyColor;
    }

}
