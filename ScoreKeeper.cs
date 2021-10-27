using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    public Text textToUpdate;

    private int score = 0;

    void OnEnable()
    {
        MessageHandler.OnMessage += RecieveMessage;
    }

    private void OnDisable()
    {
        MessageHandler.OnMessage -= RecieveMessage;
    }

    private void Start()
    {
        IncreaseScore(0);
    }

    public void RecieveMessage(string type, int num)
    {
        if(type == "changeScore")
        {
            IncreaseScore(num);
        }
    }

    public void IncreaseScore(int value)
    {
        score += value;
        textToUpdate.text = "Score: " + score.ToString();
    }


}
