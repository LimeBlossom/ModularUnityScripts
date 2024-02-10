using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlaySound : MonoBehaviour, IActivatable
{
    [SerializeField] private AudioClip[] sounds;
    [SerializeField] private float spatialBlend = 1;
    [SerializeField] private AudioMixerGroup mixer;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private bool playSingleRandom = false;
    [SerializeField] private MinMaxFloat pitchRange;
    [SerializeField] private int[] semitones = { 0 };
    [SerializeField] private bool ignoreTimeScale = false;
    [SerializeField] private FloatReference volume;

    public void Activate()
    {
        if (audioManager == null)
        {
            audioManager = FindObjectOfType<AudioManager>();
        }

        if (pitchRange.max == 0)
        {
            pitchRange.min = 1;
            pitchRange.max = 1;
        }

        float pitch = Random.Range(pitchRange.min, pitchRange.max);
        if (!ignoreTimeScale)
        {
            pitch = Time.timeScale < 1 ? pitch * 0.5f : pitch;
        }
        int semitone = Random.Range(0, semitones.Length);
        pitch *= Mathf.Pow(1.059463f, semitones[semitone]);

        if(playSingleRandom)
        {
            audioManager?.PlayClip(sounds[Random.Range(0, sounds.Length)], transform.position, spatialBlend, pitch, volume.value, mixer);
        }
        else
        {
            foreach (AudioClip sound in sounds)
            {
                audioManager?.PlayClip(sound, transform.position, spatialBlend, pitch, volume.value, mixer);
            }
        }
    }
}
