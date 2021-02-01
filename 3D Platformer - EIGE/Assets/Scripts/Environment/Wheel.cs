using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script of Water Wheel
 */
public class Wheel : MonoBehaviour
{
    public GameObject Water;

    private bool m_Used = false;

    /*
     * If player can interact with wheel,
     * play animations and sounds
     */
    private void OnTriggerStay(Collider other)
    {
        if(!other.CompareTag("Player"))
            return;
        
        if (!m_Used && Input.GetKeyDown(KeyCode.E))
        {
            if (other.GetComponent<PlayerController>().Key)
            {
                GetComponentInChildren<Animator>().SetBool("RaiseWater", true);
                Water.GetComponent<Animator>().SetBool("RaiseWater", true);
                FindObjectOfType<Turntable>().PlaySound("Water");
                FindObjectOfType<Turntable>().StopBattleTheme();
                enabled = false;
                m_Used = true;
            }
            else
            {
                FindObjectOfType<Turntable>().PlaySound("Error");
                FindObjectOfType<Narrator>().ShowInteractable("This needs a Key");
            }
        }
    }

    //Show instructions if player in range
    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player"))
            return;
        
        FindObjectOfType<Narrator>().ShowInteractable("E :    Use Wheel");
    }

    private void OnTriggerExit(Collider other)
    {
        if(!other.CompareTag("Player"))
            return;
        
        FindObjectOfType<Narrator>().HideInteractable();
    }
}
