using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CreateGrid3D : MonoBehaviour
{
    [SerializeField] private GameObject toSpawn;
    [SerializeField] private int numWidth;
    [SerializeField] private int numHeight;
    [SerializeField] private int numDepth;
    [SerializeField] private Vector3 scalar;
    [SerializeField] private Vector3 startingPosition; // Lower left corner
    [SerializeField] private Transform parentTo;

    private List<GameObject> spawnedList;

    public void RespawnMap()
    {
        for(int i = parentTo.childCount-1; i > 0; i--)
        {
            Destroy(parentTo.GetChild(i).gameObject);
        }
        SpawnGrid();
    }

    public void SpawnGrid()
    {
        spawnedList = new List<GameObject>();
        for (int i = 0; i < numWidth; i++)
        {
            for (int j = 0; j < numHeight; j++)
            {
                for(int k = 0; k < numDepth; k++)
                {
#if UNITY_EDITOR
                    GameObject spawned = PrefabUtility.InstantiatePrefab(toSpawn) as GameObject;
                    spawned.transform.position = new Vector3(startingPosition.x + i * scalar.x, startingPosition.y + j * scalar.y, startingPosition.z + k * scalar.z);
                    if (parentTo)
                    {
                        spawned.transform.SetParent(parentTo);
                    }
                    spawnedList.Add(spawned);
#else
                    GameObject spawned = Instantiate(toSpawn, new Vector3(startingPosition.x + i * scalar.x, startingPosition.y + j * scalar.y, startingPosition.z + k * scalar.z), Quaternion.identity);
                    if (parentTo)
                    {
                        spawned.transform.SetParent(parentTo);
                    }
                    spawnedList.Add(spawned);
#endif
                }
            }
        }
    }

    public void DeleteGrid()
    {
        foreach(GameObject go in spawnedList)
        {
            DestroyImmediate(go);
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(CreateGrid3D))]
public class CreateGrid3DEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CreateGrid3D createGrid3D = (CreateGrid3D)target;
        if (GUILayout.Button("Spawn Grid"))
        {
            createGrid3D.SpawnGrid();
        }
        if (GUILayout.Button("Delete Grid"))
        {
            createGrid3D.DeleteGrid();
        }
    }
}
#endif
