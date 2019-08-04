using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{

    private BoardManager bm;

    public RuntimeAnimatorController Nuetral;
    public RuntimeAnimatorController Green;
    public RuntimeAnimatorController Orange;
    public RuntimeAnimatorController Red;
    public RuntimeAnimatorController Blue;
    public RuntimeAnimatorController Yellow;

    private RuntimeAnimatorController new_anim;
    private Animator current_anim;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        bm = GameObject.FindGameObjectWithTag("BoardManager").GetComponent<BoardManager>();
        current_anim = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        current_anim.runtimeAnimatorController = Nuetral;
    }

    public void ChangeColor(int key)
    {
        //print(key);
        switch(key)
        {
            case 1:
                new_anim = Green;
                break;
            case 2:
                new_anim = Orange;
                break;
            case 3:
                new_anim = Red;
                break;
            case 4:
                new_anim = Blue;
                break;
            case 5:
                new_anim = Yellow;
                break;
            default:
                new_anim = Nuetral;
                break;
        }

        
        if (current_anim.runtimeAnimatorController != new_anim)
        {
            current_anim.runtimeAnimatorController = new_anim;
        }

    }

    private void Update()
    {
        current_anim.SetBool("moving", rb.velocity.x != 0);

        sr.flipX = rb.velocity.x < 0;
    }

}
