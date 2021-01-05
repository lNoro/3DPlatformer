using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Narrator : MonoBehaviour
{
    public VolumeProfile VolumeProfile;
    public TMP_Text DialogField;
    public TMP_Text ContinueField;
    public GameObject Player;
    
    private Vignette Vignette;
    private Queue<string> m_Lines;
    private string m_ContinueMessage;
    private bool m_NextLine = true;
    private bool m_Started = false;
    private bool m_ContinueMessageShown = false;
    // Start is called before the first frame update
    void Start()
    {
        VolumeProfile.TryGet<Vignette>(out Vignette);
        m_Lines = new Queue<string>();
    }

    public void StartDialog(Dialog dialog_p)
    {
        Player.GetComponent<PlayerController>().DisableMovement();
        m_Started = true;
        m_Lines.Clear();
        m_ContinueMessage = "";

        foreach (var sentence in dialog_p.Sentences)
        {
            m_Lines.Enqueue(sentence);
        }

        m_ContinueMessage = dialog_p.ContinueMessage;
    }

    private void DisplayNextSentence()
    {
        if (m_Lines.Count == 0)
        {
            if (!m_ContinueMessageShown)
            {
                StopAllCoroutines();
                StartCoroutine(TypeContinue());
            }
            
            if(Input.GetKeyDown(KeyCode.Q))
                EndDialog();
            
            return;
        }

        string line = m_Lines.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeLine(line));
    }

    IEnumerator TypeLine(string line_p)
    {
        m_NextLine = false;
        
        DialogField.text = "";
        foreach (var line in line_p.ToCharArray())
        {
            DialogField.text += line;
            yield return null;
        }
        m_NextLine = true;
    }
    
    private void EndDialog()
    {
        DialogField.text = "";
        ContinueField.text = "";
        Player.GetComponent<PlayerController>().EnableMovement();
        m_Started = false;
        m_ContinueMessageShown = false;
    }

    private void Update()
    {
        if (!m_Started)
        {
            Vignette.intensity.value = Mathf.Lerp(Vignette.intensity.value, 0f, Time.deltaTime * 5f);
            return;
        }
        
        if (m_NextLine)
        {
            DisplayNextSentence();
        }
        Vignette.intensity.value = Mathf.Lerp(Vignette.intensity.value, .45f, Time.deltaTime * 5f);
    }
    
    IEnumerator TypeContinue()
    {
        m_ContinueMessageShown = true;
        ContinueField.text = "";
        foreach (var line in m_ContinueMessage.ToCharArray())
        {
            ContinueField.text += line;
            yield return null;
        }
    }
}
