using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * InvisibleWallScript
 */
public class InvisibleWall : MonoBehaviour
{
    public float FadeTime;
    private bool m_Collided = false;
    private Renderer m_Renderer;

    private void Start()
    {
        m_Renderer = GetComponent<Renderer>();
    }

    /*
     * If player collided with game object,
     * "lerp" materials alpha value to 255,
     * and back to 0 again
     */
    void Update()
    {
        var material = m_Renderer.material;
        var color = material.color;
        if(m_Collided == true)
        {
            material.color = new Color(color.r, color.g, color.b, color.a + (FadeTime * Time.deltaTime));

            if(material.color.a > 0.9)
            {
                m_Collided = false;
            }

        }else if(m_Collided == false && material.color.a > 0)
        {
            material.color = new Color(color.r, color.g, color.b, color.a - (FadeTime * Time.deltaTime));

            if(material.color.a < 0)
            {
                material.color = new Color(color.r, color.g, color.b, 0);
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        m_Collided = true;
    }

    //To see invisible walls in scene view
    private void OnDrawGizmos()
    {
        BoxCollider bc = GetComponent<BoxCollider>();
        if (bc == null)
            return;
        Gizmos.color = Color.magenta;
        Matrix4x4 old = Gizmos.matrix;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(GetComponent<BoxCollider>().center, GetComponent<BoxCollider>().size);
        Gizmos.matrix = old;
    }
}
