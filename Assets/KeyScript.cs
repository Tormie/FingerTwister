using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyScript : MonoBehaviour
{
    public char keyLetter = '!';
    public Color keyColor = Color.white;
    Text text;
    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<Text>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //SetLetter();
        //SetColor();
        text.text = "" + keyLetter;
        sr.color = keyColor;
    }

    public void SetLetter()
    {
        text.text = "" + keyLetter;
    }

    public void SetColor()
    {
        sr.color = keyColor;
    }

}
