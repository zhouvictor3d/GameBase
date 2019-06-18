using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameLab.UIFramework
{
    public class UIWindowBase : MonoBehaviour
    {
        public int MaxSortOrder { get; private set; }
        public int SortOrder { get; set; }
        internal WindowData WindowData { get; set; }

        internal Canvas Canvas;

        private List<CanvasData> CanvasList;

        public virtual void Awake()
        {
            Canvas = this.gameObject.GetComponent<Canvas>();
            if (Canvas == null)
            {
                Canvas = this.gameObject.AddComponent<Canvas>();
            }
            CanvasList = GetAllCanvas();
        }

        /// <summary>
        /// 数据实例化
        /// </summary>
        /// <param name="data"></param>
        public virtual void UIInit(IUIData data)
        {

        }

        /// <summary>
        /// UI 实例化
        /// </summary>
        public virtual void UIInit()
        {

        }

        /// <summary>
        /// 重新激活
        /// </summary>
        public virtual void UIReActive()
        {

        }

        /// <summary>
        ///  数据改变
        /// </summary>
        /// <param name="data"></param>
        public virtual void UIDataChanged(IUIData data)
        {

        }

        /// <summary>
        /// 开始关闭
        /// </summary>
        public virtual void Closing()
        {

        }

        /// <summary>
        /// 开始关闭
        /// </summary>
        public virtual void Closed()
        {

        }

        /// <summary>
        /// 返回
        /// </summary>
        public virtual void Back()
        {

        }

        /// <summary>
        /// 显示Banner移动UI
        /// </summary>
        /// <param name="bannerType">广告类型</param>
        /// <param name="bannerHeight">广告宽度</param>
        public virtual void ShowBanner(BannerType bannerType, float bannerHeight)
        {

        }

        /// <summary>
        /// 关闭Banner移动UI
        /// </summary>
        public virtual void CloseBanner()
        {
            ShowBanner(BannerType.Top, 0);
        }

        /// <summary>
        /// 移动到最底层
        /// </summary>
        internal void MoveLayerToBottom()
        {
            UIWindowBase bottom = HiUIManager.Instance.GetBottomUIWindow();
            int bottomLayer = bottom.SortOrder;
            InsertTargetLayerDown(bottom);
            SetSortOrder(bottomLayer);
        }

        /// <summary>
        /// 移动到最上层
        /// </summary>
        internal void MoveLayerToTop()
        {
            UIWindowBase top = HiUIManager.Instance.GetTopUIWindow();
            int topLayer = top.SortOrder;
            SetSortOrder(topLayer);
        }

        /// <summary>
        /// 向上移动一层
        /// </summary>
        internal void MoveLayerToUp()
        {
            SetSortOrder(SortOrder + 1);
        }

        /// <summary>
        /// 向下移动一层
        /// </summary>
        internal void MoveLayerToDown()
        {
            SetSortOrder(SortOrder - 1);
        }

        /// <summary>
        /// 设置层级
        /// </summary>
        /// <param name="order"></param>
        internal void SetSortOrder(int order)
        {
            if (Canvas != null)
            {
                Canvas.overrideSorting = true;
                Canvas.sortingOrder = order;
                SortOrder = Canvas.sortingOrder;
                MaxSortOrder = SortOrder;
            }

            for (int i = 0; i < CanvasList.Count; i++)
            {
                if (CanvasList[i] != null && CanvasList[i].Canvas != Canvas)
                {
                    Canvas.overrideSorting = true;
                    CanvasList[i].Canvas.sortingOrder = Canvas.sortingOrder + CanvasList[i].SortInterval;
                    if (CanvasList[i].Canvas.sortingOrder > MaxSortOrder)
                    {
                        MaxSortOrder = CanvasList[i].Canvas.sortingOrder;
                    }
                }
            }
        }

        /// <summary>
        /// 在目标层上方
        /// </summary>
        /// <param name="targetUIWindow"></param>
        private void InsertTargetLayerUp(UIWindowBase targetUIWindow)
        {
            if (targetUIWindow != null)
            {
                int targetLayer = targetUIWindow.SortOrder + 1;
                for (int i = 0; i < HiUIManager.Instance.CurrentWindowList.Count; i++)
                {
                    if (HiUIManager.Instance.CurrentWindowList[i].SortOrder >= targetLayer)
                    {
                        HiUIManager.Instance.CurrentWindowList[i].MoveLayerToUp();
                    }
                }
            }
        }

        /// <summary>
        /// 在目标层下方
        /// </summary>
        private void InsertTargetLayerDown(UIWindowBase targetUIWindow)
        {
            if (targetUIWindow != null)
            {
                int targetLayer = targetUIWindow.SortOrder;
                for (int i = 0; i < HiUIManager.Instance.CurrentWindowList.Count; i++)
                {
                    if (HiUIManager.Instance.CurrentWindowList[i].SortOrder >= targetLayer)
                    {
                        HiUIManager.Instance.CurrentWindowList[i].MoveLayerToUp();
                    }
                }
            }
        }

        /// <summary>
        /// 获取所有All Canvas
        /// </summary>
        /// <returns></returns>
        private List<CanvasData> GetAllCanvas()
        {
            List<CanvasData> canvasDataList = new List<CanvasData>();
            Canvas[] canvas = this.gameObject.GetComponentsInChildren<Canvas>(true);
            CanvasData canvasData = null;
            for (int i = 0; i < canvas.Length; i++)
            {
                canvasData = new CanvasData();
                canvasData.Canvas = canvas[i];
                canvasData.SortInterval = canvas[i].sortingOrder - Canvas.sortingOrder;
                if (canvas[i] == Canvas)
                {
                    canvasData.IsBaseCanvas = true;
                }
                else
                {
                    canvasData.IsBaseCanvas = false;
                }
                canvasDataList.Add(canvasData);
            }
            return canvasDataList;
        }
    }

    public class CanvasData
    {
        /// <summary>
        /// 层级
        /// </summary>
        public Canvas Canvas { get; set; }

        /// <summary>
        /// 与基础Canvas 的间隔
        /// </summary>
        public int SortInterval { get; set; }

        /// <summary>
        /// 基础Canvas
        /// </summary>
        public bool IsBaseCanvas { get; set; }
    }

    public enum BannerType
    {
        Top = 0,
        Bottom = 1
    }
}