using System.Collections.Generic;
using Assets.Scripts.Data.Vo;
using Assets.Scripts.Enums;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "RoofGames/Data/Config/SkillData",order = 1)]
public class CD_SkillData : SerializedScriptableObject
{
   [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.CollapsedFoldout)]
   public Dictionary<SkillType,SkillVo> List = new Dictionary<SkillType, SkillVo>();
}

