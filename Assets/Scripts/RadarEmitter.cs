using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarEmitter : MonoBehaviour
{
    private RaycastHit vision;
    private float rayLength;
    public AiControlls aiControlls;

    void Start()
    {
        rayLength = 30.0f;
    }

    void FixedUpdate()
    {
        if(IsSomethingInTheWay())
            AvoidItByTurning();
    }

    public bool IsSomethingInTheWay()
    {
        bool somethingISinTheWay = false;
        if(Physics.Raycast(this.transform.position, this.transform.forward, out vision, rayLength))
            somethingISinTheWay = true;
        else
            somethingISinTheWay = false;
        return somethingISinTheWay;
    }

    void AvoidItByTurning()
    {
        aiControlls.Evade();
    }
}
