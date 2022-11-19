using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelTrailEffect : MonoBehaviour
{
    [SerializeField] private GameObject toGiveTrail;
    [SerializeField] private Material[] materials;
    [SerializeField] private float trailDistance;
    [SerializeField] private float trailLifeTime;

    [SerializeField] private bool trailChildren = false;
    [SerializeField] private bool randomizeMaterial = true;

    private Vector3 oldPos;

    private void Start()
    {
        oldPos = toGiveTrail.transform.position;
    }

    void Update()
    {
        if(Vector3.Distance(toGiveTrail.transform.position, oldPos) > trailDistance)
        {
            Material material = materials[0];
            if (randomizeMaterial)
            {
                material = materials[Random.Range(0, materials.Length)];
            }
            
            if(trailChildren)
            {
                foreach (MeshRenderer meshRenderer in FindAllMeshesInChildren(toGiveTrail))
                {
                    MeshFilter meshFilter = meshRenderer.GetComponent<MeshFilter>();
                    if (!meshFilter)
                    {
                        continue;
                    }
                    GameObject newGO = new GameObject(toGiveTrail.name + "TrailEffect");
                    newGO.transform.position = meshRenderer.transform.position - toGiveTrail.transform.position + oldPos;//oldPos;
                    newGO.transform.rotation = meshRenderer.transform.rotation;
                    newGO.transform.localScale = meshRenderer.transform.lossyScale;
                    MeshFilter newMeshFilter = newGO.AddComponent<MeshFilter>();
                    newMeshFilter.mesh = meshFilter.mesh;
                    MeshRenderer newMeshRenderer = newGO.AddComponent<MeshRenderer>();
                    newMeshRenderer.material = material;
                    Die die = newGO.AddComponent<Die>();
                    die.m_life = new Die.Life();
                    die.m_life.min = trailLifeTime;
                    die.m_life.max = trailLifeTime;
                }
            }
            else
            {
                GameObject newGO = new GameObject(toGiveTrail.name + "TrailEffect");
                newGO.transform.position = oldPos;
                newGO.transform.rotation = toGiveTrail.transform.rotation;
                newGO.transform.localScale = toGiveTrail.transform.lossyScale;
            }
        }
        oldPos = toGiveTrail.transform.position;
    }

    private List<MeshRenderer> FindAllMeshesInChildren(GameObject parent)
    {
        List<MeshRenderer> meshRenderers = new List<MeshRenderer>();
        foreach(MeshRenderer meshRenderer in parent.GetComponentsInChildren<MeshRenderer>())
        {
            meshRenderers.Add(meshRenderer);
        }
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            foreach(MeshRenderer childRenderer in FindAllMeshesInChildren(parent.transform.GetChild(i).gameObject))
            {
                meshRenderers.Add(childRenderer);
            }
        }
        return meshRenderers;
    }
}
