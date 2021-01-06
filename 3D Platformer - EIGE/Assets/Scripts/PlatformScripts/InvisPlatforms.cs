using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Unfinished Script of Invisible Turning Platform
 */
public class InvisPlatforms : MonoBehaviour
{    
    public float FadeTime = 5f;
    private bool m_Collided = false;


    // Update is called once per frame
    void Update()
    {
        var material = GetComponent<Renderer>().material;
        var color = material.color;
        if(m_Collided)
        {
            material.color = new Color(color.r, color.g, color.b, color.a - (FadeTime * Time.deltaTime));
            if(material.color.a <= 0)
            {
                material.color = new Color(color.r, color.g, color.b, 0);
                m_Collided = false;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        m_Collided = true;
    }
}
