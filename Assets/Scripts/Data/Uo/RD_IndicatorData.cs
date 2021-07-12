using System.Collections.Generic;
using Assets.Scripts.Data.Vo;
using Assets.Scripts.Enums;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "RoofGames/Data/Runtime/IndicatorData", order = 10)]
public class RD_IndicatorData : SerializedScriptableObject
{
    public GameObject Player;

    [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.CollapsedFoldout)]
    public Dictionary<Transform, CharacterVo> ActiveEnemyList = new Dictionary<Transform, CharacterVo>();
}
