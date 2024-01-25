using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetFrameRate : MonoBehaviour
{
    [SerializeField] private int target;
    [SerializeField] private float currentFPS;
    [SerializeField] private bool displayFPS;

    [SerializeField] private Canvas displayCanvas;
    [SerializeField] private Text fpsText;

    int frameCount = 0;
    float dt = 0.0f;
    float fps = 0.0f;
    float updateRate = 4.0f;  // 4 updates per sec.

    void Awake()
    {
        Application.targetFrameRate = target;
        // Turn off v-sync
        QualitySettings.vSyncCount = 0;
    }

    private void Update()
    {
        frameCount++;
        dt += Time.deltaTime;
        if (dt > 1.0f / updateRate)
        {
            fps = frameCount / dt;
            frameCount = 0;
            dt -= 1.0f / updateRate;
        }

        currentFPS = fps * Time.timeScale;

        if (displayFPS)
        {
            if(displayCanvas == null)
            {
                displayCanvas = new GameObject("FPSDisplay").AddComponent<Canvas>();
            }
            if(fpsText == null)
            {
                fpsText = new GameObject("FPSText").AddComponent<Text>();
                fpsText.transform.SetParent(displayCanvas.transform);
            }
            fpsText.text = currentFPS.ToString();
        }
    }
}
