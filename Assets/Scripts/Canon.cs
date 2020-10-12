using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    public AudioClip canonSound;
    AudioSource audioSource;
     public GameObject canonBall;
     public float force = 1000f;
     public ParticleSystem myParticles;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.05f;
        myParticles.GetComponent<ParticleSystem>();
    }

    public void Fire()
    {
        audioSource.PlayOneShot(canonSound);
        GameObject instCanonBall = Instantiate(canonBall, transform.position, Quaternion.identity);
        Rigidbody instCanonBallRB = instCanonBall.GetComponent<Rigidbody>();
        myParticles.Play();
        instCanonBallRB.AddForce(gameObject.transform.forward * (force*1000));
        Destroy(instCanonBall, 4f);
    }
}


     