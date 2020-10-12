using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PauseFunction;

public class AiControlls : MonoBehaviour
{
    private Rigidbody rb;
    float rotationSpeed = 0.75f;
    public float speed = 0.75f;
    float aliveTimer, directionDuration;
    int rnd = 2;
    
    void Start()
    {
        rb = GetComponent<Rigidbody> ();
        GenerateANewDirection();
        GenerateANewDuration();
    }

    void Update()
    {
        aliveTimer += (1 * Time.deltaTime  * Time.timeScale);
        if(!isPaused)
        {
            if (rnd == 0)
                StartCoroutine(TurnLeft());
            else if (rnd == 1)
                StartCoroutine(TurnRight());
            else if (rnd >= 2) 
                StartCoroutine(Forward());
            Thrust();
        }
    }

    public IEnumerator TurnLeft() 
    {
        if(KeepThisDirection())
        {
            Thrust();
            transform.Rotate(0, (-rotationSpeed * Time.timeScale), 0, Space.Self);
        }
        else
        {
            StopCoroutine(TurnLeft());
            GenerateANewDirection();
            GenerateANewDuration();
        }
        yield return null;
    }

    IEnumerator TurnRight() 
    {
        if(KeepThisDirection())
        {
            Thrust();
            transform.Rotate(0, (rotationSpeed * Time.timeScale), 0, Space.Self);
        }
        else
        {
            StopCoroutine(TurnRight());
            GenerateANewDirection();
            GenerateANewDuration();
        }
        yield return null;
    }

    IEnumerator Forward() 
    {
        if(KeepThisDirection())
        {
            Vector3 movement = new Vector3 (-1f, 0.0f, 0.0f);
            rb.AddRelativeForce((movement * (speed*2000)));
        }
        else
        {
            StopCoroutine(Forward());
            GenerateANewDirection();
            GenerateANewDuration();
        }
        yield return null;
    }

        public void Thrust()
    {
        Vector3 movement = new Vector3 (-1f, 0.0f, 0.0f);
        rb.AddRelativeForce((movement * (speed*2500)));
    }

    bool KeepThisDirection()
    {
        bool keepThisDirection = false;
        if(directionDuration > aliveTimer)
            keepThisDirection = true;
        return keepThisDirection;
    }

    void GenerateANewDuration()
    {
        int max;
        if(rnd == 0 || rnd == 1)
            max = 3;
        else
            max = 8;
        directionDuration = aliveTimer + Random.Range(1, max);
    }

    void GenerateANewDirection()
    {
        rnd = Random.Range(0, 6);
    }

    public void Evade()
    {
        rnd = 0;
        directionDuration = aliveTimer + Random.Range(1, 3);
        StartCoroutine(TurnLeft());
    }
}