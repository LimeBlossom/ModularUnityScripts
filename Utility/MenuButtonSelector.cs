using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuButtonSelector : MonoBehaviour, IActivatable
{

    public Button defaultSelection;
    public Button secondSelection;

    public bool onEnable = false;
    public bool onStart = true;

    private InputController inputController;

    void Start()
    {
        inputController = FindObjectOfType<InputController>();
        if (!onStart)
        {
            return;
        }
        Activate();
    }

    private void OnEnable()
    {
        if (!onEnable)
        {
            return;
        }
        Activate();
    }

    void OnMove()
    {
        if (EventSystem.current.currentSelectedGameObject == null || !EventSystem.current.currentSelectedGameObject.activeInHierarchy)
        {
            print("Error: No button selected. Selecting default.");
            defaultSelection.Select();
            defaultSelection.OnSelect(null);
        }
        else
        {
            EventSystem.current.currentSelectedGameObject.GetComponent<Button>().Select();
            EventSystem.current.currentSelectedGameObject.GetComponent<Button>().OnSelect(null);
        }
    }

    public void Activate()
    {
        Button toSelect = defaultSelection;
        if (!defaultSelection.IsActive() && secondSelection != null)
        {
            toSelect = secondSelection;
        }
        if (EventSystem.current != null && toSelect != EventSystem.current.currentSelectedGameObject)
        {
            // Due to a bug in Unity, sometimes selection will not be visible
            toSelect.Select();
            toSelect.OnSelect(null);
        }
    }
}
