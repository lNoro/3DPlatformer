using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/*
 * Menu behaviour
 */
public class Menu : MonoBehaviour
{
    //Play Click Sound
    public void Click()
    {
        FindObjectOfType<Turntable>().PlaySound("Click");
    }
    
    //Start Sound
    public void StartClick()
    {
        FindObjectOfType<Turntable>().PlaySound("PressStart");
    }
    
    //Close App
    public void QuitGame()
    {
        Application.Quit();
        Click();
    }

    //Set Volume of all sounds specified in turntable inspector
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
