﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public GameObject ParticlePrefab;
    
    /*
     * Collectables play Sound, increase Players Score and destroy themselves
     */
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Key"))
            {
                
                other.gameObject.GetComponent<PlayerController>().CollectKey();
                FindObjectOfType<Turntable>().PlaySound("Collect");
                Destroy(gameObject);
                return;
            }
            Instantiate(ParticlePrefab, transform.position, ParticlePrefab.transform.rotation);
            other.gameObject.GetComponent<PlayerController>().CollectCoin();
            FindObjectOfType<Turntable>().PlaySound("Collect");
            Destroy(gameObject);
        }
    }
}
