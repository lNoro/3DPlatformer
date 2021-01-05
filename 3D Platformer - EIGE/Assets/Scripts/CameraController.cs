using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float DistanceAway;
    public float DistanceUp;
    public float Smooth;
    public Transform FollowObject;
    public LayerMask CameraCollide;

    private Vector3 m_ToPosition;
    private float m_DistanceAway;

    private void LateUpdate()
    {
        CheckDistanceAway();
        m_ToPosition = FollowObject.position + Vector3.up * DistanceUp - FollowObject.forward * m_DistanceAway;
        transform.position = Vector3.Lerp(transform.position, m_ToPosition, Time.deltaTime * Smooth);
        transform.LookAt(FollowObject);
    }

    private void CheckDistanceAway()
    {
        bool hit = Hit(DistanceAway);

        if (hit)
        {
            float step = 0f;
            while (hit)
            {
                step += .005f;
                m_DistanceAway = Mathf.Lerp(DistanceAway, DistanceAway - step, 1);
                hit = Hit(m_DistanceAway);
            }
        }
        else
        {
            m_DistanceAway = DistanceAway;
        }
    }

    private bool Hit(float distance_p)
    {
        return Physics.Raycast(FollowObject.position, FollowObject.forward * -1f, distance_p, CameraCollide);
    }
}
