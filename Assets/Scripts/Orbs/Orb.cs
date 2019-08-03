using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    public float respawnTime = 5f;
    public float addTime = 5f;
    public OrbManager mngr;

    private int color;
    private bool active;
    private float timer;

    private SpriteRenderer rend;


    
    // Start is called before the first frame update
    private void Start()
    {
        active = true;
        rend = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (!active)
        {
            timer -= Time.deltaTime;
            if (timer < 0f)
            {
                active = true;
                rend.enabled = true; 
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (active && collision.gameObject.tag.Equals("Player"))
        {
            mngr.Activated(color, addTime);
            active = false;
            timer = respawnTime;
            rend.enabled = false;
        }
    }

   
}
