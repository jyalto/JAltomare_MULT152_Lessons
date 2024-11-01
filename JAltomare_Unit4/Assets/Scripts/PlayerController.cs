using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rbPlayer;
    private Renderer rendererPlayer;
    private GameObject focalPoint;
    public float speed = 10.0f;
    public float powerUpSpeed = 10.0f;

    public GameObject powerUpInd;

    bool hasPowerUp = false; // This is a state variable


    // Start is called before the first frame update
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody>();
        rendererPlayer = GetComponent<Renderer>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        float magnitude = forwardInput * speed * Time.deltaTime;
        rbPlayer.AddForce(focalPoint.transform.forward * magnitude, ForceMode.Force);

        if(forwardInput > 0)
        {
            rendererPlayer.material.color = new Color(1.0f - forwardInput, 1.0f, 1.0f - forwardInput);
        }
        else
        {
            rendererPlayer.material.color = new Color(1.0f + forwardInput, 1.0f, 1.0f + forwardInput);
        }
        powerUpInd.transform.position = transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            hasPowerUp = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCountDown());
            powerUpInd.SetActive(true);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (hasPowerUp && collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Player has collided with " + collision.gameObject + " with power up set to: " + hasPowerUp);
            Rigidbody rbEnemy = collision.gameObject.GetComponent<Rigidbody>(); // the collison gives us access to Enemy
            Vector3 awayDir  = collision.gameObject.transform.position - transform.position;

            rbEnemy.AddForce(awayDir * powerUpSpeed, ForceMode.Impulse);
        }
    }
    IEnumerator PowerUpCountDown()
    {
        yield return new WaitForSeconds(8);
        hasPowerUp = false;
        powerUpInd.SetActive(false);
    }
}
