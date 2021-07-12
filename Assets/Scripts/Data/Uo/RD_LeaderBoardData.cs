using System.Collections.Generic;
using Assets.Scripts.Data.Vo;
using Assets.Scripts.Enums;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "RoofGames/Data/Runtime/LeaderBoardData", order = 11)]
public class RD_LeaderBoardData : SerializedScriptableObject
{
    public bool PlayerDead;

    [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.CollapsedFoldout)]
    public Dictionary<Transform, CharacterVo> ActiveCharacterList = new Dictionary<Transform, CharacterVo>();
}

