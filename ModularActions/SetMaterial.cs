using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetMaterial : MonoBehaviour, IActivatable
{
    [SerializeField] private Image image;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material material;

    public void Activate()
    {
        //print("Activated");
        if(image != null)
        {
            image.material = material;
        }
        if(meshRenderer != null)
        {
            //print("Setting material");
            meshRenderer.material = material;
        }
    }
}
