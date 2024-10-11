using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rbPlayer; // class level variable to hold the rigidbody
    public float gravityModifier;
    public float jumpForce;
    private bool onGround = true;
    public bool gameOver = false;

    private Animator animPlayer;

    void Start()
    {
        rbPlayer = GetComponent<Rigidbody>(); //access and store the rigidbody
        animPlayer = GetComponent<Animator>(); //access and store the player animator
        Physics.gravity *= gravityModifier;

    }

    // Update is called once per frame
    void Update()
    {
        bool spaceDown = Input.GetKeyDown(KeyCode.Space);
        // Conditions met to Jump
        if (spaceDown && onGround && !gameOver) 
        {
            rbPlayer.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            onGround = false;
            animPlayer.SetTrigger("Jump_trig");
        }
    }
    // resetting the value of on ground to true when the player reaches the ground
    // this assumes that the player is only colliding with the ground in the game
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        { 
            onGround = true; 
        }
        // Game if over when this condition is met
        else if (collision.gameObject.CompareTag("Obstacle")) 
        {
            Debug.Log("Game Over!");
            gameOver = true;
            animPlayer.SetBool("Death_b", true);
            animPlayer.SetInteger("DeathType_int", 2);
        }
    }
}
