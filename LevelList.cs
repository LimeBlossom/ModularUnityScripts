using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

[CreateAssetMenu(menuName = "CustomSO/LevelList")]
public class LevelList : ScriptableObject
{
    [SerializeField] private SceneReference mainMenu;
    [SerializeField] private SceneReference[] mainLevelScenes;

    [SerializeField] private int tutorialLevelsFinished; /* A value between 0 and tutorials.Length */

    public int GetMainLevelsCount()
    {
        return mainLevelScenes.Length;
    }

    public string[] GetMainLevels()
    {
        string[] levelNames = new string[mainLevelScenes.Length];
        for(int i = 0; i < mainLevelScenes.Length; i++)
        {
            levelNames[i] = mainLevelScenes[i].ScenePath.Split('/').Last().
                Split('.').First();
        }
        return levelNames;
    }

    public string GetMainMenu()
    {
        return mainMenu;
    }

    public string GetNextScenePath(Scene curScene)
    {
        for (int i = 0; i < mainLevelScenes.Length; i++)
        {
            if (mainLevelScenes[i].ScenePath == curScene.path)
            {
                if (i + 1 < mainLevelScenes.Length)
                {
                    return mainLevelScenes[i+1].ScenePath;
                }
                else
                {
                    /* We've run out of main levels, return to main menu */
                    return mainMenu;
                }
            }
        }

        /* Base case return to main menu */
        return mainMenu;
    }
}
