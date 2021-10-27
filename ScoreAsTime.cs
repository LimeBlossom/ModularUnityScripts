using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreAsTime : MonoBehaviour
{
    public GameObject stopIfObjectDoesntExist;
    public float scoreMultiplier = 1;

    public Text toChange;
    public string leftText;
    public string rightText;

    void Update()
    {
        if(stopIfObjectDoesntExist != null)
        {
            toChange.text = leftText + Mathf.Floor(Time.timeSinceLevelLoad * scoreMultiplier) + rightText;
        }
    }
}
