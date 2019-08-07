using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Orb : MonoBehaviour
{
    public float respawnTime = 5f;
    public float addTime = 5f;
    //public OrbManager mngr;
    public float timer;
    public int color;

    private bool active;

    private SpriteRenderer rend;
    private BoardManager bm;

    private TextMeshPro secondsText;


    // Start is called before the first frame update
    private void Start()
    {
        //addTime += 3;
        bm = GameObject.FindGameObjectWithTag("BoardManager").GetComponent<BoardManager>();
        active = true;
        rend = GetComponent<SpriteRenderer>();

        rend.color = bm.orb_colors[color];

        //get the text component in children
        secondsText = GetComponentInChildren<TextMeshPro>();
        secondsText.text = (addTime + 3).ToString();
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
                //activate text
                secondsText.enabled = true;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (active && collision.gameObject.tag.Equals("Player"))
        {
            bm.set_color(color, addTime);
            active = false;
            timer = respawnTime;
            rend.enabled = false;
            //also deactivate text
            secondsText.enabled = false;
        }
    }


}
