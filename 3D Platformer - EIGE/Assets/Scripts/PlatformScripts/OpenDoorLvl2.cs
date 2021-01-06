using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Open This Door After Player has enough Collectables
 */
public class OpenDoorLvl2 : MonoBehaviour
{
    public int FinalScore;
    
    /*
     * If Player reached Final Score Open, if not do nothing
     * Play Sound accordingly
     */
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>().Score >= FinalScore)
        {
            FindObjectOfType<Turntable>().PlaySound("Open");
            GetComponent<Animator>().SetTrigger("Open");
        }
        else
        {
            FindObjectOfType<Turntable>().PlaySound("NotEnough");
        }
    }
}
