using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerCollition : MonoBehaviour
{
    public AudioClip impact, coinLootSound, paperLifeSound;
    AudioSource audioSource;
    public Text myCoinText;
    private Rigidbody rb;
    PlayerStats myPlayerStats;
    public GameObject coinLoot;
    public Camera myCamera;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        myPlayerStats = GetComponent<PlayerStats>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "EnemyCanonBall" || collision.gameObject.tag == "EnemyShip")
        {
            audioSource.PlayOneShot(impact);
            myPlayerStats.playerHitPoints--;
            myPlayerStats.UpdatePlayerHealthBar();
            if(ShouldISink())
                SinkPlayerBoat();
        }
    }

    private void OnTriggerEnter(Collider triggerObject)
    {
        if(triggerObject.gameObject.tag == "Coins")
        {
            coinLoot = triggerObject.gameObject;
            CoinValue myCoinValue = coinLoot.GetComponent<CoinValue>();
            myPlayerStats.playerTreasure += myCoinValue.coinValue;
            Destroy(triggerObject.gameObject);
            audioSource.PlayOneShot(coinLootSound);
            UpdateMyCoinsUI();
        }

        if(triggerObject.gameObject.tag == "PaperLife")
        {
            GameObject paperLife = triggerObject.gameObject;
            RefillPlayerHitPoints();
            myPlayerStats.UpdatePlayerHealthBar();
            audioSource.PlayOneShot(paperLifeSound);
            Destroy(paperLife);
        }
    }

    void UpdateMyCoinsUI()
    {
        myCoinText.text = myPlayerStats.playerTreasure.ToString();
    }

    void RefillPlayerHitPoints()
    {
        myPlayerStats.playerHitPoints = myPlayerStats.respawnPlayerHitPoints;
    }

    bool ShouldISink()
    {
        bool sink = false;
        if(myPlayerStats.playerHitPoints <= 0)
            sink = true;
        return sink;
    }

    void SinkPlayerBoat()
    {
        rb.mass = 100000f;
        myCamera.GetComponent<CameraControl>().enabled = false;
    }
}
