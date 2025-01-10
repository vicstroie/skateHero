using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Changeable Variables
    [SerializeField] float pushForce;
    [SerializeField] float stopForce;
    [SerializeField] float maxSpeed; //25
    [SerializeField] float jumpForce;
    [SerializeField] Sprite ride;
    [SerializeField] Sprite push;
    [SerializeField] Sprite jump;

    [SerializeField] LayerMask jumpableGround;

    //public variables
    public bool isSlowingForward;

    //private variables
    float forwardMovementTimer;
    bool canMoveForward;

    

    Rigidbody2D rb;
    SpriteRenderer sr;
    BoxCollider2D bc;


    // Start is called before the first frame update
    void Start()
    {
        //Set Variables

        //Set Components
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.D) && isGrounded())
        {
            forwardMovementTimer += Time.deltaTime * 1;
        }
        if(Input.GetKeyUp(KeyCode.D)) 
        {
            forwardMovementTimer = 0;
            canMoveForward = false;
            sr.sprite = ride;
        }

        if (forwardMovementTimer > 2) { 
            canMoveForward = false;
            sr.sprite = ride;
            forwardMovementTimer = 0;
        } else if (forwardMovementTimer > 0.5f)
        {
            canMoveForward = true;
            sr.sprite = push;
        }

        if(Input.GetKey(KeyCode.A))
        {

            if(rb.velocity.x > 0 && isGrounded())
            {
                isSlowingForward = true;
                sr.sprite = push;
            } else
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

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            sr.sprite = jump;
        }

        if(isGrounded() && sr.sprite == jump)
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

    private void FixedUpdate()
    {
        if (canMoveForward)
        {
            rb.AddForce(Vector2.right * pushForce, ForceMode2D.Force);
        }
        if(isSlowingForward && rb.velocity.x > 0)
        {
            rb.AddForce(Vector2.left * stopForce, ForceMode2D.Force);
        }
    }

    private bool isGrounded()
    {
        return Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
        //return Physics2D.Raycast(new Vector2(this.transform.position.x, this.transform.position.y - 2), Vector2.down, 0.1f, jumpableGround);
    }


}
