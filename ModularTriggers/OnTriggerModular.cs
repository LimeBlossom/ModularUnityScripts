using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerModular: MonoBehaviour
{
    [SerializeField] private bool onEnter = true;
    [SerializeField] private bool onStay;
    [SerializeField] private bool onExit;
    [SerializeField] private MonoBehaviour[] setGameObject;
    [SerializeField] private UnityEvent events;
    [SerializeField] private MonoBehaviour[] actions;
    [SerializeField] private string[] tagsThatCollide;
    [SerializeField] private string[] namesThatCollide;
    [SerializeField] private string[] tagsToIgnore;

    [SerializeField] private bool debug;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(onEnter)
        {
            if(debug)
            {
                Debug.Log(collision.gameObject.name);
            }
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
                Activate(collision.gameObject);
            }
            if (debug)
            {
                Debug.Log("canActivate: " + canActivate);
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (onEnter)
        {
            if (debug)
            {
                Debug.Log(collision.gameObject.name);
            }
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
                Activate(collision.gameObject);
            }
            if (debug)
            {
                Debug.Log("canActivate: " + canActivate);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
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
                Activate(collision.gameObject);
            }
        }
    }

    private void OnTriggerStay(Collider collision)
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
                Activate(collision.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (onExit)
        {
            if (debug)
            {
                Debug.Log(collision.gameObject.name);
            }
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
                Activate(collision.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (onExit)
        {
            if (debug)
            {
                Debug.Log(collision.gameObject.name);
            }
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
                Activate(collision.gameObject);
            }
        }
    }

    private void Activate(GameObject go)
    {
        if (setGameObject != null && setGameObject.Length > 0)
        {
            Debug.Log("Sending game object");
            foreach (ISettableGameObject toPass in setGameObject)
            {
                toPass.SetGameObject(go);
            }
        }
        events.Invoke();
        if (actions != null && actions.Length > 0)
        {
            foreach (IActivatable action in actions)
            {
                action.Activate();
            }
        }
    }
}
