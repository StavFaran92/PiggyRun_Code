using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
public class ObjectPool
{
    GameObject poolGameObject;
    Stack<GameObject> availableGameObjects;
    List<GameObject> allGameObjects;
    Action<GameObject> initAction;
    public ObjectPool(GameObject gameObject, int initialCapacity, Action<GameObject> initAction)
    {
        this.poolGameObject = gameObject;
        this.initAction = initAction;
        availableGameObjects = new Stack<GameObject>();
        allGameObjects = new List<GameObject>();
        int index = 0;
        for (index = 0; index < initialCapacity; index++)
        {
            GameObject localGameObject = GetGameObject();
            allGameObjects.Add(localGameObject);
            availableGameObjects.Push(localGameObject);
        }
    }
    private GameObject GetGameObject()
    {
        GameObject result = GameObject.Instantiate(this.poolGameObject) as GameObject;
        result.name = poolGameObject.name + "_" + (allGameObjects.Count + 1).ToString();
        initAction?.Invoke(result);
        result.SetActive(false);
        return result;
    }
    #region Methods
    public GameObject Spawn(Vector3 position, Quaternion rotation)
    {
        GameObject result = null;
        if (!availableGameObjects.Any())
        {
            result = GetGameObject();
            allGameObjects.Add(result);
            availableGameObjects.Push(result);
        }
        result = availableGameObjects.Pop();
        Transform resultTrans = result.transform;
        resultTrans.position = position;
        resultTrans.rotation = rotation;
        SetActive(result, true);
        //Debug.Log("Spawned object: "+result.name );
        result.SendMessage("Start");
        return result;
    }

    public GameObject Spawn(Vector3 position, Quaternion rotation, Transform parent)
    {
        GameObject result = null;
        if (!availableGameObjects.Any())
        {
            result = GetGameObject();
            allGameObjects.Add(result);
            availableGameObjects.Push(result);
        }
        result = availableGameObjects.Pop();
        Transform resultTrans = result.transform;
        resultTrans.position = position;
        resultTrans.rotation = rotation;
        resultTrans.parent = parent;
        SetActive(result, true);
        result.SendMessage("Start");
        return result;
    }

    public bool Destroy(GameObject target)
    {
        availableGameObjects.Push(target);
        SetActive(target, false);
        return true;
    }
    public void Clear()
    {
        foreach (var item in availableGameObjects)
        {
            GameObject.Destroy(item);
        }
        foreach (var item in allGameObjects)
        {
            GameObject.Destroy(item);
        }
        availableGameObjects.Clear();
        allGameObjects.Clear();
    }
    public void Reset()
    {
        availableGameObjects.Clear();
        foreach (var item in allGameObjects)
        {
            item.SetActive(false);
            availableGameObjects.Push(item);
        }
    }
    protected void SetActive(GameObject target, bool value)
    {
        target.SetActive(value);
    }
    #endregion
}