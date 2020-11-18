using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEngine : MonoBehaviour
{
    readonly WaitForSeconds spinnerDelay = new WaitForSeconds(2f);
    readonly WaitForSeconds fingerCheckDelay = new WaitForSeconds(0.5f);
    readonly WaitForSeconds blinkDelay = new WaitForSeconds(0.1f);

    Color player1color = Color.red;
    Color player2color = Color.blue;
    Color player3color = Color.green;
    Color player4color = Color.yellow;

    bool allowSpin = true;

    public GameObject letterObject;
    GameObject fingerSpinner;
    GameObject fingerSpinnerAnim;
    public GameObject keySpinner;
    GameObject keySpinnerAnim;
    GameObject player1key1, player1key2, player1key3, player1key4, player1key5;
    GameObject player2key1, player2key2, player2key3, player2key4, player2key5;
    GameObject player3key1, player3key2, player3key3, player3key4, player3key5;
    GameObject player4key1, player4key2, player4key3, player4key4, player4key5;
    GameObject player1text, player2text, player3text, player4text;
    GameObject player1hand, player2hand, player3hand, player4hand;
    GameObject DataStore;

    int chosenKey;
    int playerAmount;
    int playerTurn = 1;
    int player1score, player2score, player3score, player4score;
    int checkCounter = 0;

    // playerkeys gives values for fingers (thumb, index, middle, ring, pinky)
    char[] player1Keys = { '!', '!', '!', '!', '!' };
    char[] player2Keys = { '!', '!', '!', '!', '!' };
    char[] player3Keys = { '!', '!', '!', '!', '!' };
    char[] player4Keys = { '!', '!', '!', '!', '!' };
    // setup keyboard layout
    char[] keysQwerty = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '-', '=', 'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p', '[', ']', 'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', ';', '"', 'z', 'x', 'c', 'v', 'b', 'n', 'm', ',', '.', '/' };
    char[] player1KeySection = { '1','2','3','q','w','e','a','s','d','z','x'};
    char[] player2KeySection = { '4', '5', '6', 'r', 't', 'y', 'f', 'g', 'c', 'v'};
    char[] player3KeySection = { '7', '8', '9', 'u', 'i', 'o', 'h', 'j', 'k', 'b', 'n', 'm' };
    char[] player4KeySection = { '0', '-', '=', 'p', '[', ']', 'l', ';', '"', ',', '.', '/' };
    public char chosenLetter;
    // Start is called before the first frame update
    void Start()
    {
        DataStore = GameObject.Find("DataStorage");
        fingerSpinner = GameObject.Find("WheelOfFinger");
        fingerSpinnerAnim = GameObject.Find("WheelOfFingerAnim1");
        keySpinner = GameObject.Find("KeySpinner");
        keySpinnerAnim = GameObject.Find("KeyAnim1");
        player1text = GameObject.Find("Player1Text");
        player2text = GameObject.Find("Player2Text");
        player3text = GameObject.Find("Player3Text");
        player4text = GameObject.Find("Player4Text");
        player1hand = GameObject.Find("Player1Hand");
        player2hand = GameObject.Find("Player2Hand");
        player3hand = GameObject.Find("Player3Hand");
        player4hand = GameObject.Find("Player4Hand");
        fingerSpinnerAnim.SetActive(false);
        keySpinnerAnim.SetActive(false);
        playerAmount = DataStore.GetComponent<DataStore>().playerAmount;
        player1score = DataStore.GetComponent<DataStore>().player1score;
        player2score = DataStore.GetComponent<DataStore>().player2score;
        player3score = DataStore.GetComponent<DataStore>().player3score;
        player4score = DataStore.GetComponent<DataStore>().player4score;
        BuildGUI();
        BuildKeyboard();
        player1key1 = GameObject.Find("player1key1");
        player1key2 = GameObject.Find("player1key2");
        player1key3 = GameObject.Find("player1key3");
        player1key4 = GameObject.Find("player1key4");
        player1key5 = GameObject.Find("player1key5");
        player2key1 = GameObject.Find("player2key1");
        player2key2 = GameObject.Find("player2key2");
        player2key3 = GameObject.Find("player2key3");
        player2key4 = GameObject.Find("player2key4");
        player2key5 = GameObject.Find("player2key5");
        player3key1 = GameObject.Find("player3key1");
        player3key2 = GameObject.Find("player3key2");
        player3key3 = GameObject.Find("player3key3");
        player3key4 = GameObject.Find("player3key4");
        player3key5 = GameObject.Find("player3key5");
        player4key1 = GameObject.Find("player4key1");
        player4key2 = GameObject.Find("player4key2");
        player4key3 = GameObject.Find("player4key3");
        player4key4 = GameObject.Find("player4key4");
        player4key5 = GameObject.Find("player4key5");
        if (DataStore.GetComponent<DataStore>().player1Right == false)
        {
            player1hand.transform.localScale = new Vector3(-30, 15, 76.89999f);
            player1hand.GetComponent<PlayerText>().textString = "Player 1 - Lefty";
        }
        if (DataStore.GetComponent<DataStore>().player2Right == false)
        {
            player2hand.transform.localScale = new Vector3(-30, 15, 76.89999f);
        }
        if (DataStore.GetComponent<DataStore>().player3Right == false)
        {
            player3hand.transform.localScale = new Vector3(-30, 15, 76.89999f);
        }
        if (DataStore.GetComponent<DataStore>().player4Right == false)
        {
            player4hand.transform.localScale = new Vector3(-30, 15, 76.89999f);
        }
        //StartCoroutine(CheckFingers());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && allowSpin)
        {
            allowSpin = false;
            chosenLetter = SpinLetter(playerTurn);
            chosenKey = SpinFinger();
            StartCoroutine(SetFinger(chosenKey, chosenLetter));
        }
    }

    void FixedUpdate()
    {
        ClearKeys();
        SetColors();
        CheckFingers2();
    }

    void SetKeys()
    {
        player1key1.GetComponent<KeyScript>().keyLetter = player1Keys[0];
        player1key1.GetComponent<KeyScript>().SetLetter();
        player1key2.GetComponent<KeyScript>().keyLetter = player1Keys[1];
        player1key2.GetComponent<KeyScript>().SetLetter();
        player1key3.GetComponent<KeyScript>().keyLetter = player1Keys[2];
        player1key3.GetComponent<KeyScript>().SetLetter();
        player1key4.GetComponent<KeyScript>().keyLetter = player1Keys[3];
        player1key4.GetComponent<KeyScript>().SetLetter();
        player1key5.GetComponent<KeyScript>().keyLetter = player1Keys[4];
        player1key5.GetComponent<KeyScript>().SetLetter();
        if (playerAmount == 2)
        {
            player2key1.GetComponent<KeyScript>().keyLetter = player2Keys[0];
            player2key2.GetComponent<KeyScript>().keyLetter = player2Keys[1];
            player2key3.GetComponent<KeyScript>().keyLetter = player2Keys[2];
            player2key4.GetComponent<KeyScript>().keyLetter = player2Keys[3];
            player2key5.GetComponent<KeyScript>().keyLetter = player2Keys[4];
            player2key1.GetComponent<KeyScript>().SetLetter();
            player2key2.GetComponent<KeyScript>().SetLetter();
            player2key3.GetComponent<KeyScript>().SetLetter();
            player2key4.GetComponent<KeyScript>().SetLetter();
            player2key5.GetComponent<KeyScript>().SetLetter();
        }
        if (playerAmount == 3)
        {
            player2key1.GetComponent<KeyScript>().keyLetter = player2Keys[0];
            player2key2.GetComponent<KeyScript>().keyLetter = player2Keys[1];
            player2key3.GetComponent<KeyScript>().keyLetter = player2Keys[2];
            player2key4.GetComponent<KeyScript>().keyLetter = player2Keys[3];
            player2key5.GetComponent<KeyScript>().keyLetter = player2Keys[4];
            player3key1.GetComponent<KeyScript>().keyLetter = player3Keys[0];
            player3key2.GetComponent<KeyScript>().keyLetter = player3Keys[1];
            player3key3.GetComponent<KeyScript>().keyLetter = player3Keys[2];
            player3key4.GetComponent<KeyScript>().keyLetter = player3Keys[3];
            player3key5.GetComponent<KeyScript>().keyLetter = player3Keys[4];
            player2key1.GetComponent<KeyScript>().SetLetter();
            player2key2.GetComponent<KeyScript>().SetLetter();
            player2key3.GetComponent<KeyScript>().SetLetter();
            player2key4.GetComponent<KeyScript>().SetLetter();
            player2key5.GetComponent<KeyScript>().SetLetter();
            player3key1.GetComponent<KeyScript>().SetLetter();
            player3key2.GetComponent<KeyScript>().SetLetter();
            player3key3.GetComponent<KeyScript>().SetLetter();
            player3key4.GetComponent<KeyScript>().SetLetter();
            player3key5.GetComponent<KeyScript>().SetLetter();
        }
        if (playerAmount == 4)
        {
            player2key1.GetComponent<KeyScript>().keyLetter = player2Keys[0];
            player2key2.GetComponent<KeyScript>().keyLetter = player2Keys[1];
            player2key3.GetComponent<KeyScript>().keyLetter = player2Keys[2];
            player2key4.GetComponent<KeyScript>().keyLetter = player2Keys[3];
            player2key5.GetComponent<KeyScript>().keyLetter = player2Keys[4];
            player3key1.GetComponent<KeyScript>().keyLetter = player3Keys[0];
            player3key2.GetComponent<KeyScript>().keyLetter = player3Keys[1];
            player3key3.GetComponent<KeyScript>().keyLetter = player3Keys[2];
            player3key4.GetComponent<KeyScript>().keyLetter = player3Keys[3];
            player3key5.GetComponent<KeyScript>().keyLetter = player3Keys[4];
            player4key1.GetComponent<KeyScript>().keyLetter = player4Keys[0];
            player4key2.GetComponent<KeyScript>().keyLetter = player4Keys[1];
            player4key3.GetComponent<KeyScript>().keyLetter = player4Keys[2];
            player4key4.GetComponent<KeyScript>().keyLetter = player4Keys[3];
            player4key5.GetComponent<KeyScript>().keyLetter = player4Keys[4];
            player2key1.GetComponent<KeyScript>().SetLetter();
            player2key2.GetComponent<KeyScript>().SetLetter();
            player2key3.GetComponent<KeyScript>().SetLetter();
            player2key4.GetComponent<KeyScript>().SetLetter();
            player2key5.GetComponent<KeyScript>().SetLetter();
            player3key1.GetComponent<KeyScript>().SetLetter();
            player3key2.GetComponent<KeyScript>().SetLetter();
            player3key3.GetComponent<KeyScript>().SetLetter();
            player3key4.GetComponent<KeyScript>().SetLetter();
            player3key5.GetComponent<KeyScript>().SetLetter();
            player4key1.GetComponent<KeyScript>().SetLetter();
            player4key2.GetComponent<KeyScript>().SetLetter();
            player4key3.GetComponent<KeyScript>().SetLetter();
            player4key4.GetComponent<KeyScript>().SetLetter();
            player4key5.GetComponent<KeyScript>().SetLetter();
        }
    }

    void BuildGUI()
    {
        for (int p = 1; p <= playerAmount; p++)
        {
            for (int i = 1; i <= 5; i++)
            {
                GameObject letter;
                letter = Object.Instantiate(letterObject, new Vector3(-7.4f + 0.7f * (i - 1) + 4 * (p - 1), 2.6f, 0), Quaternion.identity);
                string objectName = string.Format("player" + p + "key" + i);
                letter.transform.localScale = new Vector3(0.6f, 1, 1);
                letter.name = objectName;
            }
        }
        player2hand.SetActive(false);
        player3hand.SetActive(false);
        player4hand.SetActive(false);
        player2text.SetActive(false);
        player3text.SetActive(false);
        player4text.SetActive(false);

        if (playerAmount == 2)
        {
            player2hand.SetActive(true);
            player2text.SetActive(true);
        }
        if (playerAmount == 3)
        {
            player2hand.SetActive(true);
            player3hand.SetActive(true);
            player2text.SetActive(true);
            player3text.SetActive(true);
        }
        if (playerAmount == 4)
        {
            player2hand.SetActive(true);
            player3hand.SetActive(true);
            player4hand.SetActive(true);
            player2text.SetActive(true);
            player3text.SetActive(true);
            player4text.SetActive(true);
        }
    }

    void BuildKeyboard()
    {
        for (int i = 0; i < keysQwerty.Length; i++)
        {
            if (i <= 11)
            {
                GameObject letter;
                letter = Object.Instantiate(letterObject, new Vector3(-7.5f + i, 0.5f, 0), Quaternion.identity);
                string objectName = string.Format("key" + keysQwerty[i]);
                letter.GetComponent<KeyScript>().keyLetter = (keysQwerty[i]);
                //letter.GetComponent<KeyScript>().SetLetter();
                letter.name = objectName;
            }
            if (i > 11 && i <= 23)
            {
                GameObject letter;
                string objectName = string.Format("key" + keysQwerty[i]);
                letter = Object.Instantiate(letterObject, new Vector3(-7f + i - 12, -0.25f, 0), Quaternion.identity);
                letter.GetComponent<KeyScript>().keyLetter = (keysQwerty[i]);
                //letter.GetComponent<KeyScript>().SetLetter();
                letter.name = objectName;
            }
            if (i > 23 && i <= 34)
            {
                GameObject letter;
                letter = Object.Instantiate(letterObject, new Vector3(-6.5f + i - 24, -1f, 0), Quaternion.identity);
                string objectName = string.Format("key" + keysQwerty[i]);
                letter.GetComponent<KeyScript>().keyLetter = (keysQwerty[i]);
                //letter.GetComponent<KeyScript>().SetLetter();
                letter.name = objectName;
            }
            if (i > 34 && i <= 44)
            {
                GameObject letter;
                letter = Object.Instantiate(letterObject, new Vector3(-6f + i - 35, -1.75f, 0), Quaternion.identity);
                string objectName = string.Format("key" + keysQwerty[i]);
                letter.GetComponent<KeyScript>().keyLetter = (keysQwerty[i]);
                //letter.GetComponent<KeyScript>().SetLetter();
                letter.name = objectName;
            }
        }
    }

    void SetColors()
    {
        foreach (char c in player1Keys)
        {
            if (c != '!')
            {
                GameObject targetKey = GameObject.Find(string.Format("key" + c));
                targetKey.GetComponent<KeyScript>().keyColor = player1color;
                //targetKey.GetComponent<KeyScript>().SetColor();
            }
        }
        foreach (char c in player2Keys)
        {
            if (c != '!')
            {
                GameObject targetKey = GameObject.Find(string.Format("key" + c));
                targetKey.GetComponent<KeyScript>().keyColor = player2color;
                //targetKey.GetComponent<KeyScript>().SetColor();
            }
        }
        foreach (char c in player3Keys)
        {
            if (c != '!')
            {
                GameObject targetKey = GameObject.Find(string.Format("key" + c));
                targetKey.GetComponent<KeyScript>().keyColor = player3color;
                //targetKey.GetComponent<KeyScript>().SetColor();
            }
        }
        foreach (char c in player4Keys)
        {
            if (c != '!')
            {
                GameObject targetKey = GameObject.Find(string.Format("key" + c));
                targetKey.GetComponent<KeyScript>().keyColor = player4color;
                //targetKey.GetComponent<KeyScript>().SetColor();
            }
        }
    }

    void ClearKeys()
    {
        foreach (char c in player1KeySection)
        {
            if (c != '!')
            {
                GameObject targetKey = GameObject.Find(string.Format("key" + c));
                targetKey.GetComponent<KeyScript>().keyColor = Color.white;
                //targetKey.GetComponent<KeyScript>().SetColor();
            }
        }
        foreach (char c in player2KeySection)
        {
            if (c != '!')
            {
                GameObject targetKey = GameObject.Find(string.Format("key" + c));
                targetKey.GetComponent<KeyScript>().keyColor = Color.white;
                //targetKey.GetComponent<KeyScript>().SetColor();
            }
        }
        foreach (char c in player3KeySection)
        {
            if (c != '!')
            {
                GameObject targetKey = GameObject.Find(string.Format("key" + c));
                targetKey.GetComponent<KeyScript>().keyColor = Color.white;
                //targetKey.GetComponent<KeyScript>().SetColor();
            }
        }
        foreach (char c in player4KeySection)
        {
            if (c != '!')
            {
                GameObject targetKey = GameObject.Find(string.Format("key" + c));
                targetKey.GetComponent<KeyScript>().keyColor = Color.white;
                //targetKey.GetComponent<KeyScript>().SetColor();
            }
        }
    }


    int SpinFinger()
    {
        StartCoroutine(ActivateSpinner());
        int choice = Random.Range(1, 8);
        GameObject targetKey;
        switch (choice)
        {
            default:
                return 1;
                break;
            case 1:
                fingerSpinner.transform.rotation = new Quaternion(0, 0, 0.55f, 0.837f);
                switch (playerTurn)
                {
                    case 1:
                        targetKey = GameObject.Find("player1key1");
                        StartCoroutine(BlinkKey(targetKey, player1color));
                        break;
                    case 2:
                        targetKey = GameObject.Find("player2key1");
                        StartCoroutine(BlinkKey(targetKey, player2color));
                        break;
                    case 3:
                        targetKey = GameObject.Find("player3key1");
                        StartCoroutine(BlinkKey(targetKey, player3color));
                        break;
                    case 4:
                        targetKey = GameObject.Find("player4key1");
                        StartCoroutine(BlinkKey(targetKey, player4color));
                        break;
                }
                
                return 1;
                break;
            case 2:
                fingerSpinner.transform.rotation = new Quaternion(0, 0, 0.834f, 0.55f);
                switch (playerTurn)
                {
                    case 1:
                        targetKey = GameObject.Find("player1key2");
                        StartCoroutine(BlinkKey(targetKey, player1color));
                        break;
                    case 2:
                        targetKey = GameObject.Find("player2key2");
                        StartCoroutine(BlinkKey(targetKey, player2color));
                        break;
                    case 3:
                        targetKey = GameObject.Find("player3key2");
                        StartCoroutine(BlinkKey(targetKey, player3color));
                        break;
                    case 4:
                        targetKey = GameObject.Find("player4key2");
                        StartCoroutine(BlinkKey(targetKey, player4color));
                        break;
                }
                return 2;
                break;
            case 3:
                fingerSpinner.transform.rotation = new Quaternion(0, 0, 0.98f, 0.2f);
                switch (playerTurn)
                {
                    case 1:
                        targetKey = GameObject.Find("player1key3");
                        StartCoroutine(BlinkKey(targetKey, player1color));
                        break;
                    case 2:
                        targetKey = GameObject.Find("player2key3");
                        StartCoroutine(BlinkKey(targetKey, player2color));
                        break;
                    case 3:
                        targetKey = GameObject.Find("player3key3");
                        StartCoroutine(BlinkKey(targetKey, player3color));
                        break;
                    case 4:
                        targetKey = GameObject.Find("player4key3");
                        StartCoroutine(BlinkKey(targetKey, player4color));
                        break;
                }
                return 3;
                break;
            case 4:
                fingerSpinner.transform.rotation = new Quaternion(0, 0, 0.98f, -0.2f);
                switch (playerTurn)
                {
                    case 1:
                        targetKey = GameObject.Find("player1key4");
                        StartCoroutine(BlinkKey(targetKey, player1color));
                        break;
                    case 2:
                        targetKey = GameObject.Find("player2key4");
                        StartCoroutine(BlinkKey(targetKey, player2color));
                        break;
                    case 3:
                        targetKey = GameObject.Find("player3key4");
                        StartCoroutine(BlinkKey(targetKey, player3color));
                        break;
                    case 4:
                        targetKey = GameObject.Find("player4key4");
                        StartCoroutine(BlinkKey(targetKey, player4color));
                        break;
                }
                return 4;
                break;
            case 5:
                fingerSpinner.transform.rotation = new Quaternion(0, 0, 0.834f, -0.55f);
                switch (playerTurn)
                {
                    case 1:
                        targetKey = GameObject.Find("player1key5");
                        StartCoroutine(BlinkKey(targetKey, player1color));
                        break;
                    case 2:
                        targetKey = GameObject.Find("player2key5");
                        StartCoroutine(BlinkKey(targetKey, player2color));
                        break;
                    case 3:
                        targetKey = GameObject.Find("player3key5");
                        StartCoroutine(BlinkKey(targetKey, player3color));
                        break;
                    case 4:
                        targetKey = GameObject.Find("player4key5");
                        StartCoroutine(BlinkKey(targetKey, player4color));
                        break;
                }
                return 5;
                break;
            case 6:
                fingerSpinner.transform.rotation = new Quaternion(0, 0, 0.55f, -0.837f);
                switch (playerTurn)
                {
                    case 1:
                        targetKey = GameObject.Find("player1key1");
                        StartCoroutine(BlinkKey(targetKey, player1color));
                        break;
                    case 2:
                        targetKey = GameObject.Find("player2key1");
                        StartCoroutine(BlinkKey(targetKey, player2color));
                        break;
                    case 3:
                        targetKey = GameObject.Find("player3key1");
                        StartCoroutine(BlinkKey(targetKey, player3color));
                        break;
                    case 4:
                        targetKey = GameObject.Find("player4key1");
                        StartCoroutine(BlinkKey(targetKey, player4color));
                        break;
                }
                return 1;
                break;
            case 7:
                fingerSpinner.transform.rotation = new Quaternion(0, 0, 0.2f, -0.98f);
                switch (playerTurn)
                {
                    case 1:
                        targetKey = GameObject.Find("player1key2");
                        StartCoroutine(BlinkKey(targetKey, player1color));
                        break;
                    case 2:
                        targetKey = GameObject.Find("player2key2");
                        StartCoroutine(BlinkKey(targetKey, player2color));
                        break;
                    case 3:
                        targetKey = GameObject.Find("player3key2");
                        StartCoroutine(BlinkKey(targetKey, player3color));
                        break;
                    case 4:
                        targetKey = GameObject.Find("player4key2");
                        StartCoroutine(BlinkKey(targetKey, player4color));
                        break;
                }
                return 2;
                break;
            case 8:
                fingerSpinner.transform.rotation = new Quaternion(0, 0, -0.2f, -0.98f);
                switch (playerTurn)
                {
                    case 1:
                        targetKey = GameObject.Find("player1key3");
                        StartCoroutine(BlinkKey(targetKey, player1color));
                        break;
                    case 2:
                        targetKey = GameObject.Find("player2key3");
                        StartCoroutine(BlinkKey(targetKey, player2color));
                        break;
                    case 3:
                        targetKey = GameObject.Find("player3key3");
                        StartCoroutine(BlinkKey(targetKey, player3color));
                        break;
                    case 4:
                        targetKey = GameObject.Find("player4key3");
                        StartCoroutine(BlinkKey(targetKey, player4color));
                        break;
                }
                return 3;
                break;
        }
    }

    bool CheckIfExists(int player, char letter)
    {
        switch (player)
        {
            default:
                return false;
                break;
            case 1:
                foreach (char c in player1Keys)
                {
                    if (c == letter)
                    {
                        return true;
                    }
                }
                return false;
                break;
            case 2:
                foreach (char c in player2Keys)
                {
                    if (c == letter)
                    {
                        return true;
                    }
                }
                return false;
                break;
            case 3:
                foreach (char c in player3Keys)
                {
                    if (c == letter)
                    {
                        return true;
                    }
                }
                return false;
                break;
            case 4:
                foreach (char c in player4Keys)
                {
                    if (c == letter)
                    {
                        return true;
                    }
                }
                return false;
                break;
        }
    }
    char SpinLetter(int player)
    {
        int choice = Random.Range(0, player1KeySection.Length);
        string targetname = string.Format("key" + player1KeySection[choice]);
        GameObject targetKey = GameObject.Find(targetname);
        switch (player)
        {
            default:
                return '!';
                break;
            case 1:
                choice = Random.Range(0, player1KeySection.Length);
                targetname = string.Format("key" + player1KeySection[choice]);
                targetKey = GameObject.Find(targetname);
                while (CheckIfExists(player, player1KeySection[choice]))
                {
                    choice = Random.Range(0, player1KeySection.Length);
                    targetname = string.Format("key" + player1KeySection[choice]);
                    targetKey = GameObject.Find(targetname);
                }
                keySpinner.GetComponent<KeyScript>().keyLetter = player1KeySection[choice];
                StartCoroutine(BlinkKey(targetKey, player1color));
                return player1KeySection[choice];
                break;
            case 2:
                choice = Random.Range(0, player2KeySection.Length);
                targetname = string.Format("key" + player2KeySection[choice]);
                targetKey = GameObject.Find(targetname);
                while (CheckIfExists(player, player2KeySection[choice]))
                {
                    choice = Random.Range(0, player2KeySection.Length);
                    targetname = string.Format("key" + player2KeySection[choice]);
                    targetKey = GameObject.Find(targetname);
                }
                keySpinner.GetComponent<KeyScript>().keyLetter = player2KeySection[choice];
                StartCoroutine(BlinkKey(targetKey, player2color));
                return player2KeySection[choice];
                break;
            case 3:
                choice = Random.Range(0, player3KeySection.Length);
                targetname = string.Format("key" + player3KeySection[choice]);
                targetKey = GameObject.Find(targetname);
                while (CheckIfExists(player, player3KeySection[choice]))
                {
                    choice = Random.Range(0, player3KeySection.Length);
                    targetname = string.Format("key" + player3KeySection[choice]);
                    targetKey = GameObject.Find(targetname);
                }
                keySpinner.GetComponent<KeyScript>().keyLetter = player3KeySection[choice];
                StartCoroutine(BlinkKey(targetKey, player3color));
                return player3KeySection[choice];
                break;
            case 4:
                choice = Random.Range(0, player4KeySection.Length);
                targetname = string.Format("key" + player4KeySection[choice]);
                targetKey = GameObject.Find(targetname);
                while (CheckIfExists(player, player4KeySection[choice]))
                {
                    choice = Random.Range(0, player4KeySection.Length);
                    targetname = string.Format("key" + player4KeySection[choice]);
                    targetKey = GameObject.Find(targetname);
                }
                keySpinner.GetComponent<KeyScript>().keyLetter = player4KeySection[choice];
                StartCoroutine(BlinkKey(targetKey, player4color));
                return player4KeySection[choice];
                break;
        }
        
    }

    IEnumerator ActivateSpinner()
    {
        fingerSpinnerAnim.SetActive(true);
        keySpinnerAnim.SetActive(true);
        yield return spinnerDelay;
        fingerSpinnerAnim.SetActive(false);
        keySpinnerAnim.SetActive(false);

    }

    IEnumerator SetFinger(int finger, char letter)
    {
        yield return spinnerDelay;
        switch (playerTurn)
        {
            case 1:
                player1Keys[finger - 1] = letter;
                break;
            case 2:
                player2Keys[finger - 1] = letter;
                break;
            case 3:
                player3Keys[finger - 1] = letter;
                break;
            case 4:
                player4Keys[finger - 1] = letter;
                break;
        }
        SetKeys();
        playerTurn++;
        if (playerTurn > playerAmount)
        {
            playerTurn = 1;
        }

    }

    IEnumerator BlinkKey(GameObject key, Color color)
    {
        /*for (int i = 0; i < 6; i++)
        { 
            yield return spinnerDelay;
            key.GetComponent<KeyScript>().keyColor = color;
            key.GetComponent<KeyScript>().SetColor();
            yield return blinkDelay;
            key.GetComponent<KeyScript>().keyColor = Color.white;
            yield return blinkDelay;
        }*/
        key.GetComponent<KeyScript>().keyColor = color;
        yield return spinnerDelay;
        allowSpin = true;
        yield return blinkDelay;
    }

    IEnumerator CheckFingers()
    {
        while(true)
        {
            foreach (char c in player1Keys)
            {
                //string keyCode = GetKeyCode(c);
                //Debug.Log(keyCode);
                if (c != '!')
                {
                    //if (Input.GetKey(keyCode))
                    string keyCode = "" + c;
                    if (Input.GetKey(keyCode))
                    {
                        Debug.Log("Correct Key Pressed!");
                    }
                }
            }
            yield return fingerCheckDelay;
        }
    }

    void CheckFingers2()
    {
        checkCounter++;
        if (checkCounter == 30)
        {
            for (int i = 0; i < 5; i++)
            //foreach (char c in player1Keys)
            {
                //string keyCode = GetKeyCode(c);
                //Debug.Log(keyCode);
                if (player1Keys[i] != '!')
                //if (c != '!')
                {
                    //if (Input.GetKey(keyCode))
                    //string keyCode = "" + c;
                    string keyCode = "" + player1Keys[i];
                    if (Input.GetKey(keyCode))
                    {
                        Debug.Log("Correct Key Pressed! " + player1Keys[i]);
                    }
                }
            }
        }
        if (checkCounter == 60 && playerAmount > 1)
        {
            for (int i = 0; i < 5; i++)
            {
                if (player2Keys[i] != '!')
                {
                    string keyCode = "" + player2Keys[i];
                    if (Input.GetKey(keyCode))
                    {
                        Debug.Log("Correct Key Pressed! " + player2Keys[i]);
                    }
                }
            }
        }
        if (checkCounter == 90 && playerAmount > 2)
        {
            for (int i = 0; i < 5; i++)
            {
                if (player3Keys[i] != '!')
                {
                    string keyCode = "" + player3Keys[i];
                    if (Input.GetKey(keyCode))
                    {
                        Debug.Log("Correct Key Pressed! " + player3Keys[i]);
                    }
                }
            }
        }
        if (checkCounter == 120)
        {
            if (playerAmount == 4)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (player4Keys[i] != '!')
                    {
                        string keyCode = "" + player4Keys[i];
                        if (Input.GetKey(keyCode))
                        {
                            Debug.Log("Correct Key Pressed! " + player4Keys[i]);
                        }
                    }
                }
            }
            checkCounter = 0;
        }
        /*foreach (char c in player1Keys)
        {
            //string keyCode = GetKeyCode(c);
            //Debug.Log(keyCode);
            if (c != '!')
            {
                //if (Input.GetKey(keyCode))
                string keyCode = "" + c;
                if (Input.GetKey(keyCode))
                {
                    Debug.Log("Correct Key Pressed!");
                }
            }
        }*/
    }

    string GetKeyCode(char c)
    {
        string keyCode = "";
        switch (c)
        {
            case '1':
            case '2':
            case '3':
            case '4':
            case '5':
            case '6':
            case '7':
            case '8':
            case '9':
            case '0':
                keyCode = string.Format("Alpha" + c);
                break;
            case '-':
                keyCode = "Minus";
                break;
            case '=':
                keyCode = "Equals";
                break;
            case '[':
                keyCode = "LeftBracket";
                break;
            case ']':
                keyCode = "RightBracket";
                break;
            case ';':
                keyCode = "Semicolon";
                break;
            case '"':
                keyCode = "Quote";
                break;
            case ',':
                keyCode = "Comma";
                break;
            case '.':
                keyCode = "Period";
                break;
            case '/':
                keyCode = "Slash";
                break;
            case 'q':
            case 'w':
            case 'e':
            case 'r':
            case 't':
            case 'y':
            case 'u':
            case 'i':
            case 'o':
            case 'p':
            case 'a':
            case 's':
            case 'd':
            case 'f':
            case 'g':
            case 'h':
            case 'j':
            case 'k':
            case 'l':
            case 'z':
            case 'x':
            case 'c':
            case 'v':
            case 'b':
            case 'n':
            case 'm':
                keyCode = "" +c;
                keyCode = keyCode.ToUpper();
                break;
        }
        return string.Format("KeyCode."+keyCode);
    }
}
