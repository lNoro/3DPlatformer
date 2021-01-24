using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float DistanceAway;
    public float DistanceUp;
    public float Smooth;
    public String Y_Axis = "Mouse Y";
    public Transform FollowObject;
    public LayerMask CameraCollide;
    public LayerMask CeilingCollide;

    private Vector3 m_ToPosition;
    private float m_DistanceAway;
    private float m_DistanceUp;
    private float m_YInput;


    private void Update()
    {
        if (Y_Axis.Length != 0)
            m_YInput = Input.GetAxis(Y_Axis);
        
        if (Mathf.Abs(m_YInput) > 0)
        {
            DistanceUp = Mathf.Lerp(DistanceUp, DistanceUp + m_YInput, Time.deltaTime * 5f);
            
            if (DistanceUp > 4f)
                DistanceUp = 4f;
            else if (DistanceUp < 0.35f)
                DistanceUp = 0.35f;
        }
        
        CheckDistanceUp();
    }

    /*
     * First Check if Camera collides with Walls on the way to DistanceAway
     * Recalculate DistanceAway if Wall hit
     * Update Camera Position and LookAt Character
     */
    private void LateUpdate()
    {
        CheckDistanceAway();
        m_ToPosition = FollowObject.position + Vector3.up * m_DistanceUp - FollowObject.forward * m_DistanceAway;
        transform.position = Vector3.Lerp(transform.position, m_ToPosition, Time.deltaTime * Smooth);
        transform.LookAt(FollowObject);
    }

    /*
     * Check Max Distance first
     * If Max Distance collides with something, recalculate the Distance
     */
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
    
    /*
    * Check Max Distance first
    * If Max Distance collides with something, recalculate the Distance
    */
    private void CheckDistanceUp()
    {
        bool hit = HitCeiling(DistanceUp);

        if (hit)
        {
            m_DistanceUp = Mathf.Lerp(m_DistanceUp,2f, Time.deltaTime*3f);
            DistanceUp = m_DistanceUp;
        }
        else
        {
            m_DistanceUp = DistanceUp;
        }
    }

    /*
     * Send Raycast with distance_p length, on the way back from Character
     * If Raycast collide with Wall return true, else false
     */
    private bool Hit(float distance_p)
    {
        return Physics.Raycast(FollowObject.position + Vector3.up * DistanceUp, FollowObject.forward * -1f, distance_p, CameraCollide);
    }
    
    /*
    * Send Raycast with distance_p length, on the way up from Character
    * If Raycast collide with Ceiling return true, else false
    */
    private bool HitCeiling(float distance_p)
    {
        return Physics.Raycast(FollowObject.position + FollowObject.forward * (-1f * DistanceAway), FollowObject.up, distance_p, CeilingCollide);
    }
}
