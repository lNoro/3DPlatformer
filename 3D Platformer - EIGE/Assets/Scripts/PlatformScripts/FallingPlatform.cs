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
    private void Update()
    {
        if (GetComponent<Rigidbody>().useGravity && !m_Spawning)
        {
            m_Spawning = true;
            StartCoroutine(RespawnStairs());
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        GameObject otherGameObject = other.gameObject;
        if (otherGameObject.CompareTag("Player"))
        {
            GetComponent<Animator>().SetTrigger("Fall");
        }
    }

    IEnumerator RespawnStairs()
    {
        yield return new WaitForSeconds(7.5f);

        GetComponent<Animator>().enabled = true;
        GetComponent<Animator>().SetTrigger("Spawn");

        m_Spawning = false;
    }
}
