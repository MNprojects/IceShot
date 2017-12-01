using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 2f;
    public float sensitivity = 2f;
    public GameObject cam;
    public float friction = 0f;
    public bool isSliding = false;
    public bool isOnIce = true;
    public bool invertY = true;
    public float jumpSpeed;

    CharacterController player;
    float moveFrontBackSpeed;
    float moveLeftRightSpeed;
    float rotationX;
    float rotationY;
    Vector3 curVel;
    public Vector3 slidingVector;
    Vector3 movement;

    void Awake()
    {
        player = GetComponent<CharacterController>();

    }

    // FixedUpdate is used over Update because we are calculating physics for a rigidbody
    void FixedUpdate()
    {
        if (isOnIce) // Player can slide while on ice but not on normal ground
        {
            if (isSliding) //Only allow camera rotation
            {
                rotationX = Input.GetAxis("Mouse X") * sensitivity;
                rotationY = Input.GetAxis("Mouse Y") * sensitivity;
                player.Move((slidingVector * Time.deltaTime) + (Vector3.up * Time.deltaTime * jumpSpeed));
                transform.Rotate(0, rotationX, 0);
                cam.transform.Rotate(-rotationY, 0, 0);

            }
            else // Player has collided and can move again
            {
                Movement();
                slidingVector = movement;
                if (Input.GetAxis("Vertical") > 0.80 || Input.GetAxis("Horizontal") > 0.80)
                {
                    isSliding = true;
                }
            }
        }
        else
        {
            Movement();
        }
    }
    
    // Standard function for calculating movement
    //TODO Normalise the movement so moving diagonally isn't faster
    Vector3 Movement()
    {
        // Unity presets for ""Vertical" etc
        moveFrontBackSpeed = Input.GetAxis("Vertical") * speed;
        moveLeftRightSpeed = Input.GetAxis("Horizontal") * speed;

        rotationX = Input.GetAxis("Mouse X") * sensitivity;
        rotationY = Input.GetAxis("Mouse Y") * sensitivity;

        // -5f to mimic gravity.
        movement.Set(moveLeftRightSpeed, -5f, moveFrontBackSpeed);
        transform.Rotate(0, rotationX, 0);
        cam.transform.Rotate(-rotationY, 0, 0);

        movement = transform.rotation * movement;
        player.Move(movement * Time.deltaTime);
        return movement;
    }


    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Ice")
        {
            isOnIce = true;
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Wall")
        {
            isSliding = false;
            isOnIce = false;
        }
    }
}

