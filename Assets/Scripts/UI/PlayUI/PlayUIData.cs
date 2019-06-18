using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameLab.UIFramework;

public class PlayUIData : IUIData
{
    public int Coin { get; set; }
    public UserInfo userInfo { get; set; }
    public List<RewardInfo> RewardInfoList { get; set; }
    public List<CardInfo> CardInfoList { get; set; }
    public int PlayCost { get; set; }

}