using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gate : MonoBehaviour
{
    public bool DoorInLevel;
    public bool Disabled;

    private void OnTriggerEnter(Collider other)
    {
        if(!Disabled && other.CompareTag("Player"))
            FindObjectOfType<Narrator>().ShowInteractable("E :    Enter Voronoid");
    }

    private void OnTriggerStay(Collider other)
    {
        if (!Disabled && !other.CompareTag("Player"))
            return;
        
        if (!Disabled && Input.GetKeyDown(KeyCode.E))
        {
            FindObjectOfType<Turntable>().PlaySound("Voronoid");
            if (!DoorInLevel)
            {
                GetComponent<LoadScene>().LoadLevel();
                return;
            }

            other.GetComponent<PlayerController>().EnterDoor(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(!Disabled && other.CompareTag("Player"))
            FindObjectOfType<Narrator>().HideInteractable();
    }
}
