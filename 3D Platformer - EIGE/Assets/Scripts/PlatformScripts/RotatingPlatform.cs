using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Rotating Platform Script
 */
public class RotatingPlatform : MonoBehaviour
{
    public float RotatingSpeed = 25f;
    
    // private Rigidbody m_PlayersRigidbody;
    // private bool m_ApplyForce = false;
    // public float Force = 1f;


    // Update is called once per frame, Rotate around Pivot Center Point
    void Update()
    {
        transform.RotateAround(transform.position, Vector3.up, 1f * Time.deltaTime * RotatingSpeed);
    }

    /*
     * If Player stands on Platform, apply force in Rotating Direction
     */
    // private void FixedUpdate()
    // {
    //     if (m_ApplyForce)
    //     {
    //         m_PlayersRigidbody.AddForce(transform.right * Force);
    //     }
    // }

    /*
     * Register Player if he jumps on Platform
     */
    private void OnCollisionEnter(Collision other)
    {
        // GameObject otherGameObject = other.gameObject;
        // m_PlayersRigidbody = other.gameObject.GetComponent<Rigidbody>();
        // if (otherGameObject.CompareTag("Player"))
        // {
        //     m_ApplyForce = true;
        // }
    }

    /*
     * Deregister Player from this Script
     */
    private void OnCollisionExit(Collision other)
    {
        // m_ApplyForce = false;
        // m_PlayersRigidbody = null;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        GameObject otherGameObject = other.gameObject;
        if (otherGameObject.CompareTag("Player"))
        {
            otherGameObject.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject otherGameObject = other.gameObject;
        if (otherGameObject.CompareTag("Player"))
        {
            otherGameObject.transform.SetParent(null);
        }
    }
}
