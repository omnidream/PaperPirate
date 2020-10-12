using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public int playerHitPoints;
    public int respawnPlayerHitPoints;
    float respawnPlayerMass;
    public int playerTreasure;
    public Slider slider;
    private Rigidbody rb;
    public int totalKills;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody> ();
        respawnPlayerHitPoints = playerHitPoints;
        respawnPlayerMass = rb.mass;
        playerTreasure = 0;
        slider.maxValue = playerHitPoints;
    }
    public void ResetPlayerStats()
    {
            transform.position = new Vector3(0, 4, 0);
            rb.mass = respawnPlayerMass;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            playerHitPoints = respawnPlayerHitPoints;
            UpdatePlayerHealthBar();
    }

        public void UpdatePlayerHealthBar()
    {
        slider.value = playerHitPoints;
    }


}
