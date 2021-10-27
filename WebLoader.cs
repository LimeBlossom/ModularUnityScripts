using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebLoader : MonoBehaviour, IActivatable
{
    [SerializeField] private string URL;
    
    public void Activate()
    {
        Application.OpenURL(URL);
    }
}
