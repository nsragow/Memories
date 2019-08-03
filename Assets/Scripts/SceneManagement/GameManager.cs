using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Private Variables

    // Public Variables
    public static GameManager instance = null;
    public GameObject Player_Choice; /// For character selection if desired.

    public Scene current_scene;

    private void Awake()
    {
        // This is a check for the script to make this a singleton.
        if (instance == null)
            instance = this;

        else if (instance != null)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        //Stores the active scene for later use
        current_scene = SceneManager.GetActiveScene();

    }

    //Load the level_1 scene and call initialize game
    public void NewGame()
    {
        SceneManager.LoadScene("Level_1");
    }

    //Load the Main_Menu Scene
    public void MainMenu()
    {
        SceneManager.LoadScene("Main_Menu");
    }
    
}