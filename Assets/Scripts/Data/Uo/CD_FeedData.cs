using System.Collections.Generic;
using Assets.Scripts.Data.Vo;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "RoofGames/Data/Config/FeedData",order = 4)]
public class CD_FeedData : SerializedScriptableObject
{
   [ListDrawerSettings(ShowIndexLabels = true,ListElementLabelName = "Type")]
   public List<FeedVo> List = new List<FeedVo>();
}

