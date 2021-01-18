using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gate : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<Narrator>().ShowInteractable("E :    Enter Voronoid");
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("Level1");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        FindObjectOfType<Narrator>().HideInteractable();
    }
}
