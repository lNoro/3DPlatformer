using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitMovement : MonoBehaviour
{
    public bool Move;
    public GameObject Rabbit;

    private bool m_Entered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Move && m_Entered)
        {
            Rabbit.GetComponent<Rabbit>().StartMovement();
        }
        else if (other.CompareTag("Rabbit"))
        {
            Rabbit.GetComponent<Rabbit>().StopMovement();
            m_Entered = true;
        }
    }
}
