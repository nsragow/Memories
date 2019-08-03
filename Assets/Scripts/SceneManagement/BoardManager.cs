using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoardManager : MonoBehaviour
{
    // Private Variables
    private Vector3 posCheck = new Vector3(10000, 10000, 10000); // A check for respawn purposes
    private int level = 1;
    private float timeRate = 1f;
    public Dictionary<int, Color> colors = new Dictionary<int, Color>();


    public GameObject platform_manager;
    public Color current_color = new Color(0f, 0f, 0f, 1f);

    // Public Variables
    public Vector3 respawn;
    public bool gameOver = false;

    private void Awake()
    {
        //add colors to dictionary for outside access.
        colors.Add(0, new Color(0f, 0f, 0f, 1f));
        colors.Add(1, new Color(0f, 1f, 0f, 1f));
        colors.Add(2, new Color(1f, 0f, 0f, 1f));
        colors.Add(3, new Color(0f, 0f, 1f, 1f));
        colors.Add(4, new Color(0f, 1f, 1f, 1f));
    }

    // Start is called before the first frame update
    void Start()
    {
        ///StartCoroutine(GameTime());

        set_color(0);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void set_color(int key)
    {
        if (colors[key] != current_color)
        {
            current_color = colors[key];
            gameObject.BroadcastMessage("update_color", key);
        }
    }

    //Called to setup up the level.
    public void Setup_Level(GameObject player)
    {
        respawn = posCheck;

        if (player != null)
        {
            /// Do Something
        }
    }


    // Do something every given amount of seconds;
    IEnumerator GameTime()
    {
        while (!gameOver)
        {
            yield return new WaitForSeconds(timeRate);
            /// Call function
        }
    }



}

