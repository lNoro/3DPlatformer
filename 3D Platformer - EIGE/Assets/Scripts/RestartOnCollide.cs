using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartOnCollide : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        GameObject otherGameObject = other.gameObject;
        if (otherGameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
