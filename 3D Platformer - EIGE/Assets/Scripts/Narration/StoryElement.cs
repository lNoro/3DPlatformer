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

    /*
     * Set this to true, if dialog should not be displayed anymore when entering trigger
     */
    public bool DialogShown
    {
        get => m_DialogShown;
        set => m_DialogShown = value;
    }

    /*
     * Invokes StartDialog method of Narrator
     * Gives Player ability to sprint, double jump if tag of this
     * game object is set so
     */
    private void OnTriggerEnter(Collider other)
    {
        //temporarily deactivate slippery physic collider of player
        //This is only to not slide player after he can't move anymore
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

    /*
     * Reactivates the physic collider of the player
     */
    private void OnTriggerExit(Collider other)
    {
        other.material = m_Slippery;
    }
}
