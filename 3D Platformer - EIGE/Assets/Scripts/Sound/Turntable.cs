using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/*
 * Generic Singleton Sound Manager
 * Store Sounds in SoundArray which is initialized in Inspector
 * PlaySound with Method Invocation from Anywhere
 */
public class Turntable : MonoBehaviour
{
    public Sound[] Sounds;

    private static Turntable m_Instance;
    private float[] m_InitialVolume;
    private float[] m_AdjustedVolume;
    
    /*
     * Get all Sounds registered in the inspector and save them
     */
    private void Awake()
    {
        if (m_Instance == null)
            m_Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);
        
        foreach (Sound sound in Sounds)
        {
            sound.Source = gameObject.AddComponent<AudioSource>();
            sound.Source.clip = sound.AudioClip;
            sound.Source.volume = sound.Volume;
            sound.Source.pitch = sound.Pitch;
            sound.Source.loop = sound.Loop;
        }
        
        m_InitialVolume = new float[Sounds.Length];
        m_AdjustedVolume = new float[Sounds.Length];

        int index = 0;
        foreach (Sound sound in Sounds)
        {
            m_InitialVolume[index] = sound.Source.volume;
            m_AdjustedVolume[index++] = sound.Source.volume;
        }
    }

    /*
     * Play the sound, according to given String
     */
    public void PlaySound(string name_p)
    {
        Sound sound = Array.Find(Sounds, sound_p => sound_p.Name == name_p);
        
        if (sound == null)
        {
            Debug.LogWarning("Soundfile " + name_p + " not found !");
            return;
        }
        
        sound.Source.Play();
    }
    
    /*
     * Stop the sound, according to given String
     */
    public void StopSound(string name_p)
    {
        Sound sound = Array.Find(Sounds, sound_p => sound_p.Name == name_p);
        
        if (sound == null)
        {
            Debug.LogWarning("Soundfile " + name_p + " not found !");
            return;
        }
        
        sound.Source.Stop();
    }
    
    /*
     * Fade volume of first sound to 0 and stop playing
     * Then start second Sound
     */
    public void FadeOutSound(string name_p, string next_p)
    {
        Sound fade = Array.Find(Sounds, sound_p => sound_p.Name == name_p);
        Sound next = Array.Find(Sounds, sound_p => sound_p.Name == next_p);
        
        if (fade == null || next == null)
        {
            Debug.LogWarning("Soundfile " + name_p + " or " + next_p + " not found !");
            return;
        }
        StopAllCoroutines();
        StartCoroutine(FadeOutAndPlay(fade, next));
    }

    /*
     * Reduce volume by .01f every frame, after reaching threshold
     * stop sound and play the next one
     */
    IEnumerator FadeOutAndPlay(Sound fade_p, Sound next_p)
    {
        while (Mathf.Abs(fade_p.Source.volume) > .1f)
        {
            fade_p.Source.volume -= .01f;
            yield return null;
        }
        StopSound(fade_p.Name);
        PlaySound(next_p.Name);
        AdjustVolume(1f);
    }

    /*
     * Activated by the slider of options menu.
     * Get value between 0.1 - 1
     * Set percentage volume based on value
     */
    public void AdjustVolume(float value_p)
    {
        for (int i = 0; i < m_InitialVolume.Length; i++)
        {
            m_AdjustedVolume[i] = m_InitialVolume[i] * value_p;
        }

        int index = 0;
        foreach (Sound sound in Sounds)
        {
            sound.Source.volume = m_AdjustedVolume[index++];
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        PlaySound("MainTheme");
    }

    /*
     * Called after entering Blobbs Domain
     */
    public void PlayBattleTheme()
    {
        StopSound("MainTheme");
        PlaySound("Battle");
        PlaySound("BattleAmbience");
        PlaySound("SlimeSound");
    }
    
    /*
     * Called after Blobbs Death
     */
    public void StopBattleTheme()
    {
        FadeOutSound("Battle", "MainTheme");
        PlaySound("BattleWon");
        StopSound("BattleAmbience");
        StopSound("SlimeSound");
    }
}
