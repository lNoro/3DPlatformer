using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Define next Scene to load in inspector
 */
public class LoadScene : MonoBehaviour
{
    public string NextLevel;
    public Animator Transition;
    public void LoadLevel()
    {
        StartCoroutine(LoadLevelWithTrans(NextLevel));
    }

    public void Quit()
    {
        Application.Quit();
    }

    /*
     * Start Fade animation, and load next scene after
     */
    IEnumerator LoadLevelWithTrans(string sceneName_p)
    {
        Transition.SetTrigger("StartTransition");
        
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(sceneName_p);
    }
}
