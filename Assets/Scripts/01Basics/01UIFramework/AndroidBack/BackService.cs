using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLab.AndroidBack
{
    public class BackService : MonoBehaviour
    {
        public Stack<BackListener> uiStack = new Stack<BackListener>();

        public BackListener CurrentListener;

        private static BackService instance;

        public static BackService Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject go = new GameObject("BackService");
                    instance = go.AddComponent<BackService>();
                }
                return instance;
            }
        }
        private BackListener lastbackListener;

#if UNIYT_EDITOR 

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.Escape))
            {
                if (CurrentListener != null)
                {
                    if (CurrentListener != lastbackListener && CurrentListener.OnBack != null)
                    {
                        CurrentListener.OnBack();
                    }
                }
                else
                {
                    if (uiStack.Count > 0)
                    {
                        BackListener currentUI = uiStack.Peek();
                        if (currentUI != lastbackListener)
                        {
                            if (currentUI != null && currentUI.OnBack != null)
                            {
                                currentUI.OnBack();
                            }
                        }
                    }
                    else
                    {
                        BackListener currentUI = uiStack.Peek();
                        if (currentUI != null && currentUI.OnQuit != null)
                        {
                            currentUI.OnQuit();
                        }
                    }
                }
            }
        }

#endif
        public void WindowOpen(bool isRoot, bool canBack, BackListener backListener)
        {
            if (isRoot)
            {
                uiStack.Clear();
            }

            if (canBack)
            {
                uiStack.Push(backListener);
            }
            else
            {
                CurrentListener = backListener;
            }
        }

        public void WindowClose(bool canBack, BackListener backListener)
        {
            if (canBack)
            {
                if (uiStack.Count > 0)
                {
                    lastbackListener = uiStack.Pop();
                }
            }
            else
            {
                CurrentListener = null;
                lastbackListener = null;
            }
        }
    }
}
