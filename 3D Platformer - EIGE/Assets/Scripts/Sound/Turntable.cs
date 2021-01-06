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

    // Start is called before the first frame update
    void Start()
    {
        PlaySound("MainTheme");
    }
}
