using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Ultra dumb script
 */
public class SpawnPoint : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, Vector3.one);
    }
}
