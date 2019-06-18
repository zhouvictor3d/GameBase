using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

[CustomEditor(typeof(Image))]
public class ImageOnInspector : Editor
{
    [SerializeField]
    private Image ImageTarget;

    public override void OnInspectorGUI()
    {
        Image t = (Image)target;

        ImageTarget = t;
        
        GUILayout.Label("扩展资源");
    }
}
