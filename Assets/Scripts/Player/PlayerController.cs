using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public KeyCode jump = KeyCode.UpArrow;
    public KeyCode left = KeyCode.LeftArrow;
    public KeyCode right = KeyCode.RightArrow;

    public float upForce;
    public float sideForce;
    public float maxVelocity;

    public bool allowWallJump;

    private int movement; //-1,0,1 >> L,Idle,R
    private bool executeJump; //indicates when the force will be applied
    private bool isJumping; //Indicates when to start checking for ground
    private bool usedWallJump; //Indicates if player already used walljump
    private int wallJumpSide; //Indicates to which side will be performed the wall jump

    private Rigidbody2D rigidbody2D;


    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.freezeRotation = true;
        //Start player in idle
        movement = Constants.movement_idle;
        executeJump = false;
        isJumping = false;
        usedWallJump = false;
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
        if (Input.GetKeyDown(jump))
        {
            //If player is already on air, wall jump is allowed and player hasn't walljumped
            if (isJumping && allowWallJump && !usedWallJump)
            {
                //Check if there's a wall in range to the sides, and jump in the opposite direction if so
                //Left first
                RaycastHit2D raycast = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.left, Constants.groundRaycast_lenght, 1 << 8);
                if (raycast.collider != null && raycast.collider.CompareTag(Constants.tag_platform))
                {
                    //flag the wall jump side to the right (oposite to the wall)
                    wallJumpSide = Constants.movement_right;
                    executeJump = true;
                    usedWallJump = true;
                }
                //If nothing found on the left try right
                else
                {
                    raycast = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.right, Constants.groundRaycast_lenght, 1 << 8);
                    if (raycast.collider != null && raycast.collider.CompareTag(Constants.tag_platform))
                    {
                        //flag the wall jump side to the right (oposite to the wall)
                        wallJumpSide = Constants.movement_left;
                        executeJump = true;
                        usedWallJump = true;
                    }
                }
            }
            //If this is a ground jump, so player is not in air basically
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
            //If what the player is doing is a wall jump
            if (usedWallJump)
            {
                //Perform the wall jump to the requested side
                rigidbody2D.velocity = new Vector2(Constants.wallJumpForce.x * wallJumpSide, Constants.wallJumpForce.y);
                //This will prevent any other jump to happen
                executeJump = false;
                //Allow a wall jump again
                usedWallJump = false;
            }
            //If not, it's a ground jump
            else
            {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, upForce);
                //jump executed
                executeJump = false;
                //start checking for ground
                isJumping = true;
            }
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
                //reset walljump
                usedWallJump = false;
            }
        }
    }
}
