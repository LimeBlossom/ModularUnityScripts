using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelDataVar", menuName = "CustomSO/Types/LevelDataVariable")]
public class LevelDataVariable : ScriptableObject
{
    public string levelName;
    public int score;
    public bool beaten;
    public bool revealAnimationPlayed;
    public StringReference mapData;
}
