using System.Collections.Generic;
using Assets.Scripts.Data.Vo;
using Assets.Scripts.Enums;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "RoofGames/Data/Runtime/PlayerPointData", order = 20)]
public class RD_PlayerPointData : SerializedScriptableObject
{
    public int BonusFeedCounter;
    public int NormalFeedCounter;


}
