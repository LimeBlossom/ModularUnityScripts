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
    [SerializeField] private MinMaxFloat pitchRange;
    [SerializeField] private float volume = 1;

    public void Activate()
    {
        if(pitchRange.max == 0)
        {
            pitchRange.min = 1;
            pitchRange.max = 1;
        }

        float pitch = Random.Range(pitchRange.min, pitchRange.max);

        if(audioManager == null)
        {
            audioManager = FindObjectOfType<AudioManager>();
        }

        if(playSingleRandom)
        {
            audioManager.PlayClip(sounds[Random.Range(0, sounds.Length)], transform.position, pitch, volume, mixer);
        }
        else
        {
            foreach (AudioClip sound in sounds)
            {
                if(audioManager)
                {
                    audioManager.PlayClip(sound, transform.position, pitch, volume, mixer);
                }
            }
        }
    }
}
