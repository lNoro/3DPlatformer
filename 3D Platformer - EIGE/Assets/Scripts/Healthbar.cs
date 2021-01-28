using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    // Update is called once per frame
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

    IEnumerator Hide()
    {
        yield return new WaitForSeconds(3f);
        Animator.SetBool("Show", false);
        enabled = false;
    }
}
