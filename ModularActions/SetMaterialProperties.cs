using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMaterialProperties : MonoBehaviour, IActivatable, ISettableColor
{
    [SerializeField] private Material material;
    [SerializeField] private Color color;

    public void Activate()
    {
        Debug.Log($"SetMaterialProperties {material.name}");
        material.SetColor("_Color", color);
    }

    public void SetColor(Color setTo)
    {
        color = setTo;
    }
}
