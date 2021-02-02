using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troll : MonoBehaviour
{
    private Animator m_TrollAnimator;
    // Start is called before the first frame update
    void Start()
    {
        m_TrollAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_TrollAnimator.SetBool("Troll", true);
        }
    }
}
