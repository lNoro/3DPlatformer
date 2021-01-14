using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : MonoBehaviour
{
    public Transform SpawnPoint;
    public Transform SpawnPointCopy;
    
    private ParticleSystem m_Fire;
    private bool m_BonfireLit = false;
    // Start is called before the first frame update
    void Start()
    {
        m_Fire = GetComponentInChildren<ParticleSystem>();
        m_Fire.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_BonfireLit)
        {
            m_Fire.Play();
            enabled = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            m_BonfireLit = true;
            SpawnPoint.position = SpawnPointCopy.position;
        }
    }
}
