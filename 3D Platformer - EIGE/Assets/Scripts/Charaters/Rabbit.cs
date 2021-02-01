using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Rabbit which just stupidly moves
 */
public class Rabbit : MonoBehaviour
{
    public float Speed;
    private float m_Movement = 0f;

    private Vector3 m_Velocity;
    private Rigidbody m_Rigidbody;
    private Animator m_Animator;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Animator = GetComponent<Animator>();
    }

    //Move rabbit according to set m_Movement
    void FixedUpdate()
    {
        m_Animator.SetFloat("Move", m_Movement);
        
        
        m_Velocity.z = m_Movement * Speed ;
        m_Velocity.y = m_Rigidbody.velocity.y;
        m_Velocity.x = m_Rigidbody.velocity.x;

        m_Rigidbody.velocity = transform.TransformDirection(m_Velocity);
    }

    //Give rabbit velocity in forward direction
    public void StartMovement()
    {
        m_Movement = 1f;
    }

    //Stop movement after a second
    public void StopMovement()
    {
        StopAllCoroutines();
        StartCoroutine(Stop());
    }

    IEnumerator Stop()
    {
        yield return new WaitForSeconds(1f);

        m_Movement = 0f;
    }
}
