using System.Collections.Generic;
using Assets.Scripts.Data.Vo;
using Assets.Scripts.Enums;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "RoofGames/Data/Config/EnemyData",order = 6)]
public class CD_EnemyData : SerializedScriptableObject
{
   [ListDrawerSettings(ShowIndexLabels = true, ListElementLabelName = "ChracterType")]
   public List<CharacterVo> List = new List<CharacterVo>();
}

