using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnCollisionTrigger : MonoBehaviour
{
    [SerializeField] private bool onEnter = true;
    [SerializeField] private bool onStay;
    [SerializeField] private bool onExit;
    [SerializeField] private UnityEvent events;
    [SerializeField] private MonoBehaviour[] actions;
    [SerializeField] private string[] tagsThatCollide;
    [SerializeField] private string[] namesThatCollide;
    [SerializeField] private string[] tagsToIgnore;

    private void OnCollisionEnter(Collision collision)
    {
        if (onEnter)
        {
            bool canActivate = false;
            if (tagsThatCollide.Length == 0 && namesThatCollide.Length == 0)
            {
                canActivate = true;
            }
            foreach (string tag in tagsThatCollide)
            {
                if (collision.gameObject.tag == tag)
                {
                    canActivate = true;
                }
            }
            foreach (string name in namesThatCollide)
            {
                if (collision.gameObject.name.Contains(name))
                {
                    canActivate = true;
                }
            }
            foreach (string tag in tagsToIgnore)
            {
                if (collision.gameObject.tag == tag)
                {
                    canActivate = false;
                }
            }
            if (canActivate)
            {
                Activate();
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (onStay)
        {
            bool canActivate = false;
            if (tagsThatCollide.Length == 0 && namesThatCollide.Length == 0)
            {
                canActivate = true;
            }
            foreach (string tag in tagsThatCollide)
            {
                if (collision.gameObject.tag == tag)
                {
                    canActivate = true;
                }
            }
            foreach (string name in namesThatCollide)
            {
                if (collision.gameObject.name.Contains(name))
                {
                    canActivate = true;
                }
            }
            if (canActivate)
            {
                Activate();
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (onExit)
        {
            bool canActivate = false;
            if (tagsThatCollide.Length == 0 && namesThatCollide.Length == 0)
            {
                canActivate = true;
            }
            foreach (string tag in tagsThatCollide)
            {
                if (collision.gameObject.tag == tag)
                {
                    canActivate = true;
                }
            }
            foreach (string name in namesThatCollide)
            {
                if (collision.gameObject.name.Contains(name))
                {
                    canActivate = true;
                }
            }
            if (canActivate)
            {
                Activate();
            }
        }
    }

    public void Activate()
    {
        events.Invoke();
        if (actions.Length > 0)
        {
            foreach (IActivatable action in actions)
            {
                action.Activate();
            }
        }
    }
}

