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

    private Vector3 m_ToPosition;

    private void LateUpdate()
    {
        m_ToPosition = FollowObject.position + Vector3.up * DistanceUp - FollowObject.forward * DistanceAway;
        transform.position = Vector3.Lerp(transform.position, m_ToPosition, Time.deltaTime * Smooth);
        transform.LookAt(FollowObject);
    }
}
