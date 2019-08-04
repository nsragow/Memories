using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public KeyCode jump = KeyCode.UpArrow;
    public KeyCode left = KeyCode.LeftArrow;
    public KeyCode right = KeyCode.RightArrow;

    public float upForce = 10f;
    public float sideForce = 12f;
    public float maxVelocity = 8f;
    public int cayoteeTime = 10; //frames after leaving surface when player can still perform jump

    public bool allowWallJump;
    private int touchingFrames;

    private int movement;
    public bool executeJump; //indicates when the force will be applied
    public bool isJumping; //Indicates when to start checking for ground

    private Rigidbody2D rigidbody2D;


    void Start()
    {
        //Player can't do wall jump in the first level
        allowWallJump = false;
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.freezeRotation = true;
        //Start player in idle
        movement = Constants.movement_idle;
        executeJump = false;
        isJumping = false;
        touchingFrames = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Preset the movement to idle in case no movement is required
        movement = Constants.movement_idle;

        //Impulse left/right, only if maxVelocity hasn't been reached
        if (Input.GetKey(left) && rigidbody2D.velocity.x > (Constants.movement_left * maxVelocity))
        {
            //actual movement will be applied in FixedUpdate
            movement = Constants.movement_left;
        }

        if (Input.GetKey(right) && rigidbody2D.velocity.x < (Constants.movement_right * maxVelocity))
        {
            //actual movement will be applied in FixedUpdate
            movement = Constants.movement_right;
        }

        //Check for jump. If key is pressed 
        if (Input.GetKey(jump))
        {
            if (!executeJump && !isJumping)
            {
                //Actual jump will happen in Fixed update
                executeJump = true;
            }
        }

    }

    void FixedUpdate()
    {
        //If player is moving left or right
        if (movement != Constants.movement_idle)
        {
            rigidbody2D.AddForce(new Vector2(movement * sideForce, 0f));
        }

        //If player pressed jump
        if (executeJump)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, upForce);
            //jump executed
            executeJump = false;
            //start checking for ground
            isJumping = true;
        }
        //Check for ground, so we know when to allow jump again
        else if (isJumping)
        {
            //Raycast only in Ground layer (8) to prevent the raycast to hit the player or any other object
            RaycastHit2D ground = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.down, Constants.groundRaycast_lenght, 1 << 8);

            //If the raycast hits something and that something is tagged as platform
            if (ground.collider != null && ground.collider.CompareTag(Constants.tag_platform))
            {
                //Allow to jump again
                isJumping = false;
            }
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        touchingFrames = cayoteeTime;
    }
}
