using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Falling Platform when Player is Jumping on Platform
 */
public class FallingPlatform : MonoBehaviour
{
    
    private void OnCollisionEnter(Collision other)
    {
        GameObject otherGameObject = other.gameObject;
        if (otherGameObject.CompareTag("Player"))
        {
            GetComponent<Animator>().SetTrigger("Fall");
        }
    }
}
