using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class PoolController
{
    private static Dictionary<string, GameObject> GameObjectPool = new Dictionary<string, GameObject>();

    public static GameObject GetGameObject(string path)
    {
        GameObject gobject = null;
        if (GameObjectPool != null)
        {
            if (GameObjectPool.ContainsKey(path))
            {
                gobject = GameObjectPool[path];
            }
            else
            {
                gobject = Resources.Load<GameObject>(path);
                GameObjectPool.Add(path, gobject);
            }
        }
        return gobject;
    }

}

