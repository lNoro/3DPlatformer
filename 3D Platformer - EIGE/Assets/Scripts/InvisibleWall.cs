using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleWall : MonoBehaviour
{
    public float FadeTime;
    private bool collided = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var material = GetComponent<Renderer>().material;
        var color = material.color;
        if(collided == true)
        {
            material.color = new Color(color.r, color.g, color.b, color.a + (FadeTime * Time.deltaTime));

            if(material.color.a > 0.9)
            {
                collided = false;
            }

        }else if(collided == false && material.color.a > 0)
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
        collided = true;
    }

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
