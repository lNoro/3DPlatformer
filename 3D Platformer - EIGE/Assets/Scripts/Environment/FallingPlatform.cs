using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

/*
 * Falling Platform when Player is Jumping on Platform
 */
public class FallingPlatform : MonoBehaviour
{
    private bool m_Spawning = false;
    private Rigidbody m_Rigidbody;
    private Animator m_Animator;

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Animator = GetComponent<Animator>();
    }

    /*
     * Checks if Platform has fallen, if so -> Respawn
     */
    private void Update()
    {
        if (m_Rigidbody.useGravity && !m_Spawning)
        {
            m_Spawning = true;
            StartCoroutine(RespawnStairs());
        }
    }

    /*
     * Let Platform fall, if player collides with it
     * Animator plays Jiggle animation, after which
     * the rigidbody of this game object is set to
     * use gravity
     */
    private void OnCollisionEnter(Collision other)
    {
        GameObject otherGameObject = other.gameObject;
        if (otherGameObject.CompareTag("Player") && !m_Spawning)
        {
            GetComponent<Animator>().SetTrigger("Fall");
        }
    }

    /*
     * After 7.5 seconds, let Platforms float to
     * origin place
     */
    IEnumerator RespawnStairs()
    {
        yield return new WaitForSeconds(7.5f);

        //This is needed to not let platform rise in the wrong rotation
        transform.rotation = Quaternion.Euler(0, -90f, 0);
        
        //Animator handles Rigidbody adjustments
        m_Animator.enabled = true;
        m_Animator.SetTrigger("Spawn");

        m_Spawning = false;
    }
}
