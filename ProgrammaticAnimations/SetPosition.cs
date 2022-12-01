using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPosition : MonoBehaviour, IActivatable
{
    [SerializeField] private GameObject toMove;
    [SerializeField] private Vector3 positionToMoveTo;
    [SerializeField] private bool localPosition = false;
    [SerializeField] private bool setX = true;
    [SerializeField] private bool setY = true;
    [SerializeField] private bool setZ = true;

    public void Activate()
    {
        if (localPosition)
        {
            toMove.transform.localPosition = new Vector3(
                setX ? positionToMoveTo.x : toMove.transform.localPosition.x,
                setY ? positionToMoveTo.y : toMove.transform.localPosition.y,
                setZ ? positionToMoveTo.z : toMove.transform.localPosition.z);
        }
        else
        {
            toMove.transform.position = new Vector3(
                setX ? positionToMoveTo.x : toMove.transform.position.x,
                setY ? positionToMoveTo.y : toMove.transform.position.y,
                setZ ? positionToMoveTo.z : toMove.transform.position.z);
        }
    }

    private void Start()
    {
        if (toMove == null)
        {
            toMove = gameObject;
        }
    }
}
