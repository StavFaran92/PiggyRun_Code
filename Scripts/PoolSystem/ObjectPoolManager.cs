using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

public class ObjectPoolManager
{
    public Dictionary<string, ObjectPool> objectsPool;
    static ObjectPoolManager instance = null;
    public static ObjectPoolManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ObjectPoolManager();
                instance.objectsPool = new Dictionary<string, ObjectPool>();
            }
            return instance;
        }
    }
    public void CreatePool(string poolType, ObjectPool pool)
    {
        if (!objectsPool.ContainsKey(poolType))
        {
            objectsPool.Add(poolType, pool);
        }
        else
        {
            objectsPool[poolType].Clear();
        }
    }
    public void Reset()
    {
        foreach (var item in objectsPool)
        {
            item.Value.Reset();
        }
    }
    public ObjectPool GetPool(string poolType)
    {
        try
        {
            return objectsPool[poolType];
        }
        catch
        {
            Debug.Log(poolType);
        }
        return null;
    }
    public GameObject Spawn(string poolType, Vector3 instantiateVector)
    {
        string prefabName = poolType.Split('_')[0];

        if (Instance.objectsPool.Any())
        {
            if(ObjectPoolManager.Instance.GetPool(prefabName) == null)
            {
                Debug.Log("cannot find the specified pool: " + prefabName);
            }
            return ObjectPoolManager.Instance.GetPool(prefabName).Spawn(instantiateVector, Quaternion.identity);
        }

        return null;
    }

    public GameObject Spawn(string poolType, Vector3 instantiateVector, Transform parent)
    {
        string prefabName = poolType.Split('_')[0];

        if (Instance.objectsPool.Any())
        {
            if (ObjectPoolManager.Instance.GetPool(prefabName) == null)
            {
                Debug.Log("cannot find the specified pool");
            }
            return ObjectPoolManager.Instance.GetPool(prefabName).Spawn(instantiateVector, Quaternion.identity, parent);
        }

        return null;
    }

    public void Destroy(string poolType, GameObject target)
    {
        string prefabName = poolType.Split('_')[0];

        if (ObjectPoolManager.Instance.objectsPool.Any())
        {
            ObjectPool pool = ObjectPoolManager.Instance.GetPool(prefabName);
            pool.Destroy(target);
        }
    }

    public IEnumerator Destroy(string poolType, GameObject target, float delay)
    {
        yield return new WaitForSeconds(delay);

        Destroy(poolType, target);
    }
}