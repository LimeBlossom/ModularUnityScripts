using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    [SerializeField] private bool dontDestroyOnLoad = true;
    public float timeAtAwake;

    private void Awake()
    {
        timeAtAwake = Time.fixedTime;
    }

    void Start()
    {
        if(dontDestroyOnLoad)
        {
            DontDestroyOnLoad(gameObject);
        }
        Singleton[] singletons = FindObjectsOfType<Singleton>();
        if (singletons.Length > 1)
        {
            for(int i = 0; i < singletons.Length; i++)
            {
                if(singletons[i].gameObject.name == gameObject.name && timeAtAwake > singletons[i].timeAtAwake)
                {
                    Destroy(gameObject);
                    return;
                }
            }
        }
    }
}
