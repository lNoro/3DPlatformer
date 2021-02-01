using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Elevator Script
 */
public class CallElevator : MonoBehaviour
{
    //Set Elevator GameObject in Inspector
    public GameObject Elevator;
    
    //Private Members
    private Animator m_ElevatorAnim;
    private String m_CurrentState = "Up";
    private static bool m_CallingAllowed = false;
    private Transform m_Parent;

    private void Awake()
    {
        //reset Calling allowed when level reloaded
        if (m_CallingAllowed)
            m_CallingAllowed = false;
    }

    private void Start()
    {
        m_ElevatorAnim = Elevator.GetComponent<Animator>();
        m_Parent = transform.parent;
    }

    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<Narrator>().ShowInteractable("E :    Use Elevator");
    }

    /*
     * If Player Presses E in Range of Button Try to Call the Elevator
     * Play Sound according to if Elevator is callable
     */
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryCall();
            PlaySound();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        FindObjectOfType<Narrator>().HideInteractable();
    }

    /*
     * Elevator is deactivated at first
     * Activate Elevator when Player reached the top floor
     * Activation is saved in static member
     * According to where the Elevator is, move up or down
     */
    private void TryCall()
    {
        if (!m_CallingAllowed)
        {
            if (m_Parent.CompareTag("Up"))
                ActivateElevator();
            else
            {
                FindObjectOfType<Narrator>().ShowInteractable("Currently not working");
                return;
            }
        }
        
        if (m_CurrentState == "Up")
        {
            m_ElevatorAnim.SetTrigger("GoDown");
            m_CurrentState = "Down";
        }
        else
        {
            m_ElevatorAnim.SetTrigger("GoUp");
            m_CurrentState = "Up";
        }
    }

    public void ActivateElevator()
    {
        m_CallingAllowed = true;
    }

    public void DisableElevator()
    {
        m_CallingAllowed = false;
    }

    private void PlaySound()
    {
        FindObjectOfType<Turntable>().PlaySound(!m_CallingAllowed ? "Error" : "ElevatorCall");
    }
}
