using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public GameObject audioPrefab;

    public void PlayClip(AudioClip clip, Vector3 pos, float pitch = 1, float volume = 1, float spatialBlend = 1f, AudioMixerGroup mixer = null)
    {
        GameObject sourceObj = Instantiate(audioPrefab, pos, Quaternion.identity);
        sourceObj.GetComponent<AudioSource>().outputAudioMixerGroup = mixer;
        sourceObj.GetComponent<AudioSource>().pitch = Time.timeScale < 1 ? pitch * 0.5f : pitch;
        sourceObj.GetComponent<AudioSource>().volume = volume;
        sourceObj.GetComponent<AudioSource>().spatialBlend = spatialBlend;
        sourceObj.GetComponent<AudioSource>().PlayOneShot(clip);
    }
}
