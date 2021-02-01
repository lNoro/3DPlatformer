using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Script of Deathzone
 */
public class RestartOnCollide : MonoBehaviour
{
    /*
     * If player collides with GameObject attached to this script, Reload Scene
     */
    private void OnCollisionEnter(Collision other)
    {
        GameObject otherGameObject = other.gameObject;
        if (otherGameObject.CompareTag("Player"))
        {
            otherGameObject.GetComponent<PlayerController>().Spawn();
        }
    }
}
