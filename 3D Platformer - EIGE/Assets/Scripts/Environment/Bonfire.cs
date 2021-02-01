using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script of unlit Bonfire
 */
public class Bonfire : MonoBehaviour
{
    public Transform SpawnPoint;
    public Transform SpawnPointCopy;
    
    private ParticleSystem m_Fire;
    private bool m_BonfireLit = false;
    
    //Set particle System to stop at beginning of scene
    void Start()
    {
        m_Fire = GetComponentInChildren<ParticleSystem>();
        m_Fire.Stop();
    }

    //Start Particle System if pressed by Player
    void Update()
    {
        if (m_BonfireLit)
        {
            m_Fire.Play();
            enabled = false;
        }
    }

    //Show Instructions to light bonfire, if in range
    private void OnTriggerEnter(Collider other)
    {
        if (!m_BonfireLit)
        {
            FindObjectOfType<Narrator>().ShowInteractable("E :    Kindle Bonfire");
        }
    }

    //When lit, change spawn point and start particle system
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            m_BonfireLit = true;
            SpawnPoint.position = SpawnPointCopy.position;
            FindObjectOfType<Turntable>().PlaySound("Bonfire");
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        FindObjectOfType<Narrator>().HideInteractable();
    }
}
