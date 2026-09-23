using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAction : MonoBehaviour, IActivatable
{
    [SerializeField] private bool randomizeToSpawn = false;
    [SerializeField] private GameObjectReference[] toSpawn;
    [SerializeField] private Transform[] spawnLocation;
    [SerializeField] private Transform spawnRotation;
    [SerializeField] private GameObject parentTo;
    [SerializeField] private bool debug;

    public bool CanSpawn()
    {
        if(toSpawn.Length > 0)
        {
            if(toSpawn[0].value != null)
            { return true; }
        }
        return false;
    }

    public GameObject[] SpawnArray()
    {
        List<GameObject> spawnList = new();
        foreach(GameObjectReference goRef in toSpawn)
        {
            spawnList.Add(goRef.value);
        }
        return spawnList.ToArray();
    }

    public void Activate()
    {
        TryActivate();
    }

    // Returns false when nothing could be spawned. A null reference is logged as an
    // error rather than thrown, naming which reference was null and whether it was
    // never assigned or has been destroyed, since only player builds report errors.
    public bool TryActivate()
    {
        if (debug)
            print("SpawnAction activated");
        bool spawnedAny = false;
        if(randomizeToSpawn)
        {
            GameObject prefab = Resolve(toSpawn[Random.Range(0, toSpawn.Length)]);
            if (prefab == null)
                return false;
            AdjustSpawned(Instantiate(prefab));
            return true;
        }

        foreach (GameObjectReference spawn in toSpawn)
        {
            GameObject prefab = Resolve(spawn);
            if (prefab == null)
                continue;

            GameObject spawned;
            if(spawnLocation.Length > 0)
            {
                Transform location = spawnLocation[Random.Range(0, spawnLocation.Length)];
                if (location == null)
                {
                    LogSkipped("spawnLocation", location);
                    continue;
                }
                spawned = Instantiate(prefab, location.position, prefab.transform.rotation);
            }
            else
            {
                spawned = Instantiate(prefab);
            }

            AdjustSpawned(spawned);
            spawnedAny = true;
        }
        return spawnedAny;
    }

    private GameObject Resolve(GameObjectReference spawn)
    {
        if (spawn == null)
        {
            LogSkipped("toSpawn entry", null);
            return null;
        }
        if (!spawn.useConstant && spawn.variable == null)
        {
            LogSkipped("toSpawn variable", spawn.variable);
            return null;
        }
        GameObject prefab = spawn.value;
        if (prefab == null)
            LogSkipped("toSpawn value", prefab);
        return prefab;
    }

    private void LogSkipped(string what, Object reference)
    {
        string state = ReferenceEquals(reference, null) ? "unassigned" : "destroyed";
        Debug.LogError($"SpawnAction on {name} skipped: {what} {state}");
    }

    private void AdjustSpawned(GameObject spawned)
    {
        //if (spawnLocation.Length > 0)
        //{
        //    Transform tempLocation = spawnLocation[Random.Range(0, spawnLocation.Length)];
        //    if (spawned != null)
        //    {
        //        spawned.transform.position = tempLocation.position;
        //    }
        //}
        if (spawnRotation != null)
        {
            spawned.transform.rotation = spawnRotation.rotation;
        }
        if (parentTo != null)
        {
            spawned.transform.SetParent(parentTo.transform);
        }
    }
}
