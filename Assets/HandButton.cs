using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandButton : MonoBehaviour
{

    public char keyLetter;
    Text text;
    GameObject DataStore;
    WaitForSeconds switchDelay = new WaitForSeconds(0.1f);

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<Text>();
        DataStore = GameObject.Find("DataStorage");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(CheckMouseClick());
        }
            switch (gameObject.name)
        {
            case "Player1Hand":
                if (DataStore.GetComponent<DataStore>().player1Right == true)
                {
                    keyLetter = 'R';
                } else
                {
                    keyLetter = 'L';
                }
                break;
            case "Player2Hand":
                if (DataStore.GetComponent<DataStore>().player2Right == true)
                {
                    keyLetter = 'R';
                }
                else
                {
                    keyLetter = 'L';
                }
                break;
            case "Player3Hand":
                if (DataStore.GetComponent<DataStore>().player3Right == true)
                {
                    keyLetter = 'R';
                }
                else
                {
                    keyLetter = 'L';
                }
                break;
            case "Player4Hand":
                if (DataStore.GetComponent<DataStore>().player4Right == true)
                {
                    keyLetter = 'R';
                }
                else
                {
                    keyLetter = 'L';
                }
                break;
        }
        text.text = "" + keyLetter;
    }

    IEnumerator CheckMouseClick()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null && hit.collider.name == "Player1Hand" && gameObject.name == "Player1Hand")
        {
            StartCoroutine(ToggleHand(1));
            yield return switchDelay;
        }
        if (hit.collider != null && hit.collider.name == "Player2Hand" && gameObject.name == "Player2Hand")
        {
            StartCoroutine(ToggleHand(2));
            yield return switchDelay;
        }
        if (hit.collider != null && hit.collider.name == "Player3Hand" && gameObject.name == "Player3Hand")
        {
            StartCoroutine(ToggleHand(3));
            yield return switchDelay;
        }
        if (hit.collider != null && hit.collider.name == "Player4Hand" && gameObject.name == "Player4Hand")
        {
            StartCoroutine(ToggleHand(4));
            yield return switchDelay;
        }
    }

    IEnumerator ToggleHand(int player)
    {
        switch (player)
        {
            case 1:
                if (DataStore.GetComponent<DataStore>().player1Right == true)
                {
                    Debug.Log("Switch to L1");
                    DataStore.GetComponent<DataStore>().player1Right = false;
                    yield return switchDelay;
                    break;
                }
                else
                {
                    Debug.Log("Switch to R1");
                    DataStore.GetComponent<DataStore>().player1Right = true;
                    yield return switchDelay;
                    break;
                }
            case 2:
                if (DataStore.GetComponent<DataStore>().player2Right == true)
                {
                    Debug.Log("Switch to L2");
                    DataStore.GetComponent<DataStore>().player2Right = false;
                    yield return switchDelay;
                }
                else
                {
                    Debug.Log("Switch to R2");
                    DataStore.GetComponent<DataStore>().player2Right = true;
                    yield return switchDelay;
                }
                break;
            case 3:
                if (DataStore.GetComponent<DataStore>().player3Right == true)
                {
                    Debug.Log("Switch to L3");
                    DataStore.GetComponent<DataStore>().player3Right = false;
                    yield return switchDelay;
                }
                else
                {
                    Debug.Log("Switch to R3");
                    DataStore.GetComponent<DataStore>().player3Right = true;
                    yield return switchDelay;
                }
                break;
            case 4:
                if (DataStore.GetComponent<DataStore>().player4Right == true)
                {
                    Debug.Log("Switch to L4");
                    DataStore.GetComponent<DataStore>().player4Right = false;
                    yield return switchDelay;
                }
                else
                {
                    Debug.Log("Switch to R4");
                    DataStore.GetComponent<DataStore>().player4Right = true;
                    yield return switchDelay;
                }
                break;

        }
    }
}
