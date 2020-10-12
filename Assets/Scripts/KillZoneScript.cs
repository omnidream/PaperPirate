using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class KillZoneScript : MonoBehaviour
{
    public SpawnABoat spawnerOne;
    public SpawnABoat spawnerTwo;
    public SpawnABoat spawnerThree;
    public SpawnABoat spawnerFour;
    public GameObject myPlayer;
    public Camera myCamera;

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "EnemyShip")
        {
            Destroy(collision.gameObject);
            SpawnABoat randomSpawnpoint = GetRandomSpawnPoint();
            randomSpawnpoint.SpawnABoatAtSpawnpoint();
            RewardPlayerWithOneKill();
        }

        if(collision.gameObject.tag == "Player")
        {
            myPlayer.transform.position = new Vector3(0, 4, 0);
            PlayerStats myPlayerStats = myPlayer.GetComponent<PlayerStats>();
            myPlayerStats.ResetPlayerStats();
            myCamera.GetComponent<CameraControl>().enabled = true;
        }

        SpawnABoat GetRandomSpawnPoint()
        {
            SpawnABoat randomSpawnpoint;
            int rnd = Random.Range(0, 3);
            if(rnd == 0)
                randomSpawnpoint = spawnerOne;
            else if (rnd == 1)
                randomSpawnpoint = spawnerTwo;
            else if (rnd == 2)
                randomSpawnpoint = spawnerThree;
            else
                randomSpawnpoint = spawnerFour;
            return randomSpawnpoint;
        }
    }

    void RewardPlayerWithOneKill()
    {
        PlayerStats myPlayerStats = myPlayer.GetComponent<PlayerStats>();
        myPlayerStats.totalKills++;
    }
}
