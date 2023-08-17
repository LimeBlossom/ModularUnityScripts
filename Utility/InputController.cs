using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private List<string> axi;
    [SerializeField] private List<float> axiVal;
    [SerializeField] private List<float> axiValOld;
    [SerializeField] private List<bool> axiResetUp;
    [SerializeField] private List<bool> axiResetDown;

    void Start()
    {
        axi = new List<string>();
        axiVal = new List<float>();
        axiValOld = new List<float>();
        axiResetUp = new List<bool>();
        axiResetDown = new List<bool>();
    }

    void Update()
    {
        for (int i = 0; i < axi.Count; i++)
        {
            axiValOld[i] = axiVal[i];
            axiVal[i] = Input.GetAxisRaw(axi[i]);
            if (axiVal[i] < 0.4f && axiResetUp[i] == false)
            {
                axiResetUp[i] = true;
            }
            if (axiVal[i] > -0.4f)
            {
                axiResetDown[i] = true;
            }
        }
    }

    // Returns true on the frame that the axis hits -1 but will not return true again until the axis goes up and back down again
    public bool GetOnAxisDown(string axis)
    {
        if (axi.Contains(axis))
        {
            int i = axi.FindIndex(x => x == axis);
            if (axiVal[i] < axiValOld[i] && axiVal[i] < -0.6f && axiResetDown[i] == true)
            {
                axiResetDown[i] = false;
                return true;
            }
        }
        else
        {
            axi.Add(axis);
            axiVal.Add(0);
            axiValOld.Add(0);
            axiResetUp.Add(true);
            axiResetDown.Add(true);
        }
        return false;
    }

    // Returns true on the frame that the axis hits 1 but will not return true again until the axis goes down and back up again
    public bool GetOnAxisUp(string axis)
    {
        if (axi.Contains(axis))
        {
            int i = axi.FindIndex(x => x == axis);
            if (axiVal[i] > axiValOld[i] && axiVal[i] > 0.6f && axiResetUp[i] == true)
            {
                axiResetUp[i] = false;
                return true;
            }
        }
        else
        {
            axi.Add(axis);
            axiVal.Add(0);
            axiValOld.Add(0);
            axiResetUp.Add(true);
            axiResetDown.Add(true);
        }
        return false;
    }

    // Returns true while the axis is held at -1
    public bool GetAxisDown(string axis)
    {
        if (axi.Contains(axis))
        {
            int i = axi.FindIndex(x => x == axis);
            if (axiVal[i] == -1)
            {
                return true;
            }
        }
        else
        {
            axi.Add(axis);
            axiVal.Add(0);
            axiValOld.Add(0);
        }
        return false;
    }

    // Returns true while the axis is held at 1
    public bool GetAxisUp(string axis)
    {
        if (axi.Contains(axis))
        {
            int i = axi.FindIndex(x => x == axis);
            if (axiVal[i] == 1)
            {
                return true;
            }
        }
        else
        {
            axi.Add(axis);
            axiVal.Add(0);
            axiValOld.Add(0);
        }
        return false;
    }
}
