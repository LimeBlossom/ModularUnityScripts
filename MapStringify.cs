using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class MapStringify : MonoBehaviour
{
    [SerializeField] private GameObject map;
    [SerializeField] private TextAsset mapFile;
    [SerializeField] private StringVariable mapSO;
    [SerializeField] private LevelDataRef levelSO;
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private bool centerMap = false;

    private void Start()
    {
        UnitTests();
    }

    public void SaveMapFile()
    {
#if UNITY_EDITOR
        Debug.Log(StringifyObjects());
        File.WriteAllText(AssetDatabase.GetAssetPath(mapFile), StringifyObjects());
        EditorUtility.SetDirty(mapFile);
#endif
    }

    public void SaveToStringReference()
    {
        mapSO.value = StringifyObjects();
    }

    public void InstantiateObjects()
    {
        if (mapFile != null)
        {
            InstantiateObjectsFromString(mapFile.text);
        }
        else if (mapSO != null)
        {
            InstantiateObjectsFromString(mapSO.value);
        }
        else if (levelSO != null)
        {
            InstantiateObjectsFromString(levelSO.value.mapData.value);
        }
    }

    public void InstantiateObjectsFromFile()
    {
        InstantiateObjectsFromString(mapFile.text);
    }

    public void InstantiateObjectsFromSO()
    {
        InstantiateObjectsFromString(mapSO.value);
    }

    public void InstantiateObjectsFromString(string mapText)
    {
        if(mapText != "")
        {
            while(map.transform.childCount > 0)
            {
                DestroyImmediate(map.transform.GetChild(0).gameObject);
            }
        }
        string[] mapChunks = mapText.Split('/');
        foreach (string chunk in mapChunks)
        {
            try
            {
                GameObject toSpawn = ReadMapChunk(chunk);
                if(toSpawn != null)
                {
                    GameObject spawned = Instantiate(toSpawn);
                    spawned.transform.SetParent(map.transform);

                    if(centerMap) // TODO: This function is not complete.
                        CenterMapTransforms(map.transform);
                }
            }
            catch
            {
                Debug.LogError("Was not able to spawn map chunk.");
                Debug.LogError(chunk);
            }
        }
    }

    private void CenterMapTransforms(Transform levelMap) // TODO: This function is not complete.
    {
        Transform[] transforms = levelMap.GetComponentsInChildren<Transform>();
        float totalX = 0;
        float totalZ = 0;
        foreach (Transform child in levelMap.GetComponentsInChildren<Transform>())
        {
            totalX += child.position.x;
            totalZ += child.position.z;
        }
        float avgX = totalX / transforms.Length;
        float avgZ = totalZ / transforms.Length;
        Vector3 centerPoint = new Vector3(avgX, 0, avgZ);

        levelMap.transform.position = new Vector3(centerPoint.x, 0, centerPoint.z);
    }

    public string StringifyObjects()
    {
        return StringifyObjects(map);
    }

    public string StringifyObjects(GameObject toStringify)
    {
        string toWrite = "";
        foreach (Transform child in toStringify.transform)
        {
            toWrite += Mathf.RoundToInt(Mathf.Abs(child.transform.position.x)).ToString("D3");
            toWrite += child.transform.position.x >= 0 ? "p" : "n";
            toWrite += Mathf.RoundToInt(Mathf.Abs(child.transform.position.y)).ToString("D3");
            toWrite += child.transform.position.y >= 0 ? "p" : "n";
            toWrite += Mathf.RoundToInt(Mathf.Abs(child.transform.position.z)).ToString("D3");
            toWrite += child.transform.position.z >= 0 ? "p" : "n";
            toWrite += Mathf.RoundToInt(Mathf.Abs(child.transform.eulerAngles.x)).ToString("D3");
            toWrite += child.transform.eulerAngles.x >= 0 ? "p" : "n";
            toWrite += Mathf.RoundToInt(Mathf.Abs(child.transform.eulerAngles.y)).ToString("D3");
            toWrite += child.transform.eulerAngles.y >= 0 ? "p" : "n";
            toWrite += Mathf.RoundToInt(Mathf.Abs(child.transform.eulerAngles.z)).ToString("D3");
            toWrite += child.transform.eulerAngles.z >= 0 ? "p" : "n";
            toWrite += ParseObjectName(child.name);
            toWrite += "/";
        }

        return toWrite;
    }

    private string ParseObjectName(string name)
    {
        int parenthPos = name.IndexOf("(");
        if(parenthPos >= 0)
        {
            name = name.Substring(0, parenthPos);
            name = name.Replace(" ", "");
        }
        return name;
    }

    private GameObject ReadMapChunk(string mapChunk)
    {
        if(mapChunk.Length < 25)
        {
            return null;
        }

        Vector3 spawnPosition;
        Vector3 spawnRotation;
        int i = 0;

        spawnPosition.x = int.Parse(mapChunk.Substring(i, 3));
        spawnPosition.x *= (mapChunk.Substring(i + 3, 1) == "p") ? 1 : -1;
        i += 4;

        spawnPosition.y = int.Parse(mapChunk.Substring(i, 3));
        spawnPosition.y *= (mapChunk.Substring(i + 3, 1) == "p") ? 1 : -1;
        i += 4;

        spawnPosition.z = int.Parse(mapChunk.Substring(i, 3));
        spawnPosition.z *= (mapChunk.Substring(i + 3, 1) == "p") ? 1 : -1;
        i += 4;

        spawnRotation.x = int.Parse(mapChunk.Substring(i, 3));
        spawnRotation.x *= (mapChunk.Substring(i + 3, 1) == "p") ? 1 : -1;
        i += 4;

        spawnRotation.y = int.Parse(mapChunk.Substring(i, 3));
        spawnRotation.y *= (mapChunk.Substring(i + 3, 1) == "p") ? 1 : -1;
        i += 4;

        spawnRotation.z = int.Parse(mapChunk.Substring(i, 3));
        spawnRotation.z *= (mapChunk.Substring(i + 3, 1) == "p") ? 1 : -1;
        i += 4;

        GameObject toSpawn = FindSpawnableByName(mapChunk.Substring(i));
        //Debug.Log(mapChunk.Substring(i));
        if(toSpawn != null)
        {
            toSpawn.transform.position = spawnPosition;
            toSpawn.transform.rotation = Quaternion.Euler(spawnRotation);
        }
        else
        {
            //Debug.Log("MapStringify::ReadMapChunk could not find a prefab named " + mapChunk.Substring(i));
        }

        return toSpawn;
    } // End ReadMapChunk

    private GameObject FindSpawnableByName(string name)
    {
        foreach(GameObject go in prefabs)
        {
            if(go.name == name)
            {
                return go;
            }
        }

        return null;
    }

    private void UnitTests()
    {
        string objectName = "Player(clone)";
        if(ParseObjectName(objectName) != "Player")
        {
            Debug.LogError("ParseObjectName::UnitTest error: Expected Player but got " + ParseObjectName(objectName));
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(MapStringify))]
public class MapStringifyEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MapStringify mapStringify = (MapStringify)target;
        if (GUILayout.Button("Spawn Level"))
        {
            mapStringify.InstantiateObjects();
        }
        if (GUILayout.Button("Save To File"))
        {
            mapStringify.SaveMapFile();
        }
    }
}
#endif
