using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //If the player touches the finalObject
        Debug.Log(collision.tag);
        if (collision.CompareTag("Player"))
        {
            //Move to next scene
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            /*
             * 0 = menu
             * 1 = L1
             * 2 = L2
             * 3 = L3
             * 4 = Credits, TODO
             */
            SceneManager.LoadScene(sceneIndex + 1);
        }
    }
}
