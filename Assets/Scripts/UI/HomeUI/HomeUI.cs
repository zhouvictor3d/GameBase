using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityExtendTools.TabUI;
using GameLab.UIFramework;

public class HomeUI : UIWindowBase
{
    [SerializeField] private Button BtnPlay;
    [SerializeField] private TabUIView MainMenu;
    [SerializeField] private TopBarUI TopBarUI;

    public override void UIInit(IUIData data)
    {
        HomeUIData homeUIData = (HomeUIData)data;
        TopBarUI.InitUI(homeUIData.UserData.Coin, homeUIData.UserData.Gem);
        BtnPlay?.onClick.AddListener(() => { BtnPlayFunction(); });
        MainMenu.OnSelecItem += MainMenu_OnSelecItem;
    }

    private void BtnPlayFunction()
    {
        GameUIController.Instance.ShowUI(UIWindowType.PlayUI);
    }

    private void MainMenu_OnSelecItem(int index)
    {
        Debug.Log("index=====>" + index);
    }
}
