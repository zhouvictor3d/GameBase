
using System;
using System.Collections;
using UnityEngine;

namespace GameLab.AndroidBack
{
    public class BackListener
    {
        private bool CanBack;
        private bool IsRoot;
        public string UIName { get; set; }
        public BackListener(bool canBackToThisWindow, bool isRoot = false)
        {
            CanBack = canBackToThisWindow;
            IsRoot = isRoot;
        }

        public Action OnBack { get; set; }

        public Action OnQuit { get; set; }

        public void Open()
        {
            BackService.Instance.WindowOpen(IsRoot, CanBack, this);
        }

        public void Close()
        {
            BackService.Instance.WindowClose(CanBack, this);
        }
    }
}
