using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
