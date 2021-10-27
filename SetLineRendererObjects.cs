using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLineRendererObjects : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private GameObject[] targets;
    [SerializeField] private string[] targetTags;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer.SetPositions(GetTargetPositions());
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPositions(GetTargetPositions());
    }

    private Vector3[] GetTargetPositions()
    {
        List<Vector3> positions = new List<Vector3>();
        foreach(GameObject target in targets)
        {
            positions.Add(target.transform.position);
        }
        foreach(string tag in targetTags)
        {
            foreach(GameObject target in GameObject.FindGameObjectsWithTag(tag))
            {
                positions.Add(target.transform.position);
            }
        }
        return positions.ToArray();
    }
}
