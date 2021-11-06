using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLayerOrderToIntVariable : MonoBehaviour, IActivatable
{
    [SerializeField] private IntVariable variable;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public void Activate()
    {
        spriteRenderer.sortingOrder = variable.value;
    }
}
