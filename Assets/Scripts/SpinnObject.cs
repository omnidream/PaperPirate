using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnObject : MonoBehaviour
{
    private float spinnRate = 1f;
    
    void Start()
    {
        transform.Rotate(-90 , 0, 0, Space.Self);
        transform.position = new Vector3(transform.position.x, 3, transform.position.z);
    }

    void Update()
    {
        float spinn =+ spinnRate;
        transform.Rotate(0, 0, (spinn * Time.timeScale), Space.Self);
    }
}
