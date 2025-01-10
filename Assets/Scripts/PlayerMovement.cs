using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Changeable Variables (Riding)
    [SerializeField] float pushForce;
    [SerializeField] float stopForce;
    [SerializeField] float maxSpeed; //25
    [SerializeField] float jumpForce;

    //Changeable Variables (Walking)
    [SerializeField] float walkForce;
    [SerializeField] float maxWalkSpeed;
    [SerializeField] float jumpFootForce;
    [SerializeField] float walkSpeed;

    //Sprites
    [SerializeField] Sprite ride;
    [SerializeField] Sprite push;
    [SerializeField] Sprite jump;
    [SerializeField] Sprite hold;

    [SerializeField] LayerMask jumpableGround;

    //public variables
    public bool isSlowingForward;

    //private variables (Riding)
    float forwardMovementTimer;
    bool canMoveForward;
    bool isRiding;

    //private variables (Walking)
    Vector2 walkDirection;
    bool canWalk;

    
    //Components
    Rigidbody2D rb;
    SpriteRenderer sr;
    BoxCollider2D bc;


    // Start is called before the first frame update
    void Start()
    {
        //Set Variables
        isRiding = true;
        //Set Components
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.W))
        {
            if(isRiding && rb.velocity.x == 0)
            {
                isRiding = false;
                sr.sprite = hold;
            } else if(!isRiding)
            {
                isRiding = true;
                sr.sprite = ride;
            }
        }




        if (!isRiding)
        {

            if(Input.GetKey(KeyCode.D))
            {
                this.transform.position += new Vector3(walkSpeed * Time.deltaTime, 0, 0);
                sr.flipX = false;
            }
            if(Input.GetKey(KeyCode.A))
            {
                this.transform.position += new Vector3(-walkSpeed * Time.deltaTime, 0, 0);
                sr.flipX = true;
            }


            /*
            if (Input.GetKeyDown(KeyCode.D))
            {
                walkDirection = Vector2.right;
                canWalk = true;
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                canWalk = false;
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                walkDirection = Vector2.left;
                canWalk = true;
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                canWalk = false;
            }

            if (rb.velocity.x > maxWalkSpeed)
            {
                rb.velocity = new Vector2(maxWalkSpeed, rb.velocity.y);
            } else if(rb.velocity.x < -maxWalkSpeed)
            {
                rb.velocity = new Vector2(-maxWalkSpeed, rb.velocity.y);
            }
            */

        }
        else
        {
            if (Input.GetKey(KeyCode.D) && isGrounded())
            {
                forwardMovementTimer += Time.deltaTime * 1;
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                forwardMovementTimer = 0;
                canMoveForward = false;
                sr.sprite = ride;
            }

            if (forwardMovementTimer > 2)
            {
                canMoveForward = false;
                sr.sprite = ride;
                forwardMovementTimer = 0;
            }
            else if (forwardMovementTimer > 0.5f)
            {
                canMoveForward = true;
                sr.sprite = push;
            }

            if (Input.GetKey(KeyCode.A))
            {

                if (rb.velocity.x > 0 && isGrounded())
                {
                    isSlowingForward = true;
                    sr.sprite = push;
                }
                else
                {
                    isSlowingForward = false;
                    sr.sprite = push;
                }
            }
            if (Input.GetKeyUp(KeyCode.A) && isGrounded())
            {
                if (isSlowingForward)
                {
                    isSlowingForward = false;

                }

                sr.sprite = ride;
            }

            /*
            if(canMoveForward && rb.velocity.x > 0 && rb.velocity.x < 0.5f)
            {
                sr.sprite = ride;
            }
            */

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                sr.sprite = jump;
            }

            if (isGrounded() && sr.sprite == jump)
            {
                sr.sprite = ride;
            }
            if (!isGrounded())
            {
                sr.sprite = jump;
            }



            if (rb.velocity.x > maxSpeed)
            {
                rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
            }
        }

        }

    private void FixedUpdate()
    {
        //Riding
        if (canMoveForward)
        {
            rb.AddForce(Vector2.right * pushForce, ForceMode2D.Force);
        }
        if(isSlowingForward && rb.velocity.x > 0)
        {
            rb.AddForce(Vector2.left * stopForce, ForceMode2D.Force);
        }

        //Walking
        if(canWalk)
        {
            rb.AddForce(walkDirection * walkForce, ForceMode2D.Force);
        }
    }

    private bool isGrounded()
    {
        return Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
        //return Physics2D.Raycast(new Vector2(this.transform.position.x, this.transform.position.y - 2), Vector2.down, 0.1f, jumpableGround);
    }


}
