using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class OnButtonHighlighted : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private bool onHighlighted = true;
    [SerializeField] private bool onUnhighlighted = false;
    [SerializeField] private UnityEvent events;

    public void OnSelect(BaseEventData eventData)
    {
        if(onHighlighted)
        {
            Activate();
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {
        if (onUnhighlighted)
        {
            Activate();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(onHighlighted)
        {
            Activate();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(onUnhighlighted)
        {
            Activate();
        }
    }

    private void Activate()
    {
        events.Invoke();
    }
}