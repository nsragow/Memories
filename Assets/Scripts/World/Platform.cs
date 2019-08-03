using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private BoardManager bm;

    [SerializeField]
    public List<int> MyColors = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        //grab BoardManager Script for colors
        bm = GameObject.FindGameObjectWithTag("BoardManager").GetComponent<BoardManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void update_color(int key)
    {
        //if color code in list, change color
        if (MyColors.Contains(key))
        {
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<BoxCollider2D>().enabled = true;
            GetComponent<SpriteRenderer>().color = bm.colors[key];
            print("Color Changed");
        }

        //if color code not in list, turn off
        else
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
        }
        
    }
}
