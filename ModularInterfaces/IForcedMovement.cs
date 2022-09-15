using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IForcedMovement
{
    void ForcedMoveTo(Vector3 destination);
    void ForcedStopMoving();
}
