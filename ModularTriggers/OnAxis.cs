using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAxis : MonoBehaviour
{
    [SerializeField] private string[] axis;
    [SerializeField] private float deadZone = .001f;

    [SerializeField] private MonoBehaviour[] positiveActions;
    [SerializeField] private MonoBehaviour[] negativeActions;

    [SerializeField] private bool onChangeFromZero = false;
    [SerializeField] private bool onChangeToZero = false;

    [SerializeField] private bool debug;

    private int lastValue = 0;

    // Update is called once per frame
    void Update()
    {
        bool activatePositive = false;
        bool activateNegative = false;
        foreach (string axis in axis)
        {
            float value = Input.GetAxis(axis);

            if (debug)
            {
                Debug.Log(axis + ": " + value);
            }

            // If axis is out of deadzone
            if (Mathf.Abs(value) > deadZone)
            {
                if (value > 0)
                {
                    if(onChangeFromZero && changedFromZero(value))
                    {
                        activatePositive = true;
                    }
                    else if(onChangeToZero && lastValue > 0 && changedToZero(value))
                    {
                        activatePositive = true;
                    }
                    else
                    {
                        activatePositive = true;
                    }

                    lastValue = 1;
                }
                else if (value < 0)
                {
                    if (onChangeFromZero && changedFromZero(value))
                    {
                        activateNegative = true;
                    }
                    else if (onChangeToZero && lastValue < 0 && changedToZero(value))
                    {
                        activateNegative = true;
                    }
                    else
                    {
                        activateNegative = true;
                    }
                    lastValue = -1;
                }
            }
            else
            {
                lastValue = 0;
            }
        }
        if(activatePositive)
        {
            Activate(positiveActions);
        }
        else if (activateNegative)
        {
            Activate(negativeActions);
        }
    }

    private bool changedFromZero(float newValue)
    {
        if(lastValue == 0 && newValue != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool changedToZero(float newValue)
    {
        if (lastValue != 0 && newValue == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Activate(MonoBehaviour[] actions)
    {
        if (actions != null && actions.Length > 0)
        {
            foreach (IActivatable action in actions)
            {
                action.Activate();
            }
        }
    }
}
