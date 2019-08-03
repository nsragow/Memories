using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    // Private Variables
    private Vector3 posCheck = new Vector3(10000, 10000, 10000); // A check for respawn purposes
    private int level = 1;
    private float timeRate = 1f;

    // Public Variables
    public Vector3 respawn;
    public bool gameOver = false;
    

    // Start is called before the first frame update
    void Start()
    {
        ///StartCoroutine(GameTime());
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Called to setup up the level.
    public void Setup_Level(GameObject player, int Lvl)
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

