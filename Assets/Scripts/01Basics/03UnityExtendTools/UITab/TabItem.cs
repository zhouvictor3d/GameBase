
using UnityEngine;
using UnityEngine.UI;
using System;

namespace UnityExtendTools.TabUI
{
    public class TabItem : MonoBehaviour
    {
        [SerializeField] private Text TabItemText;
        [SerializeField] private Image TabActiveImage;
        [SerializeField] private Image TabNormalImage;
        [SerializeField] private Button TabButton;

        private bool isActive;
        public bool IsActive
        {
            get
            {
                return isActive;
            }
            set
            {
                isActive = !isActive;
                TabActiveImage.gameObject.SetActive(isActive);
                TabNormalImage.gameObject.SetActive(!isActive);
            }
        }

        public void OnInit(Action<int> OnTabSelectAction)
        {
            TabButton = this.gameObject.GetComponent<Button>();
            if (TabButton != null)
            {
                TabButton.onClick.AddListener(() =>
                {
                    OnTabSelectAction?.Invoke(GetIndex());
                });
            }
        }
        private int GetIndex()
        {
            if (this.transform.parent.childCount > 0)
            {
                for (int i = 0; i < transform.parent.childCount; i++)
                {
                    if (gameObject == transform.parent.GetChild(i).gameObject)
                    {
                        return i;
                    }
                }
            }
            else
            {
                Debug.LogError("TabView not contant childItem");
            }
            return -1;
        }
    }
}
