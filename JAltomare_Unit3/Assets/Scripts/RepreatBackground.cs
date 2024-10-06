using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepreatBackground : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatWidth;
    // Start is called before the first frame update
    void Start()
    {
        // get the initial position of the background
        startPos = transform.position;
        repeatWidth = GetComponent<BoxCollider>().size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < startPos.x - repeatWidth) 
        { 
            transform.position = startPos;
        }
    }
}
