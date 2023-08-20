using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebLoader : MonoBehaviour, IActivatable
{
    [SerializeField] private string URL;
    [SerializeField] private bool gameOverlay = false;
    
    public void Activate()
    {
        if(gameOverlay)
        {
            //Steamworks.SteamFriends.ActivateGameOverlayToWebPage(URL);
        }
        else
        {
            Application.OpenURL(URL);
        }
    }
}
