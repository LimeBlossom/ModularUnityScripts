using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameQuiter : MonoBehaviour, IActivatable
{
    public void Activate()
    {
        QuitGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
