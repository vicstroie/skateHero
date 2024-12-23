using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Changeable Variables
    [SerializeField] float pushForce;
    [SerializeField] float stopForce;
    [SerializeField] float maxSpeed;
    [SerializeField] Sprite ride;
    [SerializeField] Sprite push;

    //public variables

    //private variables
    float forwardMovementTimer;
    bool canMoveForward;

    bool isSlowingForward;

    Rigidbody2D rb;
    SpriteRenderer sr;


    // Start is called before the first frame update
    void Start()
    {
        //Set Variables

        //Set Components
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.D))
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

            if(rb.velocity.x > 0)
            {
                isSlowingForward = true;
                sr.sprite = push;
            } else
            {
                isSlowingForward = false;
                sr.sprite = push;
            }
        }
        if (Input.GetKeyUp(KeyCode.A))
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


}
