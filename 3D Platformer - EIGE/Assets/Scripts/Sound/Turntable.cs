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
}
