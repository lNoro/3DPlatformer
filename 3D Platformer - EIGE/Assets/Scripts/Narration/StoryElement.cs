using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script which activates StoryTelling Mode when entering Trigger
 * Stores a Dialog Object which will be displayed then
 */
public class StoryElement : MonoBehaviour
{
    public Dialog Dialog;
    private bool m_DialogShown = false;
    private static bool m_Instantiated = false;

    private PhysicMaterial m_Slippery;

    private void Awake()
    {
        if (m_Instantiated)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        m_Instantiated = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        m_Slippery = other.material;
        other.material = null;
        if (!m_DialogShown)
        {
            FindObjectOfType<Narrator>().StartDialog(Dialog);
            m_DialogShown = true;
            if (gameObject.CompareTag("Sprint"))
            {
                other.GetComponent<PlayerController>().SprintAquired = true;
            }
            else if (gameObject.CompareTag("DoubleJump"))
            {
                other.GetComponent<PlayerController>().DoubleJumpAquired = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        other.material = m_Slippery;
    }
}
