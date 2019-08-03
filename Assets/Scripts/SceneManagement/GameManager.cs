using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Private Variables
    private int level = 1;
    private BoardManager boardScript;

    // Public Variables
    public static GameManager instance = null;
    public GameObject Player_Choice; /// For character selection if desired.

    

    private void Awake()
    {
        // This is a check for the script to make this a singleton.
        if (instance == null)
            instance = this;

        else if (instance != null)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        boardScript = GetComponent<BoardManager>();
        InitGame();

    }

    // Initializes the start of the game.
    void InitGame()
    {
        boardScript.Setup_Level(Player_Choice, level);
    }

}