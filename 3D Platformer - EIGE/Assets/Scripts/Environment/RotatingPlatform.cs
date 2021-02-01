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

    // Update is called once per frame, Rotate around Pivot Center Point
    void Update()
    {
        transform.RotateAround(transform.position, Vector3.up, 1f * Time.deltaTime * RotatingSpeed);
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
