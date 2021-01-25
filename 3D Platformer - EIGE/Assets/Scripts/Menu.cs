using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Menu : MonoBehaviour
{
    public void Click()
    {
        FindObjectOfType<Turntable>().PlaySound("Click");
    }
    
    public void QuitGame()
    {
        Application.Quit();
        Click();
    }

    public void SetVolume(float value_p)
    {
        FindObjectOfType<Turntable>().AdjustVolume(value_p);
    }

    public void Fullscreen(bool value_p)
    {
        Screen.fullScreen = value_p;
        Click();
    }

    public void SetQuality(int index_p)
    {
        QualitySettings.SetQualityLevel(index_p);
        Click();
    }
}
