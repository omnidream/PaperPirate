using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointToCamera : MonoBehaviour
{
    private Transform myCamera;

    void Start () 
    {
         myCamera = Camera.main.transform;
    }

    void LateUpdate()
    {
        transform.LookAt(myCamera);
    }
}
