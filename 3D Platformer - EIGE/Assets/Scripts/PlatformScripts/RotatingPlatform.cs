using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    public float RotatingSpeed = 25f;
    private Rigidbody m_PlayersRigidbody;
    private bool m_ApplyForce = false;
    public float Force = 1f;


    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, Vector3.up, 1f * Time.deltaTime * RotatingSpeed);
    }

    private void FixedUpdate()
    {
        if (m_ApplyForce)
        {
            m_PlayersRigidbody.AddForce(transform.right * Force);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        GameObject otherGameObject = other.gameObject;
        m_PlayersRigidbody = other.gameObject.GetComponent<Rigidbody>();
        if (otherGameObject.CompareTag("Player"))
        {
            m_ApplyForce = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        m_ApplyForce = false;
        m_PlayersRigidbody = null;
    }
}
