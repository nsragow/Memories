using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Platform : MonoBehaviour
{
    private BoardManager bm;
    private SpriteShapeController shape_controller;
    private EdgeCollider2D my_col;

    [SerializeField]
    public List<int> MyColors = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        //grab BoardManager Script for colors
        bm = GameObject.FindGameObjectWithTag("BoardManager").GetComponent<BoardManager>();
        shape_controller = GetComponent<SpriteShapeController>();
        my_col = GetComponent<EdgeCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
         
    }

    public void update_color(int key)
    {
        //if color code in list, change color
        if (MyColors.Contains(0))
        {
            if (shape_controller.spriteShape != bm.colors[0]) { shape_controller.spriteShape = bm.colors[0]; }
            return;
        }

        else if (MyColors.Contains(key))
        {
            shape_controller.enabled = true;
            my_col.enabled = true;
            shape_controller.spriteShape = bm.colors[key];
            //print("Color Changed");
        }

        //if color code not in list, turn off
        else
        {
            shape_controller.enabled = false;
            my_col.enabled = false;
        }
        
    }
}
