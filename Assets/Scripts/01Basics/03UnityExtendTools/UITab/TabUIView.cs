using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace UnityExtendTools.TabUI
{
    public class TabUIView : MonoBehaviour
    {
        [SerializeField] private Transform CellObj;
        private TabItem[] tabItems;

        public event Action<int> OnSelecItem;

        void Start()
        {
            tabItems = CellObj.transform.GetComponentsInChildren<TabItem>();
            InitChildTabItem();
        }

        private void InitChildTabItem()
        {
            if (tabItems != null && tabItems.Length > 0)
            {
                for (int i = 0; i < tabItems.Length; i++)
                {
                    tabItems[i].OnInit(SelectedMenuItem);
                    tabItems[i].IsActive = true;
                }
            }
        }

        private void SelectedMenuItem(int index)
        {
            OnSelecItem?.Invoke(index);
        }
    }
}
