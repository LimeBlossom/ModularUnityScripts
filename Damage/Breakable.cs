using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.Audio;

public class Breakable : MonoBehaviour, IBreakable
{
    public GameObject toBreak;
    public GameObject replaceOnBreak;
    [SerializeField] private Transform replacePoint;
    public AudioClip soundEffectOnBreak;
    [SerializeField] private GameObject soundEffectPrefab;
    [SerializeField] private AudioMixerGroup mixer;
    public damageTypes[] brokenBy;
    public float moveBrokenYUp = 0;
    public UnityEvent events;
    [SerializeField] private bool broken;
    [SerializeField] private bool canBreakMultipleTimes = false;

    [SerializeField] private bool debug;

    private void Start()
    {
        if (toBreak == null)
        {
            toBreak = gameObject;
        }
    }

    public void MarkBroken(bool value)
    {
        broken = value;
    }

    public bool Break(damageTypes damageType)
    {
        if (broken && !canBreakMultipleTimes)
        {
            return true;
        }
        if (CanBreakFrom(damageType))
        {
            broken = true;
            if (debug)
            {
                print($"{name} is breaking from {damageType}");
            }
            ActivateEvents();
            if (soundEffectOnBreak)
            {
                FindObjectOfType<AudioManager>().PlayClip(soundEffectOnBreak, transform.position, pitch:1, volume:1, spatialBlend:1, mixer);
            }
            GetComponent<Collider>().enabled = false;
            if(replaceOnBreak)
            {
                if(replacePoint == null)
                {
                    replacePoint = transform;
                }

                GameObject brokenObj = Instantiate(
                    replaceOnBreak, replacePoint.position + Vector3.up * moveBrokenYUp, replacePoint.rotation);

                if (GetComponent<Rigidbody>() && brokenObj.GetComponent<Rigidbody>())
                {
                    brokenObj.GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity;
                }
            }
            // Debug.Log(toBreak.name + " destroyed by " + damageType.ToString());
            //foreach(Transform t in toBreak.GetComponentsInChildren<Transform>())
            //{
            //    t.gameObject.layer = LayerMask.NameToLayer("Rubble");
            //}
            if(toBreak != null)
            {
                Destroy(toBreak);
            }
            return true;
        }
        return false;
    }

    public bool CanBreakFrom(damageTypes damageType)
    {
        foreach(damageTypes dt in brokenBy)
        {
            if(dt == damageType)
            {
                return true;
            }
        }
        return false;
    }

    private void ActivateEvents()
    {
        events.Invoke();
    }
}
