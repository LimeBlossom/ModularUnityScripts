using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlaySound : MonoBehaviour, IActivatable
{
    [SerializeField] private AudioClip[] sounds;
    [SerializeField] private AudioMixerGroup mixer;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private bool playSingleRandom = false;

    public void Activate()
    {
        if(audioManager == null)
        {
            audioManager = FindObjectOfType<AudioManager>();
        }

        if(playSingleRandom)
        {
            audioManager.PlayClip(sounds[Random.Range(0, sounds.Length)], transform.position, mixer);
        }
        else
        {
            foreach (AudioClip sound in sounds)
            {
                audioManager.PlayClip(sound, transform.position, mixer);
            }
        }
    }
}
