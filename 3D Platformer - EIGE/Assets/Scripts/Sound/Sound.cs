using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/*
 * Basic Soundfile
 * Determine Name, Audioclip and other settings in Inspector
 */
[System.Serializable]
public class Sound
{
    public string Name;
    public AudioClip AudioClip;

    [Range(0f, 1f)]
    public float Volume;
    [Range(0.1f, 3f)]
    public float Pitch;
    
    public bool Loop;

    [HideInInspector] public AudioSource Source;
}
