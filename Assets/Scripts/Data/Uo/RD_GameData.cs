using System.Collections.Generic;
using Assets.Scripts.Data.Vo;
using Assets.Scripts.Enums;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "RoofGames/Data/Runtime/GameData",order = 0)]
public class RD_GameData : SerializedScriptableObject
{
   public GameStatus GameStatus;
   public float CurrentTime;
   
   [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.CollapsedFoldout)]
   public Dictionary<Transform,CharacterVo> ActiveCharacterList = new Dictionary<Transform, CharacterVo>();
}

