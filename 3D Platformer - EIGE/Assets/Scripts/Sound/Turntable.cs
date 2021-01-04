using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Turntable : MonoBehaviour
{
    public Sound[] Sounds;


    private void Awake()
    {
        foreach (Sound sound in Sounds)
        {
            sound.Source = gameObject.AddComponent<AudioSource>();
            sound.Source.clip = sound.AudioClip;
            sound.Source.volume = sound.Volume;
            sound.Source.pitch = sound.Pitch;
        }
    }

    public void PlaySound(string name_p)
    {
        Sound sound = Array.Find(Sounds, sound_p => sound_p.Name == name_p);
        sound.Source.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
