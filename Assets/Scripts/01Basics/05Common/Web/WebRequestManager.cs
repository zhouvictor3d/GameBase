using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

/// <summary>
/// Http Web 请求
/// </summary>
public class WebRequestManager : MonoBehaviour
{

    public IEnumerator Get(string path, Action<string> action)
    {
        if (!string.IsNullOrEmpty(path))
        {
            UnityWebRequest www = new UnityWebRequest(path);
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.LogError(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                string text = www.downloadHandler.text;
                action?.Invoke(text);
            }
        }
        yield return null;
    }

    public IEnumerator Post(string path, List<IMultipartFormSection> formData)
    {
        if (!string.IsNullOrEmpty(path) && formData != null)
        {
            UnityWebRequest www = UnityWebRequest.Post(path, formData);
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
        yield return null;
    }


    public IEnumerator Post(string path, WWWForm form)
    {
        if (!string.IsNullOrEmpty(path) && form != null)
        {
            UnityWebRequest www = UnityWebRequest.Post(path, form);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
        yield return null;
    }


    public IEnumerator Download()
    {
        yield return null;

    }

    public IEnumerator Upload(string path)
    {
        yield return null;
    }
}
