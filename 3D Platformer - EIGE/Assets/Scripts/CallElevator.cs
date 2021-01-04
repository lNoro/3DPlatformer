using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallElevator : MonoBehaviour
{
    public GameObject Elevator;
    private Animator m_ElevatorAnim;

    private static String m_CurrentState = "Up";
    private static bool m_CallingAllowed = false;
    private Transform m_Parent;

    private void Start()
    {
        m_ElevatorAnim = Elevator.GetComponent<Animator>();
        m_Parent = transform.parent;
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TryCall();
            PlaySound();
        }
    }

    private void TryCall()
    {
        if (!m_CallingAllowed)
        {
            if (m_Parent.CompareTag("Up"))
                ActivateElevator();
            else
                return;
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
