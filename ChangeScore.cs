using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScore : MonoBehaviour, IActivatable
{
    private MessageHandler messageHandler;

    public bool changeOnStart = true;

    public int scoreChange = 0;

    private void Start()
    {
        messageHandler = FindObjectOfType<MessageHandler>();
        if(changeOnStart)
        {
            IncreaseScore();
        }
    }

    public void IncreaseScore(int value)
    {
        messageHandler.SendMessage("changeScore", value);
    }

    public void IncreaseScore()
    {
        IncreaseScore(scoreChange);
    }

    public void Activate()
    {
        IncreaseScore();
    }
}
