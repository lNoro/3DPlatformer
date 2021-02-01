using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Voronoid Gates which can be entered
 */
public class Gate : MonoBehaviour
{
    public bool DoorInLevel;
    public bool Disabled;

    //Show Instructions if player in range
    private void OnTriggerEnter(Collider other)
    {
        if(!Disabled && other.CompareTag("Player"))
            FindObjectOfType<Narrator>().ShowInteractable("E :    Enter Voronoid");
    }

    //Load Next Level or Enter Gate inside Level
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
            
            //Sets player position behind the gate 
            other.GetComponent<PlayerController>().EnterDoor(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(!Disabled && other.CompareTag("Player"))
            FindObjectOfType<Narrator>().HideInteractable();
    }
}
