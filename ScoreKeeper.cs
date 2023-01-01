using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    public Text textToUpdate;
    public TextMeshProUGUI[] textMeshPro;

    [SerializeField] private IntReference score;

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
        score.variable.value += value;
        if(textToUpdate)
        {
            textToUpdate.text = "Score: " + score.value.ToString();
        }
        foreach(TextMeshProUGUI textMesh in textMeshPro)
        {
            textMesh.text = "Score: " + score.value.ToString();
        }
    }


}
