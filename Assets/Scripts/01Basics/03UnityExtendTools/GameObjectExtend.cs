using System;
using System.Collections.Generic;
using UnityEngine;



public static class GameObjectExtend
{
    public static GameObject Inistance(this GameObject gameObject, GameObject prefabObj, Transform parentTransform)
    {
        GameObject gobject = GameObject.Instantiate<GameObject>(prefabObj, parentTransform);
        gobject.transform.localPosition = Vector3.zero;
        gobject.transform.localRotation = Quaternion.identity;
        gobject.transform.localScale = Vector3.one;
        return gobject;
    }
}

