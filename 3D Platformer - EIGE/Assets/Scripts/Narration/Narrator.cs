using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

/*
 * Generic Singleton Dialog Manager
 * Store Sentences from Dialog Class in LinesArray
 * Display Lines on Canvas, with PostProcessing Effect Vignette
 */
public class Narrator : MonoBehaviour
{
    public bool PalaceScene;
    public VolumeProfile VolumeProfile;
    public TMP_Text DialogField;
    public TMP_Text ContinueField;
    public TMP_Text InteractField;
    public Image InteractBackground;
    public GameObject Player;

    private PlayerController m_PlayerController;
    private Vignette Vignette;
    private Queue<string> m_Lines;
    private string m_ContinueMessage;
    private bool m_NextLine = true;
    private bool m_Started = false;
    private bool m_StoryTeaser = false;


    // Start is called before the first frame update
    void Start()
    {
        m_Lines = new Queue<string>();
        m_PlayerController = Player.GetComponent<PlayerController>();
        
        VolumeProfile.TryGet<Vignette>(out Vignette);
        InteractBackground.enabled = false;
    }
    
    /*
     * Shows the Interact TextUI with given string
     */
    public void ShowInteractable(string line_p)
    {
        InteractBackground.enabled = true;
        InteractField.text = line_p;
    }

    /*
     * Disables Interact TextUI and deletes its content
     */
    public void HideInteractable()
    {
        InteractBackground.enabled = false;
        InteractField.text = "";
    }

    /*
     * Invoke this from any StoryElement Object to let its Dialog display on Screen
     */
    public void StartDialog(Dialog dialog_p)
    {
        //Need this check, otherwise sometimes Null reference exception
        if(m_Lines == null)
            m_Lines = new Queue<string>();
        
        //Disable Player Movement while Storytelling, and reset members
        Player.GetComponent<PlayerController>().DisableMovement();
        m_Started = true;
        m_Lines.Clear();
        m_ContinueMessage = "";
        m_StoryTeaser = dialog_p.StoryTeaser;

        //Add Lines to Queue
        foreach (var sentence in dialog_p.Sentences)
        {
            m_Lines.Enqueue(sentence);
        }

        m_ContinueMessage = dialog_p.ContinueMessage;
        DisplayNextSentence();
    }

    /*
     * Goes through lines to display and sends them to Coroutine TypeLine
     */
    private void DisplayNextSentence()
    {
        if (m_Lines.Count == 0)
        {
            //Player ends Dialog with Q
            if(Input.GetKeyDown(KeyCode.Q))
                EndDialog();
            
            return;
        }

        //Get next line to Display, and let Coroutine display it
        string line = m_Lines.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeLine(line));
    }

    /*
     * Types given line, Char by Char into DialogTextField
     */
    IEnumerator TypeLine(string line_p)
    {
        m_NextLine = false;
        
        DialogField.text = "";
        foreach (var line in line_p.ToCharArray())
        {
            DialogField.text += line;
            yield return null;
        }

        if (ContinueField.text == "")
        {
            foreach (var line in m_ContinueMessage.ToCharArray())
            {
                ContinueField.text += line;
                yield return null;
            }
        }
        
        m_NextLine = true;
    }
    
    /*
     * Enables PlayerMovement, deletes all Texts in TextFields
     */
    private void EndDialog()
    {
        DialogField.text = "";
        ContinueField.text = "";
        m_PlayerController.EnableMovement();
        m_Started = false;
        
        if (m_StoryTeaser)
        {
            m_StoryTeaser = false;
            FindObjectOfType<StoryTeaser>().NextScene();
        }
    }

    /*
     * Activate Vignette Effect
     * If last Line reached and player presses Q, deactivate Vignette
     */
    private void Update()
    {
        if (!m_Started)
        {
            Vignette.intensity.value = Mathf.Lerp(Vignette.intensity.value, PalaceScene ? 0.4f : 0f, Time.deltaTime * 5f);
            return;
        }
        
        if (m_NextLine && Input.GetKeyDown(KeyCode.Q))
        {
            DisplayNextSentence();
        }
        Vignette.intensity.value = Mathf.Lerp(Vignette.intensity.value, PalaceScene ? .47f : .425f, Time.deltaTime * 5f);
    }
}
