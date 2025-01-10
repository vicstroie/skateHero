using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    Camera cam;
    GameObject player;
    float playerSpeed;

    // Start is called before the first frame update
    void Start()
    {
        cam = this.GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        playerSpeed = Mathf.Abs(player.GetComponent<Rigidbody2D>().velocity.x);

        if (player.GetComponent<PlayerMovement>().isSlowingForward)
        {
            if (cam.orthographicSize > 10)
            {
                cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 10, 0.008f);
            }
        }
        else
        {
            if (playerSpeed > 20)
            {
                if (cam.orthographicSize < 20)
                {
                    cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 20, 0.005f);
                }
            }
            else if (playerSpeed > 10)
            {
                if (cam.orthographicSize != 15)
                {
                    cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 15, 0.005f);
                }
            }
        }

        
        
        /*else
        {
            if (cam.orthographicSize > 10)
            {
                cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 10, 0.01f);
            }
        }
        */

        
 


        this.transform.position = new Vector3(player.transform.position.x + 6, player.transform.position.y + 2, -10);
    }
}
