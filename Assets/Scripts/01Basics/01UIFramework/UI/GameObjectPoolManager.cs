using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 对象池管理类
/// </summary>
public static class GameObjectPoolManager
{
    #region Config

    //池子最大容量
    public const int MAX = 250;

    #endregion

    /// <summary>取出来对象池</summary>
    /// <param name="o"></param>
    public static GameObject GetObj(string path)
    {
        GameObject ret = null;
        if (!string.IsNullOrEmpty(path))
        {
            ret = GetObj(Resources.Load(path));
        }
        return ret;
    }

    /// <summary>取出来对象池</summary>
    /// <param name="o"></param>
    public static GameObject GetObj(Object obj)
    {
        GameObject ret = null;
        do
        {
            if (obj == null)
            {
                break;
            }
            ret = PopPool(obj);
            if (ret == null)//对象池中没有,实例化出来      
            {
                ret = GameObject.Instantiate(obj) as GameObject;
                ret.name = obj.name;
            }
            PushActivePool(ret, obj);
        } while (false);

        return ret;
    }

    /// <summary>放回对象池</summary>
    /// <param name="o"></param>
    public static void Recycle(GameObject o)
    {
        Recycle(o, false);
    }

    /// <summary>放回对象池</summary>
    /// <param name="o"></param>
    public static void Recycle(GameObject o, bool isDetory)
    {
        do
        {
            if (o == null)
            {
                break;
            }
            CreateRoot();
            Object prefab = PopActivePool(o);
            if (prefab == null || isDetory)//立即销毁的 或 不是从对象池中取出来的
            {
                MyDestroy(o);
                break;
            }

            PushPool(o, prefab);

            Check();
        } while (false);
    }

    /// <summary>消除所有对象</summary>
    public static void DestroyAll()
    {
        inactiveDic.Clear();
        activeDic.Clear();
        enterInfoLs.Clear();
        if (poolRoot != null)
        {
            GameObject.Destroy(poolRoot.gameObject);
        }
    }

    //****************************私有方法************************
    //

    //池子根节点
    private static Transform poolRoot;

    //对象池缓存的对象
    private static Dictionary<Object, List<GameObject>> inactiveDic = new Dictionary<Object, List<GameObject>>();

    //对象池激活的对象
    private static Dictionary<GameObject, Object> activeDic = new Dictionary<GameObject, Object>();

    //记录进入池的循序
    private static List<GameObjectInfo> enterInfoLs = new List<GameObjectInfo>();

    /// <summary>压入缓存池</summary>
    /// <param name="o"></param>
    /// <param name="prefab"></param>
    private static void PushPool(GameObject o, Object prefab)
    {
        if (!inactiveDic.ContainsKey(prefab)) inactiveDic.Add(prefab, new List<GameObject>());
        if (!inactiveDic[prefab].Contains(o))
        {
            inactiveDic[prefab].Add(o);

            o.SetActive(false);
            o.transform.SetParent(poolRoot);

            RecordEntry(o, prefab);
        }
    }

    /// <summary>压出缓存池</summary>
    /// <param name="o"></param>
    /// <param name="prefab"></param>
    private static GameObject PopPool(Object prefab)
    {
        GameObject ret = null;
        if (prefab != null && inactiveDic.ContainsKey(prefab) && inactiveDic[prefab].Count > 0)
        {
            do
            {
                ret = inactiveDic[prefab][0];
                inactiveDic[prefab].RemoveAt(0);
            } while (ret == null && inactiveDic[prefab].Count > 0);//防止之前界面存在引用，删掉了缓存池的东西

            if (ret != null)
            {
                ret.gameObject.SetActive(true);
                ret.transform.SetParent(null);

                DeleteRecordEntry(ret);
            }
        }
        return ret;
    }

    /// <summary>压入激活缓存池</summary>
    /// <param name="o"></param>
    /// <param name="prefab"></param>
    private static void PushActivePool(GameObject o, Object prefab)
    {
        if (!activeDic.ContainsKey(o))
        {
            activeDic.Add(o, prefab);
        }
    }

    /// <summary>压出激活缓存池</summary>
    /// <param name="o"></param>
    /// <param name="prefab"></param>
    private static Object PopActivePool(GameObject prefab)
    {
        Object ret = null;
        if (activeDic.ContainsKey(prefab))
        {
            ret = activeDic[prefab];
            activeDic.Remove(prefab);
        }
        return ret;
    }

    /// <summary>创建根节点</summary>
    private static void CreateRoot()
    {
        if (poolRoot == null)
        {
            poolRoot = new GameObject().transform;
            poolRoot.name = "PoolRoot";
        }
    }

    /// <summary>
    /// 检查是否需要释放
    /// </summary>
    private static void Check()
    {
        while (enterInfoLs.Count > MAX && enterInfoLs.Count > 0)
        {
            MyDestroy(PopPool(enterInfoLs[0].prefab));
            enterInfoLs.RemoveAt(0);
        }
    }

    /// <summary>记录</summary>
    /// <param name="o"></param>
    private static void RecordEntry(GameObject o, Object prefab)
    {
        enterInfoLs.Add(new GameObjectInfo(o, prefab));
    }

    /// <summary>删除记录</summary>
    /// <param name="o"></param>
    private static void DeleteRecordEntry(GameObject o)
    {
        for (int i = enterInfoLs.Count - 1; i >= 0; i--)
        {
            if (enterInfoLs[i].gameobject == o)
            {
                enterInfoLs.RemoveAt(i);
                break;
            }
            if (enterInfoLs[i].gameobject == null)
            {
                enterInfoLs.RemoveAt(i);
            }
        }
    }

    /// <summary>删除资源</summary>
    /// <param name="obj"></param>
    private static void MyDestroy(GameObject obj)
    {
        GameObject.Destroy(obj);
    }
}

public class GameObjectInfo
{
    //实例化物体
    public GameObject gameobject;

    //预设
    public Object prefab;

    public GameObjectInfo(GameObject gameobject, Object prefab)
    {
        this.gameobject = gameobject;
        this.prefab = prefab;
    }
}