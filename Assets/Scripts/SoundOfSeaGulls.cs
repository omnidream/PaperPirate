using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOfSeaGulls : MonoBehaviour
{

    public AudioClip seaGullOne;
    public AudioClip seaGullTwo;
    AudioSource audioSource;

    void Start()
{
    audioSource = GetComponent<AudioSource>();
    audioSource.volume = 0.70f;

}

    // Update is called once per frame
    void Update()
    {
        int rnd = Random.Range(0, 2000);
        if(rnd == 1)
            audioSource.PlayOneShot(seaGullOne);
        else if(rnd == 2)
            audioSource.PlayOneShot(seaGullTwo);
            
    }
}
