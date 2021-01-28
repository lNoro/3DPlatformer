﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    public GameObject Water;

    private void OnTriggerStay(Collider other)
    {
        if(!other.CompareTag("Player"))
            return;
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (other.GetComponent<PlayerController>().Key)
            {
                GetComponentInChildren<Animator>().SetBool("RaiseWater", true);
                Water.GetComponent<Animator>().SetBool("RaiseWater", true);
                FindObjectOfType<Turntable>().PlaySound("Water");
                FindObjectOfType<Turntable>().StopBattleTheme();
                enabled = false;
            }
            else
            {
                FindObjectOfType<Turntable>().PlaySound("Error");
            }
        }
    }

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
