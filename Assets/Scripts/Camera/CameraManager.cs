using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform playerTransform;

    void Start()
    {
        //Find the player, get the transform
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Only adjusting X and Y, preserving camera Z as defined on editor
        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y + 2f, transform.position.z);
    }
}
