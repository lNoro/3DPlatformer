using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Boss Healthbar Script
 */
public class Healthbar : MonoBehaviour
{
    public float LerpSpeed;
    public Animator Animator;

    private bool m_Die;
    private Image m_Healthbar;


    private void Start()
    {
        m_Healthbar = GetComponent<Image>();
    }

    //If Boss died, lerp its health to 0
    void Update()
    {
        if (m_Die)
        {
            m_Healthbar.fillAmount = Mathf.Lerp(m_Healthbar.fillAmount, 0f, Time.deltaTime * LerpSpeed);
        }
    }

    public void Die()
    {
        m_Die = true;
        StartCoroutine(Hide());
    }

    //After Lerping health to 0, hide healthbar again
    IEnumerator Hide()
    {
        yield return new WaitForSeconds(3f);
        Animator.SetBool("Show", false);
        enabled = false;
    }
}
