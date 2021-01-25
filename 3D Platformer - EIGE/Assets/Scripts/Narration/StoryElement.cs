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

    private PhysicMaterial m_Slippery;

    public bool DialogShown
    {
        get => m_DialogShown;
        set => m_DialogShown = value;
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
