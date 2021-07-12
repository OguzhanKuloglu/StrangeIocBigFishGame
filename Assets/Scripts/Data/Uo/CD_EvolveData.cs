using System.Collections.Generic;
using Assets.Scripts.Data.Vo;
using Assets.Scripts.Enums;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "RoofGames/Data/Config/EvolveData",order = 0)]
public class CD_EvolveData : SerializedScriptableObject
{
   [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.CollapsedFoldout)]
   public Dictionary<EvolveType,EvolveVo> List = new Dictionary<EvolveType, EvolveVo>();
}

