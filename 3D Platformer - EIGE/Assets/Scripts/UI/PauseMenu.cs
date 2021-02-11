using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Pause menu behaviour
 */
public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused = false;
    public GameObject PauseMenuUI;

    //Activate pause with Esc
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private void Pause()
    {
        PauseMenuUI.SetActive(true);
        GamePaused = true;
        //freeze time
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        GamePaused = false;
        //unfreeze time
        Time.timeScale = 1f;
    }

    public void ReturnMenu()
    {
        Time.timeScale = 1f;
        //Reset everything
        FindObjectOfType<PlayerController>().ResetProgress();
        
        //Reset Music
        FindObjectOfType<Turntable>().ReturnMenu();
        
        //Load Startscene
        SceneManager.LoadScene("StartScene");
    }
}
