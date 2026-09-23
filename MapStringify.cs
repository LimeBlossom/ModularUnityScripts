using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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

    [SerializeField] private bool debug;

    public bool loadedMap = false;

    public GameObject GetMap()
    {
        return map;
    }

    public LevelDataVariable getLevelData()
    {
        return levelSO != null ? levelSO.value : null;
    }

    // Map text can come straight from a player's clipboard, and errors are forwarded
    // to telemetry, so a bad chunk is logged by length and hash, never verbatim.
    private static string Describe(string text)
    {
        uint hash = 2166136261;
        foreach (char c in text)
        {
            hash ^= c;
            hash *= 16777619;
        }
        return $"{text.Length} chars, hash {hash:x8}";
    }

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
        if(mapSO != null && mapSO.value != "")
        {
            InstantiateObjectsFromSO();
            return;
        }
        if (levelSO != null && levelSO.value.mapData != null && levelSO.value.mapData.value != "")
        {
            InstantiateObjectsFromLevelData();
            return;
        }
        if(mapFile.text != "")
        {
            InstantiateObjectsFromFile();
            return;
        }
        Debug.LogError("MapStringify:InstantiateObjects could not load map from any data type.");
    }

    public void InstantiateObjectsFromLevelData()
    {
        InstantiateObjectsFromString(levelSO.value.mapData.value);
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
                }
            }
            catch
            {
                Debug.LogError("Was not able to spawn map chunk: " + Describe(chunk));
            }
        }
        loadedMap = true;
}

    public string StringifyObjects(bool snapped = false)
    {
        return StringifyObjects(map, snapped);
    }

    public string StringifyObjects(GameObject toStringify, bool snapped = false)
    {
        string toWrite = "";
        foreach (Transform child in toStringify.transform)
        {
            toWrite += GetPositionString(child, snapped);
            toWrite += GetRotationString(child, snapped);
            toWrite += ParseObjectName(child.name);
            toWrite += "/";
        }
        if (debug)
        {
            print($"{toWrite}");
        }
        return toWrite;
    }

    private string GetPositionString(Transform t, bool snapped = false)
    {
        Vector3 pos = t.position;
        if(snapped)
        {
            pos = GridMovementController.SnappedPosition(pos);
        }

        string toWrite = "";
        toWrite += Mathf.Abs(pos.x).ToString("F1");
        toWrite += pos.x >= 0 ? "p" : "n";
        toWrite += Mathf.Abs(pos.y).ToString("F1");
        toWrite += pos.y >= 0 ? "p" : "n";
        toWrite += Mathf.Abs(pos.z).ToString("F1");
        toWrite += pos.z >= 0 ? "p" : "n";
        toWrite.Replace(',', '.');

        return toWrite;
    }

    private string GetRotationString(Transform t, bool snapped = false)
    {
        Vector3 eulers = t.eulerAngles;
        if(snapped)
        {
            eulers = GridMovementController.SnappedRotation(eulers);
        }

        string toWrite = "";
        toWrite += Mathf.RoundToInt(Mathf.Abs(eulers.x)).ToString("D3");
        toWrite += eulers.x >= 0 ? "p" : "n";
        toWrite += Mathf.RoundToInt(Mathf.Abs(eulers.y)).ToString("D3");
        toWrite += eulers.y >= 0 ? "p" : "n";
        toWrite += Mathf.RoundToInt(Mathf.Abs(eulers.z)).ToString("D3");
        toWrite += eulers.z >= 0 ? "p" : "n";

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
        if (mapChunk.Length < 25)
        {
            return null;
        }

        Vector3 spawnPosition;
        Vector3 spawnRotation;
        int i = 0;

        char[] separators = new char[] { 'p', 'n' };
        double parsed;

        string[] substring = mapChunk[i..].Split(separators, 2);
        double.TryParse(substring[0], NumberStyles.Any, CultureInfo.InvariantCulture, out parsed);
        spawnPosition.x = (float)parsed;
        spawnPosition.x *= (mapChunk.Substring(i + substring[0].Length, 1) == "p") ? 1 : -1;
        i += substring[0].Length + 1;

        substring = mapChunk[i..].Split(separators, 2);
        double.TryParse(substring[0], NumberStyles.Any, CultureInfo.InvariantCulture, out parsed);
        spawnPosition.y = (float)parsed;
        spawnPosition.y *= (mapChunk.Substring(i + substring[0].Length, 1) == "p") ? 1 : -1;
        i += substring[0].Length + 1;

        substring = mapChunk[i..].Split(separators, 2);
        double.TryParse(substring[0], NumberStyles.Any, CultureInfo.InvariantCulture, out parsed);
        spawnPosition.z = (float)parsed;
        spawnPosition.z *= (mapChunk.Substring(i + substring[0].Length, 1) == "p") ? 1 : -1;
        i += substring[0].Length + 1;

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
            if(mapChunk.Substring(i) != "CreativeModeBlock" && mapChunk.Substring(i) != "Falling")
            {
                string prefabName = mapChunk.Substring(i);
                bool looksLikeName = prefabName.Length <= 64 && System.Text.RegularExpressions.Regex.IsMatch(prefabName, @"^[A-Za-z][A-Za-z0-9_ ()\-]*$");
                Debug.LogError("MapStringify::ReadMapChunk could not find a prefab named " + (looksLikeName ? prefabName : Describe(prefabName)));
            }
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
            mapStringify.InstantiateObjectsFromFile();
        }
        if (GUILayout.Button("Save To File"))
        {
            mapStringify.SaveMapFile();
        }
    }
}
#endif
