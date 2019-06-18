using UnityEngine;
using System.Collections;
using GameLab.UIFramework;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance = null;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(T)) as T;
                if (instance == null)
                {
                    instance = new GameObject("_" + typeof(T).Name).AddComponent<T>();
                    DontDestroyOnLoad(instance);
                }
            }
            return instance;
        }
    }

    void OnApplicationQuit()
    {
        if (instance != null)
        {
            instance = null;
        }
    }
}