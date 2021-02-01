using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Trigger for Rabbit movement
 */
public class RabbitMovement : MonoBehaviour
{
    public bool Move;
    public GameObject Rabbit;

    private bool m_Entered = false;

    /*
     * Rabbit moves until it enters trigger
     * if Player enters a trigger, rabbit starts movement again
     */
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
