using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScore : MonoBehaviour
{
    private MessageHandler messageHandler;

    public bool changeOnStart = true;

    public int scoreChange = 0;

    private void Start()
    {
        messageHandler = FindObjectOfType<MessageHandler>();
        if(changeOnStart)
        {
            IncreaseScore(scoreChange);
        }
    }

    public void IncreaseScore(int value)
    {
        messageHandler.SendMessage("changeScore", value);
    }

    public void IncreaseScore()
    {
        messageHandler.SendMessage("changeScore", scoreChange);
    }
}
