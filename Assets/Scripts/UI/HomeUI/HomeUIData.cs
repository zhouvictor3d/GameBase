using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameLab.UIFramework;

public class HomeUIData : IUIData
{
    public UserInfo UserData { get; set; }
    public bool UnlockRank { get; set; }
    public bool UnlockTask { get; set; }
    public bool UnlockMessage { get; set; }
    public bool UnlockSale { get; set; }
}
