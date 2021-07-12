using System.Collections.Generic;
using Assets.Scripts.Data.Vo;
using Assets.Scripts.Enums;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "RoofGames/Data/Config/ScaleData",order = 5)]
public class CD_ScaleData : SerializedScriptableObject
{
   [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.CollapsedFoldout)]
   public Dictionary<FeedType, List<ScaleVo>> List = new Dictionary<FeedType, List<ScaleVo>>();
}
