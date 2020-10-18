using Assets.Scripts;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPoolsInitializers : MonoBehaviour
{
    //private List<string> _poolTypes = new List<string>();

    public static ObjectPoolsInitializers Instance;

    void Awake()
    {
        Instance = this;

        InitializeObjectPools();
    }

    private void InitializeObjectPools()
    {
        CreatePoolFromResources("Prefabs/Coins", 1, null);
        CreatePoolFromResources("Prefabs/Grounds", 2, null);
        CreatePoolFromResources("Prefabs/PowerUps", 2, null);
        CreatePoolFromResources("Prefabs/Hole", 1, null);
        CreatePoolFromResources("Prefabs/Enemies", 1, null);
        CreatePoolFromResources("Prefabs/Backgrounds", 1, null);
        CreatePoolFromResources("Prefabs/Foregrounds", 1, null);
        CreatePoolFromResources("Prefabs/Particles", 1, null);
    }

    private void CreatePoolFromResources(string resourcesPath, int poolSize, System.Action<GameObject> method)
    {
        Object[] resourcesPrefabs = ResourceLoader.LoadGameObjectFromPath(resourcesPath);

        foreach (Object prefab in resourcesPrefabs)
        {
            if (!prefab)
            {
                throw new System.Exception("Resource loading has failed!");
            }

            string poolName = prefab.name;
            //_poolTypes.Add(poolName);

            // A callback to attach the pool name to the instantiated object.
            method = (go) =>
            {
                PrefabTracker tracker = go.AddComponent<PrefabTracker>();
                tracker.SetPrefabName(poolName);
            };

            ObjectPoolManager.Instance.CreatePool(poolName, GetPool((GameObject)prefab, poolSize, method));
        }
    }

    public ObjectPool GetPool(GameObject poolGameObject, int poolSize, System.Action<GameObject> method)
    {
        return new ObjectPool(poolGameObject, poolSize, method);
    }
}