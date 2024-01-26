using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class IfSceneData : MonoBehaviour, IActivatable
{
    public string sceneName = "";
    public int sceneNumber = -1;
    public bool isNot = false;

    [SerializeField] private UnityEvent events;
    [SerializeField] private MonoBehaviour[] actions;

    public void Activate()
    {
        if(sceneName != "")
        {
            if(SceneManager.GetActiveScene().name == sceneName && !isNot)
            {
                ActivateActions();
            }
            else if(SceneManager.GetActiveScene().name != sceneName && isNot)
            {
                ActivateActions();
            }
        }
        else if(sceneNumber > -1)
        {
            if(SceneManager.GetActiveScene().buildIndex == sceneNumber && !isNot)
            {
                ActivateActions();
            }
            else if(SceneManager.GetActiveScene().buildIndex != sceneNumber && isNot)
            {
                ActivateActions();
            }
        }
    }

    private void ActivateActions()
    {
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
