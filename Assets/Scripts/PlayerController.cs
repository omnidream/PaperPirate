using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static PauseFunction;


public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public int speed = 10;
    public Text timeLeft;
    float playedTime;
    public float timeLeftInNumbers = 300, rotationSpeed = 0.7f;
    public Canon canonLeft;
    public Canon canonRight;
    public GameObject gameView, menuView, endRoundScreen, inputInitialsField;
    public Text endRoundText;

    void Start () {
        rb = GetComponent<Rigidbody> ();
    }

    void Update () 
    {
        playedTime += (1 * Time.deltaTime);
        UpdateTimeLeft();

        if(UpdateTimeLeft() == 0)
            EndRound();

        if(!isPaused)
        {
            if(Input.GetButtonDown("Fire1")) 
                canonLeft.Fire();


            if(Input.GetButtonDown("Fire2")) 
                canonRight.Fire();
        }
    }
    void FixedUpdate ()
    {
         if (Input.GetKey(KeyCode.A))
            TurnLeft();
        
        if (Input.GetKey(KeyCode.D))
            TurnRight();

        float moveForward = -Input.GetAxis ("Vertical");
        float turn = -Input.GetAxis ("Horizontal");
        Vector3 movement = new Vector3 (moveForward, 0.0f, 0.0f);
        rb.AddRelativeForce((movement * (speed*2000)));

     }

     void TurnLeft()
     {
        transform.Rotate(0, -rotationSpeed, 0, Space.Self);  
     }
     void TurnRight()
     {
        transform.Rotate(0, rotationSpeed, 0, Space.Self);        
     }

     int UpdateTimeLeft() //Jag har noterat att det fanns en inbygd funktion för detta. Lite sent dock.
     {
         int timeLeftAsInt = (int)(timeLeftInNumbers - playedTime);
         timeLeft.text = ("Time left: " + timeLeftAsInt.ToString());
         return timeLeftAsInt;
     }

     void EndRound()
     {
        PauseFunction.UnLockCursor();
        endRoundScreen.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
        AudioListener.pause = true;
        ChangeEndRoundScreenText();
     }

    void ChangeEndRoundScreenText()
    {
        PlayerStats myPlayerStats = GetComponent<PlayerStats>();
        endRoundText.text = ($"Congrats! You collected {myPlayerStats.playerTreasure} coins and managed to sink {myPlayerStats.totalKills} ship/s.");
    }

    public void ReturnToTitleScreen()
    {
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
        Time.timeScale = 1;
        isPaused = false;
        gameView.SetActive(false);
        menuView.SetActive(true);
    }
}
