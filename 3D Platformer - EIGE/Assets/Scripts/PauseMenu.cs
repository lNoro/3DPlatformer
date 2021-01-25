using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused = false;
    public GameObject PauseMenuUI;

    // Update is called once per frame
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
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        GamePaused = false;
        Time.timeScale = 1f;
    }

    public void ReturnMenu()
    {
        Time.timeScale = 1f;
        //Reset everything
        FindObjectOfType<PlayerController>().ResetProgress();
        
        //Load Startscene
        SceneManager.LoadScene("StartScene");
    }
}
