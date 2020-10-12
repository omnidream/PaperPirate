using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmulateFloat : MonoBehaviour
{
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {   
        Vector3 movement = new Vector3 (0, 0.5f, 0.0f);
        
        if(Random.Range(1, 5) == 2)
            rb.AddRelativeForce((movement * (20*2000)));
    }
}
