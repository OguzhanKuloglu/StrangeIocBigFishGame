using System.Collections.Generic;
using Assets.Scripts.Data.Vo;
using Assets.Scripts.Enums;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "RoofGames/Data/Runtime/LevelStatusData", order = 99)]
public class RD_LevelStatusData : SerializedScriptableObject
{
    [ListDrawerSettings(ShowIndexLabels = true)]
    public List<LevelDataVo> List = new List<LevelDataVo>();
    public int TotalScore;
}

