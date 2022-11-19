using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour, IActivatable
{
    public bool randomScene;
    public bool reloadThisScene;
    public int sceneNumber = -1;
    public string sceneName;
    public bool loadOnAwake = false;
    public MinMaxFloat loadAfterTime;

    [SerializeField] private bool loadNextScene;

    private float loadTime = 0;

    private float birthTime;

    public void Activate()
    {
        LoadScene();
    }

    private void Awake()
    {
        birthTime = Time.fixedTime;
        if(loadOnAwake)
        {
            LoadScene();
        }

        loadTime = Random.Range(loadAfterTime.min, loadAfterTime.max);
    }

    private void Update()
    {
        if(loadAfterTime.min > 0)
        {
            if (Time.fixedTime - loadTime > birthTime)
            {
                LoadScene();
            }
        }
    }

    public void LoadScene()
    {
        if(loadNextScene)
        {
            int curIndex = SceneManager.GetActiveScene().buildIndex;
            if(curIndex == SceneManager.sceneCountInBuildSettings - 1)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                SceneManager.LoadScene(curIndex + 1);
            }
        }
        else if(sceneNumber != -1)
        {
            SceneManager.LoadScene(sceneNumber);
        }
        else if(sceneName != "")
        {
            SceneManager.LoadScene(sceneName);
        }
        else if(randomScene)
        {
            SceneManager.LoadScene(Random.Range(0, SceneManager.sceneCount));
        }
        else if(reloadThisScene)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void LoadScene(int value)
    {
        SceneManager.LoadScene(value);
    }

    public void LoadScene(string value)
    {
        SceneManager.LoadScene(value);
    }
}
