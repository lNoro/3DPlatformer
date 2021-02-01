using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Moving Platform Script - Only Append PlayerTransform to the GameObject of this Script
 */
public class MovingPlatform : MonoBehaviour
{
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
