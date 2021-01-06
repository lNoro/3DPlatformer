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

    private void OnTriggerEnter(Collider other)
    {
        if (!m_DialogShown)
        {
            FindObjectOfType<Narrator>().StartDialog(Dialog);
            m_DialogShown = true;
        }
    }
}
