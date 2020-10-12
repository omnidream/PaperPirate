using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SpawnABoat : MonoBehaviour
{
    public GameObject myBoatModel;
    public GameObject spawnPosition;
    public GameObject myPlayer;
    Vector3 scaleChange;
    int bigBoatHitPoints = 15;
    int normalBoatHitPoints = 10;

    // Start is called before the first frame update
    void Start()
    {
        MakeItANormalBoat();
    }

    public void SpawnABoatAtSpawnpoint()
    {   
        if(ShouldTheNewBoatBeABigOne())
            MakeItABigBoat();
        else
            MakeItANormalBoat();
    }

    bool ShouldTheNewBoatBeABigOne()
    {
        bool bigBoat = false;
        if((myPlayer.GetComponent<PlayerStats>().totalKills % 4) == 0)
            bigBoat = true;
        return bigBoat;
    }

    void MakeItABigBoat()
    {
        GameObject myBoat = InstantiateABoat();
        EnemyStats enemyStats = GetEnemyStats(myBoat);
        myBoat.name = "BigBoat";
        enemyStats.isABigBoat = true;
        enemyStats.hitPoints = bigBoatHitPoints;
        scaleChange = new Vector3(0.025f, 0.025f, 0.025f);
        myBoat.transform.localScale = scaleChange;
    }

    void MakeItANormalBoat()
    {
        GameObject myBoat = InstantiateABoat();
        EnemyStats enemyStats = GetEnemyStats(myBoat);
        myBoat.name = "NormalBoat";
        enemyStats.isABigBoat = false;
        enemyStats.hitPoints = normalBoatHitPoints;
        scaleChange = new Vector3(0.01f, 0.01f, 0.01f);
        myBoat.transform.localScale = scaleChange;
    }

    GameObject InstantiateABoat()
    {
        return Instantiate(myBoatModel, spawnPosition.transform);
    }

    EnemyStats GetEnemyStats(GameObject myBoat)
    {
        return myBoat.GetComponent<EnemyStats>();
    }


}
