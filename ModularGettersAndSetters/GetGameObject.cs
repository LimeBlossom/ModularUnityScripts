using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetGameObject : MonoBehaviour, IActivatable
{
    [SerializeField] private MonoBehaviour getObjectFrom;
    [SerializeField] private MonoBehaviour[] gameObjectSettables;

    public void Activate()
    {
        var gettableClass = getObjectFrom as IGettableGameObject;
        GameObject toPass = gettableClass.GetGameObject();
        foreach(MonoBehaviour behaviour in gameObjectSettables)
        {
            var settable = behaviour as ISettableGameObject;
            if(settable != null)
            {
                settable.SetGameObject(toPass);
            }
        }
    }
}
