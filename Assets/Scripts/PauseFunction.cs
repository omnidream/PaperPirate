using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseFunction : MonoBehaviour
{

    public static bool isPaused = false;
    public GameObject gameView, menuView, myPlayer;

    public GameObject myPausScreen;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            if(isPaused)
                unPauseGame();
            else
                PauseGame();
        }
    }

    public void unPauseGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        AudioListener.pause = false;
        myPausScreen.SetActive(false);
    }

    void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;
        UnLockCursor();
        AudioListener.pause = true;
        myPausScreen.SetActive(true);
    }

    public static void UnLockCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
