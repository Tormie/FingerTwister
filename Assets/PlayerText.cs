using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerText : MonoBehaviour
{
    public string textString;
    public bool righty;
    Text text;
    GameObject DataStore;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<Text>();
        DataStore = GameObject.Find("DataStorage");
        switch (gameObject.name)
        {
            case "Player1Text":
                righty = DataStore.GetComponent<DataStore>().player1Right;
                break;
            case "Player2Text":
                righty = DataStore.GetComponent<DataStore>().player2Right;
                break;
            case "Player3Text":
                righty = DataStore.GetComponent<DataStore>().player3Right;
                break;
            case "Player4Text":
                righty = DataStore.GetComponent<DataStore>().player4Right;
                break;
        }
        SetText();
    }

        // Update is called once per frame
        void Update()
    {
        
    }

    public void SetText()
    {
        switch (gameObject.name)
        {
            case "Player1Text":
                if (righty)
                {
                    textString = "Player 1 - Righty \nScore: " + DataStore.GetComponent<DataStore>().player1score;
                }
                else
                {
                    textString = "Player 1 - Lefty \nScore: " + DataStore.GetComponent<DataStore>().player1score;
                }
                break;
            case "Player2Text":
                if (righty)
                {
                    textString = "Player 2 - Righty \nScore: " + DataStore.GetComponent<DataStore>().player2score;
                }
                else
                {
                    textString = "Player 2 - Lefty \nScore: " + DataStore.GetComponent<DataStore>().player2score;
                }
                break;
            case "Player3Text":
                if (righty)
                {
                    textString = "Player 3 - Righty \nScore: " + DataStore.GetComponent<DataStore>().player3score;
                }
                else
                {
                    textString = "Player 3 - Lefty \nScore: " + DataStore.GetComponent<DataStore>().player3score;
                }
                break;
            case "Player4Text":
                if (righty)
                {
                    textString = "Player 4 - Righty \nScore: " + DataStore.GetComponent<DataStore>().player4score;
                }
                else
                {
                    textString = "Player 4 - Lefty \nScore: " + DataStore.GetComponent<DataStore>().player4score;
                }
                break;
        }
        text.text = textString;
    }
}
