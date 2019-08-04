using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public KeyCode control = KeyCode.Space;
    public float upForce = 10f;
    public float maxVel = 20f;
    public float sideForce= 1f;
    public int framesForJump = 7;
    public int cayoteeTime = 10;

    private Rigidbody2D rigidbody2D;
	private int touchingFrames = 0;

    private int extraJumpFrames = 0;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(control))
        {
            extraJumpFrames = framesForJump;
        }
        if (Input.GetKey(control))
        {

            LeftForce();
        }
        else
        {

            RightForce();
        }

        Jump();
        touchingFrames--;

    }

    private void LeftForce()
    {
        float currentVel = rigidbody2D.velocity.x;

        if(currentVel > (-1 * maxVel))
        {

            rigidbody2D.AddForce(new Vector2(-1 * sideForce, 0));
        }
    }
    private void RightForce()
    {
        float currentVel = rigidbody2D.velocity.x;

        if (currentVel < (maxVel))
        {

            rigidbody2D.AddForce(new Vector2(sideForce, 0));
        }
    }
    private void Jump()
	{

        if (CanJump() && (extraJumpFrames > 0))
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, upForce);
            //rigidbody2D.AddForce(new Vector2(0f, upForce));
            extraJumpFrames = 0;
        }
        else
        {
            extraJumpFrames--;
        }
	}
    private bool CanJump()
    {
        return touchingFrames > 0;
    }
	void OnCollisionStay2D(Collision2D collision)
	{
		touchingFrames = cayoteeTime;

	}

}
