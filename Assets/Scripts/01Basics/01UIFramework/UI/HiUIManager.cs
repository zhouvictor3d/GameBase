using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GameLab.UIFramework
{
    public class HiUIManager : MonoSingleton<HiUIManager>
    {
        [SerializeField]
        public Transform UIRoot;

        [SerializeField]
        public Transform UnEnableUIRoot;

        /// <summary>
        /// UI 对象缓存池
        /// </summary>
        internal Dictionary<string, UIWindowBase> UIWindowDictonary = new Dictionary<string, UIWindowBase>();

        /// <summary>
        /// 当前展示的UI
        /// </summary>
        internal List<UIWindowBase> CurrentWindowList = new List<UIWindowBase>();

        /// <summary>
        /// 当前最上方UI
        /// </summary>
        public UIWindowBase CurrentActiveUI { get; set; }

        public bool IsBannerShow { get; set; }

        /// <summary>
        /// 展示UI
        /// </summary>
        /// <param name="windowData"></param>
        public void Show(WindowData windowData)
        {
            //实例化UI
            UIWindowBase uiWindowBase = GetCurrentWindowBase(windowData);

            // 设置UI层级
            if (CurrentActiveUI == null || CurrentWindowList.Count <= 0)
            {
                uiWindowBase.SetSortOrder(0);
            }
            else if (CurrentActiveUI != uiWindowBase)
            {
                if (windowData.UIType.Equals(CurrentActiveUI.WindowData.UIType))
                {
                    CurrentActiveUI.Closing();
                    CurrentWindowList.Remove(CurrentActiveUI);
                    CurrentActiveUI.gameObject.transform.SetParent(UnEnableUIRoot);
                    CurrentActiveUI.Closed();
                    CurrentActiveUI = null;
                }
                else
                {
                    if (windowData.UIType == UIType.Window)
                    {
                        CloseToWindowUIAndTips(uiWindowBase);
                    }
                }

                CurrentActiveUI = GetTopUIWindow();
                if (CurrentActiveUI != null)
                {
                    uiWindowBase.SetSortOrder(CurrentActiveUI.MaxSortOrder + 2);
                }
                else
                {
                    uiWindowBase.SetSortOrder(0);
                }
            }

            if (!CurrentWindowList.Contains(uiWindowBase))
            {
                CurrentWindowList.Add(uiWindowBase);
            }

            if (!UIWindowDictonary.ContainsKey(windowData.Path))
            {
                UIWindowDictonary.Add(windowData.Path, uiWindowBase);
            }

            CurrentActiveUI = uiWindowBase;

            if (windowData.UIData != null)
            {
                CurrentActiveUI.UIInit(windowData.UIData);
            }
            else
            {
                CurrentActiveUI.UIInit();
            }

            //if (IsBannerShow)
            //{
            //    CurrentActiveUI.ShowBanner(BannerType.Top,DPHelper.GetBannerHeightPixel());
            //}
            //else
            //{
            //    CurrentActiveUI.CloseBanner();
            //}
        }

        public void OnBannerChanged(bool isBannerShow)
        {
            //IsBannerShow = isBannerShow;
            //if (isBannerShow)
            //{
            //    if (CurrentWindowList != null && CurrentWindowList.Count > 0)
            //    {

            //        CurrentActiveUI.ShowBanner(BannerType.Top,DPHelper.GetBannerHeightPixel());
            //        for (int i = 0; i < CurrentWindowList.Count; i++)
            //        {
            //            CurrentWindowList[i].ShowBanner(BannerType.Top, DPHelper.GetBannerHeightPixel());
            //        }
            //    }
            //}
            //else
            //{
            //    if (CurrentWindowList != null && CurrentWindowList.Count > 0)
            //    {
            //        CurrentActiveUI.CloseBanner();
            //        for (int i = 0; i < CurrentWindowList.Count; i++)
            //        {
            //            CurrentWindowList[i].CloseBanner();
            //        }
            //    }
            //}
        }

        /// <summary>
        /// 刷新UI
        /// </summary>
        /// <param name="windowData"></param>
        public void Refresh(WindowData windowData)
        {
            if (CurrentWindowList != null && CurrentWindowList.Count > 0)
            {
                for (int i = 0; i < CurrentWindowList.Count; i++)
                {
                    if (CurrentWindowList[i].WindowData.Path.Equals(windowData.Path))
                    {
                        CurrentWindowList[i].UIDataChanged(windowData.UIData);
                    }
                }
            }
        }

        /// <summary>
        /// 关闭固定UI
        /// </summary>
        public void Close(WindowData windowData)
        {
            if (CurrentWindowList != null && CurrentWindowList.Count > 1)
            {
                UIWindowBase closeWindow = null;
                for (int i = 0; i < CurrentWindowList.Count; i++)
                {
                    if (string.Equals(CurrentWindowList[i].WindowData.Path, windowData.Path))
                    {
                        closeWindow = CurrentWindowList[i];
                    }
                }
                if (closeWindow != null)
                {
                    if (windowData.UIType == UIType.Window)
                    {
                        CloseToWindowUIAndTips(closeWindow);
                    }
                    else
                    {
                        closeWindow.Closing();
                        CurrentWindowList.Remove(closeWindow);
                        closeWindow.gameObject.transform.SetParent(UnEnableUIRoot);
                        closeWindow.Closed();
                    }
                }
                closeWindow = null;
                CurrentActiveUI = GetTopUIWindow();
                if (CurrentActiveUI != null)
                {
                    CurrentActiveUI.UIReActive();
                }
            }
        }

        public UIWindowBase GetWindow(WindowData windowData)
        {
            if (CurrentWindowList != null && CurrentWindowList.Count > 1)
            {
                UIWindowBase closeWindow = null;
                for (int i = 0; i < CurrentWindowList.Count; i++)
                {
                    if (string.Equals(CurrentWindowList[i].WindowData.Path, windowData.Path))
                    {
                        closeWindow = CurrentWindowList[i];
                        return closeWindow;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 关闭当前UI
        /// </summary>
        public void Close()
        {
            if (CurrentActiveUI != null)
            {
                Close(CurrentActiveUI.WindowData);
            }
        }

        /// <summary>
        /// 获取UiWindowBase
        /// </summary>
        /// <param name="windowData"></param>
        /// <returns></returns>
        private UIWindowBase GetCurrentWindowBase(WindowData windowData)
        {
            GameObject uiObj = CreateUIWindow(windowData.Path);
            UIWindowBase uiWindowBase = uiObj.GetComponent<UIWindowBase>();
            uiWindowBase.WindowData = windowData;
            return uiWindowBase;
        }

        /// <summary>
        /// 创建UI
        /// </summary>
        /// <returns></returns>
        private GameObject CreateUIWindow(string uiPaht)
        {
            if (string.IsNullOrEmpty(uiPaht))
            {
                Debug.Log("UI Paht is null");
                return null;
            }

            GameObject uiObj = null;
            if (UIWindowDictonary.ContainsKey(uiPaht))
            {
                uiObj = UIWindowDictonary[uiPaht].gameObject;
            }
            else
            {
                GameObject gameObject = Resources.Load(uiPaht) as GameObject;
                uiObj = GameObject.Instantiate(gameObject);
            }
            if (uiObj != null)
            {
                uiObj.transform.SetParent(UIRoot);
                uiObj.transform.localPosition = Vector3.zero;
                uiObj.transform.localRotation = Quaternion.identity;
                uiObj.transform.localScale = Vector3.one;
                uiObj.GetComponent<RectTransform>().offsetMin = Vector2.zero;
                uiObj.GetComponent<RectTransform>().offsetMax = Vector2.zero;
                uiObj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            }
            return uiObj;
        }

        /// <summary>
        /// 跳转到别的WindowUI
        /// </summary>
        private void CloseToWindowUIAndTips(UIWindowBase uiWindowBase)
        {
            UIWindowBase topWindow = GetTopWindow();
            if (topWindow != null)
            {
                List<UIWindowBase> closeList = GetCloseWindowList(topWindow.Canvas.sortingOrder);
                closeList.Add(topWindow);
                for (int i = 0; i < closeList.Count; i++)
                {
                    //关闭不等于自己的Window
                    if (!string.Equals(closeList[i].WindowData.Path, uiWindowBase.WindowData.Path))
                    {
                        closeList[i].Closing();
                        CurrentWindowList.Remove(closeList[i]);
                        closeList[i].gameObject.transform.SetParent(UnEnableUIRoot);
                        closeList[i].Closed();
                    }
                }
            }
        }

        /// <summary>
        /// 获取需要激活曾
        /// </summary>
        /// <returns></returns>
        private UIWindowBase GetRequireWindowBase(WindowData windowData)
        {
            if (CurrentWindowList != null && CurrentWindowList.Count > 1)
            {
                UIWindowBase uIWindowBase = null;
                for (int i = 0; i < CurrentWindowList.Count; i++)
                {
                    if (CurrentWindowList[i].SortOrder < CurrentActiveUI.SortOrder)
                    {
                        if (uIWindowBase == null || uIWindowBase.SortOrder < CurrentWindowList[i].SortOrder)
                        {
                            uIWindowBase = CurrentWindowList[i];
                        }
                    }
                }
                return uIWindowBase;
            }
            return null;
        }

        /// <summary>
        /// 获取需要关闭的窗口
        /// </summary>
        /// <param name="layer"></param>
        /// <returns></returns>
        private List<UIWindowBase> GetCloseWindowList(int layer)
        {
            List<UIWindowBase> uIWindowList = new List<UIWindowBase>();
            for (int i = 0; i < CurrentWindowList.Count; i++)
            {
                if (CurrentWindowList[i].SortOrder >= layer)
                {
                    uIWindowList.Add(CurrentWindowList[i]);
                }
            }
            return uIWindowList;
        }

        /// <summary>
        /// 刷新所有后台界面banner状态
        /// </summary>
        public void RefreshAllViewBannerState()
        {
            //if (CurrentWindowList != null && CurrentWindowList.Count > 0)
            //{
            //    for (int i = 0; i < CurrentWindowList.Count; i++)
            //    {
            //        if (IsBannerShow)
            //        {
            //            CurrentWindowList[i].ShowBanner(BannerType.Top, DPHelper.GetBannerHeightPixel());
            //        }
            //        else
            //        {
            //            CurrentWindowList[i].CloseBanner();
            //        }
            //    }
            //}
        }

        /// <summary>
        /// 获取最上层的 Type 为 Windown 的 UI
        /// </summary>
        /// <returns></returns>
        private UIWindowBase GetTopWindow()
        {
            UIWindowBase topWindow = null;
            for (int i = 0; i < CurrentWindowList.Count; i++)
            {
                if (CurrentWindowList[i].WindowData != null && CurrentWindowList[i].WindowData.UIType == UIType.Window)
                {
                    if (topWindow == null || topWindow.SortOrder <= CurrentWindowList[i].SortOrder)
                    {
                        topWindow = CurrentWindowList[i];
                    }
                }
            }
            return topWindow;
        }

        /// <summary>
        /// 获取最上层UI
        /// </summary>
        /// <param name="CurrentWindowList"></param>
        /// <returns></returns>
        internal UIWindowBase GetTopUIWindow()
        {
            UIWindowBase uIWindowBase = null;
            if (CurrentWindowList != null && CurrentWindowList.Count > 0)
            {
                for (int i = 0; i < CurrentWindowList.Count; i++)
                {
                    if (uIWindowBase == null)
                    {
                        uIWindowBase = CurrentWindowList[i];
                    }
                    else
                    {
                        if (uIWindowBase.SortOrder < CurrentWindowList[i].SortOrder)
                        {
                            uIWindowBase = CurrentWindowList[i];
                        }
                    }
                }
            }
            return uIWindowBase;
        }

        /// <summary>
        /// 获取最下层UI
        /// </summary>
        /// <param name="CurrentWindowList"></param>
        /// <returns></returns>
        internal UIWindowBase GetBottomUIWindow()
        {
            UIWindowBase uIWindowBase = null;
            if (CurrentWindowList != null && CurrentWindowList.Count > 0)
            {
                for (int i = 0; i < CurrentWindowList.Count; i++)
                {
                    if (uIWindowBase == null)
                    {
                        uIWindowBase = CurrentWindowList[i];
                    }
                    else
                    {
                        if (uIWindowBase.SortOrder > CurrentWindowList[i].SortOrder)
                        {
                            uIWindowBase = CurrentWindowList[i];
                        }
                    }
                }
            }
            return uIWindowBase;
        }
    }
}