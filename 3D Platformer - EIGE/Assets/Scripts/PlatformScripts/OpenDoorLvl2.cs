using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorLvl2 : MonoBehaviour
{
    public int FinalScore;
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
