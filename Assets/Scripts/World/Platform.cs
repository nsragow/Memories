using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Platform : MonoBehaviour
{
    private BoardManager bm;
    private SpriteShapeController shape_controller;
    private PolygonCollider2D my_col;
    private Color color;
    private IEnumerator co_inst;
    private IEnumerator co_inst2;

    [SerializeField]
    public List<int> MyColors = new List<int>();

    bool visible;

    // Start is called before the first frame update
    void Start()
    {
        //grab BoardManager Script for colors
        bm = GameObject.FindGameObjectWithTag("BoardManager").GetComponent<BoardManager>();
        shape_controller = GetComponent<SpriteShapeController>();
        my_col = GetComponent<PolygonCollider2D>();
        color = shape_controller.spriteShapeRenderer.color;
    }

    public void update_color(int key)
    {

        StopAllCoroutines();
        //if nuetral platform, stay nuetral
        if (MyColors.Contains(0))
        {
            if (shape_controller.spriteShape != bm.colors[0]) { shape_controller.spriteShape = bm.colors[0]; }
            return;
        }
        //if color code in list, change color
        else if (MyColors.Contains(key))
        {
            shape_controller.spriteShape = bm.colors[key];
            StartCoroutine(Start_Fade(1, 1f));
            shape_controller.enabled = true;
            my_col.enabled = true;
        }

        //if color code not in list, turn off
        else
        {
            StartCoroutine(Start_Fade(0, 0.1f));
        }

        

    }

    //For Alpha fade
    IEnumerator Start_Fade(float to, float time)
    {

        color = shape_controller.spriteShapeRenderer.color;
        Color newcolor = new Color(color.r, color.g, color.b, to);

        float E_time = 0;

        while (E_time < time)
        {
            shape_controller.spriteShapeRenderer.color = Color.Lerp(color, newcolor, (E_time / time));
            E_time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        //yield return new WaitUntil(() => E_time <= time);

        if (to == 0f)
        {
            //print("called");
            shape_controller.enabled = false;
            my_col.enabled = false;
            visible = false;
        }

        if (to == 1)
            visible = true;

    }

    public void call_fade_out(float time)
    {
        if (!MyColors.Contains(0))
            StartCoroutine(Begin_Fade_Out(time));
    }

    IEnumerator Begin_Fade_Out(float time)
    {
        yield return new WaitForSeconds(3);
        StartCoroutine(Start_Fade(0f, time));
        yield return new WaitForSeconds(time);
        bm.current_color = bm.colors[0];
    }
}
