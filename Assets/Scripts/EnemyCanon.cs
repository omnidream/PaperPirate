using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCanon : MonoBehaviour
{
    public AudioClip canonSound;
    AudioSource audioSource;
    public GameObject canonBall;
    public float force = 1000f;
    public ParticleSystem myParticles;
    private RaycastHit hit;
    public float rayLength;
    bool isFiering;
    public float rateOfFire;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.05f;
        myParticles.GetComponent<ParticleSystem>();
    }

    void FixedUpdate()
    {
        Debug.DrawRay(this.transform.position, this.transform.forward * rayLength, Color.black, 0.5f);
        if(Physics.Raycast(this.transform.position, this.transform.forward, out hit, rayLength))
        if(PlayerInSight() && isFiering == false)
            StartCoroutine(Fire());
    }

    bool PlayerInSight()
    {
        bool inSight = false;
        if(hit.collider.tag == "Player")
            inSight = true;
        return inSight;
    }

    IEnumerator Fire() 
    {
        isFiering = true;
        audioSource.PlayOneShot(canonSound);
        GameObject instCanonBall = Instantiate(canonBall, transform.position, Quaternion.identity);
        Rigidbody instCanonBallRB = instCanonBall.GetComponent<Rigidbody>();
        myParticles.Play();
        instCanonBallRB.AddForce(gameObject.transform.forward * (force*1000));
        Destroy(instCanonBall, 4f);
        yield return new WaitForSeconds(rateOfFire);
        isFiering = false;
   }
}





     