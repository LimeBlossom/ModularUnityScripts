using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum damageTypes
{
    falling,
    bullet,
    cannon,
    bomb,
    sealion,
    penguin,
    polarbear,
    orca
}

public interface IBreakable
{
    bool Break(damageTypes damageType);
    bool CanBreakFrom(damageTypes damageType);
}

public interface IMoveable
{
    bool MoveTo(Vector3 newPosition);
    bool Rotate(Vector3 rotation);
    Vector2 GetDimensions();
    void SnapIntoPlace();
    void Grab();
    void ReleaseGrab();
    bool CanMoveDirection(Vector3 direction);
    void SetGrabber(Grabbing toSet);
}

public interface ITorchable
{
    void Torch();
}

public interface IDrenchable
{
    void Drench();
}

public interface ISteppedOn
{
    void GetSteppedOn(GameObject stepper);
}