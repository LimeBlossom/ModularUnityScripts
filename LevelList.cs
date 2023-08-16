using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "CustomSO/LevelList")]
public class LevelList : ScriptableObject
{
    [SerializeField] private string mainMenu;
    [SerializeField] private string[] tutorials;
    [SerializeField] private string[] mainLevels;
    [SerializeField] private string[] customLevels;

    [SerializeField] private int tutorialLevelsFinished; /* A value between 0 and tutorials.Length */

    public int GetTutorialCount()
    {
        return tutorials.Length;
    }
    public int GetMainLevelsCount()
    {
        return mainLevels.Length;
    }
    public int GetCustomLevelsCount()
    {
        return customLevels.Length;
    }

    /* Used only when the player wants to replay tutorials */
    public Scene GetTutorial(int number)
    {
        if(number <= tutorials.Length - 1 && tutorialLevelsFinished > number)
        {
            return SceneManager.GetSceneByName(tutorials[number]);
        }
        else
        {
            Debug.LogError("LevelList::GetTutorial " + number + " is out of range.");
            return SceneManager.GetSceneByName(mainMenu);
        }
    }

    public string[] GetMainLevels()
    {
        return mainLevels;
    }

    public Scene GetMainLevel(int number)
    {
        if (number <= mainLevels.Length - 1)
        {
            return SceneManager.GetSceneByName(mainLevels[number]);
        }
        else
        {
            Debug.LogError("LevelList::GetMainLevel " + number + " is out of range.");
            return SceneManager.GetSceneByName(mainMenu);
        }
    }

    public Scene GetCustomLevel(int number)
    {
        if (number <= customLevels.Length - 1)
        {
            return SceneManager.GetSceneByName(customLevels[number]);
        }
        else
        {
            Debug.LogError("LevelList::GetCustomLevel " + number + " is out of range.");
            return SceneManager.GetSceneByName(mainMenu);
        }
    }

    public void ReplaceCustomLevel(int number, string name)
    {
        customLevels[number] = name;
    }

    public Scene GetNextScene(Scene curScene)
    {
        /* This is only if we're replaying the tutorials */
        for (int i = 0; i < tutorials.Length; i++)
        {
            if (SceneManager.GetSceneByName(tutorials[i]) == curScene)
            {
                if (i + 1 < tutorials.Length)
                {
                    return SceneManager.GetSceneByName(tutorials[i + 1]);
                }
                else
                {
                    /* Return to the main menu if we've finished replaying all of the tutorials */
                    return SceneManager.GetSceneByName(mainMenu);
                }
            }
        }
        for (int i = 0; i < mainLevels.Length; i++)
        {
            if (SceneManager.GetSceneByName(mainLevels[i]) == curScene)
            {
                if(i + 1 < mainLevels.Length)
                {
                    return SceneManager.GetSceneByName(mainLevels[i + 1]);
                }
                else
                {
                    /* We've run out of main levels, return to main menu */
                    return SceneManager.GetSceneByName(mainMenu);
                }
            }
        }
        for(int i = 0; i < customLevels.Length; i++)
        {
            if(SceneManager.GetSceneByName(customLevels[i]) == curScene)
            {
                /* TODO: If the player finished a custom level they should return to the custom level selection screen */
                return SceneManager.GetSceneByName(mainMenu);
            }
        }

        /* Base case return to main menu */
        return SceneManager.GetSceneByName(mainMenu);
    }
}
