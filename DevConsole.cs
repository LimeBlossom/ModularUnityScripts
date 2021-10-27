using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DevConsole : MonoBehaviour
{
    public Sprite backgroundSprite;

    [SerializeField] private string consoleLog;

    private GameObject canvasObj;
    private Canvas canvas;
    private InputField inputField;
    private Text textBox;
    private GameObject bgImage;
    private DefaultControls.Resources resources;

    [SerializeField] private string myLog;
    private Queue myLogQueue = new Queue();    

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.BackQuote))
        {
            if(canvasObj == null)
            {
                ShowConsole();
            }
            else
            {
                HideConsole();
            }
        }
        if(textBox != null)
        {
            textBox.text = myLog;
        }
    }

    private void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    private void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        myLog = logString;
        string newString = "\n [" + type + "] : " + myLog;
        myLogQueue.Enqueue(newString);
        if (type == LogType.Exception)
        {
            newString = "\n" + stackTrace;
            myLogQueue.Enqueue(newString);
        }
        myLog = string.Empty;
        foreach (string mylog in myLogQueue)
        {
            myLog += mylog;
        }
    }

    private void HideConsole()
    {
        Destroy(canvasObj);
    }

    private void ShowConsole()
    {
        if(canvasObj != null)
        {
            Destroy(canvasObj);
        }

        canvasObj = new GameObject();
        canvasObj.name = "ConsoleCanvas";
        canvasObj.AddComponent<Canvas>();
        canvasObj.transform.SetParent(gameObject.transform);
        canvas = canvasObj.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasObj.AddComponent<CanvasScaler>();
        canvasObj.AddComponent<GraphicRaycaster>();

        // BG Image
        RectTransform rectTransform;
        resources = new DefaultControls.Resources();
        resources.background = backgroundSprite;
        resources.standard = backgroundSprite;
        bgImage = DefaultControls.CreateImage(resources);
        bgImage.transform.SetParent(canvas.transform);
        bgImage.GetComponent<Image>().color = new Color(.25f, .25f, .25f, .25f);

        rectTransform = bgImage.GetComponent<RectTransform>();
        rectTransform.anchorMax = new Vector2(1, 1);
        rectTransform.anchorMin = new Vector2(0, .75f);
        rectTransform.pivot = new Vector2(0.5f, -1);

        rectTransform.localPosition = new Vector3(0, 0, 0);
        rectTransform.sizeDelta = new Vector2(0, 0);

        // Text
        GameObject myText;
        myText = new GameObject();
        myText.transform.parent = canvasObj.transform;
        myText.name = "ConsoleText";

        Text text;
        text = myText.AddComponent<Text>();
        text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        text.text = myLog;
        text.color = Color.black;
        text.fontSize = 20;
        text.alignment = TextAnchor.LowerLeft;

        // Text position
        rectTransform = text.GetComponent<RectTransform>();
        rectTransform.anchorMax = new Vector2(1, 1);
        rectTransform.anchorMin = new Vector2(0, .75f);
        rectTransform.pivot = new Vector2(0.5f, -1);

        rectTransform.localPosition = new Vector3(0, 0, 0);
        rectTransform.sizeDelta = new Vector2(0, 0);
    }
}
