using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private const float minForce = 10.0f; // using const means the values cannot be changed
    private const float maxForce = 15.0f;
    private const float minTorque = -10.0f;
    private const float maxTorque = 10.0f;
    private const float minXPos = -3.0f;
    private const float maxXPos = 3.0f;
    private const float ySpawnPos = -2.0f;

    private Rigidbody targetRB;
    // Start is called before the first frame update
    void Start()
    {
        targetRB = GetComponent<Rigidbody>();
        RandomForce();
        RandomTorque();
        RandomSpawnPos();
    }
    void RandomForce()
    {
        targetRB.AddForce(Vector3.up * Random.Range(minForce, maxForce), 
            ForceMode.Impulse);
    }
    void RandomTorque()
    {
        targetRB.AddTorque(Random.Range(minTorque, maxTorque), 
            Random.Range(minTorque, maxTorque), 
            Random.Range(minTorque, maxTorque), ForceMode.Impulse);
    }
    void RandomSpawnPos()
    {
        transform.position = new Vector3(Random.Range(minXPos, maxXPos), 
            ySpawnPos);
    }

    private void OnMouseDown()
    {
        Destroy(gameObject);
    }

    // This makes the sensor destroy targets that collide with it
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
