using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // variables for player components
    private Rigidbody rbPlayer;
    private Animator animPlayer;
    private AudioSource asPLayer;

    // Other Variables
    public float gravityModifier;
    public float jumpForce;
    private bool onGround = true;
    public bool gameOver = false;

    // Particle Systems
    public ParticleSystem expSystem;
    public ParticleSystem dirtSystem;

    // Audio Clips
    public AudioClip jumpSound;
    public AudioClip crashSound;

    void Start()
    {
        // Accessing and storing component references
        rbPlayer = GetComponent<Rigidbody>(); 
        animPlayer = GetComponent<Animator>(); 
        asPLayer = GetComponent<AudioSource>();

        // When this is called, it motifies the gravity value for the entire project
        Physics.gravity *= gravityModifier;
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
