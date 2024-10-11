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
    public ParticleSystem expSystem;
    public ParticleSystem dirtSystem;

    public AudioClip jumpSound;
    public AudioClip crashSound;

    private AudioSource asPLayer;

    void Start()
    {
        rbPlayer = GetComponent<Rigidbody>(); //access and store the rigidbody

        Physics.gravity *= gravityModifier;

        animPlayer = GetComponent<Animator>(); //access and store the player animator

        asPLayer = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        bool spaceDown = Input.GetKeyDown(KeyCode.Space);
        // Conditions met to Jump
        if (spaceDown && onGround && !gameOver) 
        {
            // Jump Code
            rbPlayer.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            onGround = false;
            animPlayer.SetTrigger("Jump_trig");
            dirtSystem.Stop();
            asPLayer.PlayOneShot(jumpSound, 2.0f); 
            // if we used just .Play() it would play the clip associated with the audio source
        }
    }
    // resetting the value of on ground to true when the player reaches the ground
    // this assumes that the player is only colliding with the ground in the game
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        { 
            onGround = true;
            dirtSystem.Play();
        }
        // Game is over when this condition is met
        else if (collision.gameObject.CompareTag("Obstacle")) 
        {
            Debug.Log("Game Over!");
            gameOver = true;
            animPlayer.SetBool("Death_b", true);
            animPlayer.SetInteger("DeathType_int", 2);
            expSystem.Play();
            dirtSystem.Stop();
            asPLayer.PlayOneShot(crashSound, 2.0f);
        }
    }
}
