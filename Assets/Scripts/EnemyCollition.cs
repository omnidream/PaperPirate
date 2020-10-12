using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCollition : MonoBehaviour
{

     Rigidbody rb;
     EnemyStats enemyStats;
     PlayerStats playerStats;
     public GameObject rewardCoinsPrefab;
     public Slider slider;
     bool isAlive;
     int normalCoinReward = 10, bigCoinReward = 30;
     

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody> ();
        enemyStats = GetComponent<EnemyStats> ();
        isAlive = true;
    }

    void Update()
    {
        UpdateEnemyHealthBar();
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "canonBall")
        {
            enemyStats.hitPoints--;
            UpdateEnemyHealthBar();
            if(ShouldISink() && isAlive == true)
                SinkEnemyShip();
        }

        if(collision.gameObject.tag == "Player")
        {
            playerStats = collision.gameObject.GetComponent<PlayerStats>();
            enemyStats.hitPoints = enemyStats.hitPoints - 3;
            UpdateEnemyHealthBar();
            if(ShouldISink() && isAlive == true)
                SinkEnemyShip();
        }
    }

    bool ShouldISink()
    {
        bool sink = false;
        if(enemyStats.hitPoints <= 0)
            sink = true;
        return sink;
    }

    void SinkEnemyShip()
    {
        isAlive = false;
        SpawnCoins();
        rb.constraints = RigidbodyConstraints.None;
        rb.mass = 100000f;
    }

    void SpawnCoins()
    {
        GameObject myCoins = Instantiate(rewardCoinsPrefab, transform.position, Quaternion.identity);
        CoinValue myCoinValue = myCoins.GetComponent<CoinValue>();
        myCoinValue.coinValue = AssignValueToCoins();
    }

    public void UpdateEnemyHealthBar()
    {
        slider.value = enemyStats.hitPoints;
    }

    int AssignValueToCoins()
    {
        int value = normalCoinReward;
        if(enemyStats.isABigBoat)
            value = bigCoinReward;
        return value;
    }
}
