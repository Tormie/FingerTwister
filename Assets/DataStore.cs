using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStore : MonoBehaviour
{

    public int playerAmount = 1;
    public int player1score = 0, player2score = 0, player3score = 0, player4score = 0;
    public bool player1Right = true, player2Right = true, player3Right = true, player4Right = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
