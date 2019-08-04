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

            if (sceneIndex == 4)
            {
                //back to initial screen
                SceneManager.LoadScene(0);
            }
            else
            {
                SceneManager.LoadScene(sceneIndex + 1);
            }
        }
    }
}
