using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class GenericFactory
{
    /// <summary>
    /// Creating new instance of prefab.
    /// </summary>
    /// <returns>New instance of prefab.</returns>
    public GameObject GetNewInstance(string  poolType, Vector3 position)
    {
        return ObjectPoolManager.Instance.Spawn(poolType, position);
    }
}


