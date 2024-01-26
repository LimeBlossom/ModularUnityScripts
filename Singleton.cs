using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour, IActivatable
{
    [SerializeField] private bool dontDestroyOnLoad = true;
    [SerializeField] private bool destroyNewest = true;
    [SerializeField] private bool matchTags = true;
    [SerializeField] private bool onStart = true;
    
    public string id;
    public float timeAtAwake;

    private void Awake()
    {
        timeAtAwake = Time.time;
    }

    void Start()
    {
        if(id == "")
        {
            id = gameObject.name;
        }

        if(dontDestroyOnLoad)
        {
            DontDestroyOnLoad(gameObject);
        }

        if(onStart)
        {
            Activate();
        }
    }

    public void Activate()
    {
        Singleton[] singletons = FindObjectsOfType<Singleton>();
        if (singletons.Length > 1)
        {
            for (int i = 0; i < singletons.Length; i++)
            {
                if(matchTags && gameObject.tag != singletons[i].tag)
                {
                    continue;
                }
                if (singletons[i].id == id && singletons[i].gameObject != gameObject)
                {
                    if (destroyNewest && timeAtAwake > singletons[i].timeAtAwake)
                    {
                        Destroy(gameObject);
                        return;
                    }
                    else
                    {
                        Destroy(singletons[i].gameObject);
                        return;
                    }
                }
            }
        }
    }
}
