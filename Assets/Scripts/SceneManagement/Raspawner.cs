using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raspawner : MonoBehaviour
{
    public Vector3 startingPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If player touches this, respawn it to starting pos
        collision.transform.position = startingPosition;
    }


}
