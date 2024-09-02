using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // rate of forward/backward movement
    private float speed = 15.0f;
    private float turnSpeed = 15.0f;

    // variables for player input
    private float horizontalInput;
    private float verticalInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        Debug.Log(Time.deltaTime);

        // move vehicle forward along the z axis Vector3.forward --> (0,0,1)
        transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput);

        // Allowing the vehicle to turn left and right
        // transform.Translate(Vector3.right * Time.deltaTime * turnSpeed * horizontalInput); this causes the car to drift left and right
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput); // up means that we are rotating around the y axis
    }
}
