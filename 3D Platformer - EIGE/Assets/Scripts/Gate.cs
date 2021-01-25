using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gate : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            FindObjectOfType<Narrator>().ShowInteractable("E :    Enter Voronoid");
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            GetComponent<LoadScene>().LoadLevel();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
            FindObjectOfType<Narrator>().HideInteractable();
    }
}
