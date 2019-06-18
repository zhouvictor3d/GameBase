using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameLab.UIFramework
{
    public class WindowData
    {
        /// <summary>
        /// UI数据
        /// </summary>
        public IUIData UIData { get; set; }

        /// <summary>
        /// 模型路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// UI 类型
        /// </summary>
        public UIType UIType { get; set; }
    }

    /// <summary>
    /// UI 类型
    /// </summary>
    public enum UIType
    {
        Window = 0,
        Tip = 1,
        Confirm = 2
    }

    /// <summary>
    /// UI 层级
    /// </summary>
    public enum UILayer
    {
        BottomLayer = 0,
        TargetLayer = 1,
        TopLayer = 2
    }
}