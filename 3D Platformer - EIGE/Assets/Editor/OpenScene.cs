using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class OpenScene : MonoBehaviour
{
    [MenuItem("Open Scene/Start Screen")]
    static void StartScreen()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/Scenes/StartScene.unity");
    }
    
    [MenuItem("Open Scene/Level 1")]
    static void Level1()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/Scenes/Level1.unity");
    }
    
    [MenuItem("Open Scene/Palace Night")]
    static void Palace()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/Scenes/Palace Night.unity");
    }
    
    [MenuItem("Open Scene/End")]
    static void End()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/Scenes/End.unity");
    }
    
    [MenuItem("Open Scene/End Credits")]
    static void EndCredits()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/Scenes/EndCredits.unity");
    }   
}
