using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller : MonoBehaviour
{
    // Start is called before the first frame update
    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;
    private int desiredLane = 1;//0:left, 1:middle, 2:right
    public float laneDistance = 4;
    public float jumpForce;
    public float Gravity = -20;
    //The distance between tow lanes


    void Start()
    {
        controller = GetComponent<CharacterController>();
        //Gather the inputs on which lane we should be
    }

    // Update is called once per frame
    void Update()
    {
        direction.z = forwardSpeed;
       
        if (controller.isGrounded)
        {
            direction.y = -1;
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Jump();
            }
        }
        else
        {
            direction.y += Gravity * Time.deltaTime;
        }
        
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            desiredLane++;
            if (desiredLane == 3)
                desiredLane = 2;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            desiredLane--;
            if (desiredLane == -1)
                desiredLane = 0;
        }

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }
        else if (desiredLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }
        transform.position = Vector3.Lerp(transform.position, targetPosition, 80 * Time.fixedDeltaTime);


    }
    private void FixedUpdate()
    {
        controller.Move(direction* Time.fixedDeltaTime);

    }

    private void Jump()
    {
        direction.y = jumpForce;
    }
}


