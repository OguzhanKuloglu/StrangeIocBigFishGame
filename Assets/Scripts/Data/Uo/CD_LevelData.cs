using System.Collections.Generic;
using Assets.Scripts.Data.Vo;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "RoofGames/Data/Config/LevelData",order = 3)]
public class CD_LevelData : SerializedScriptableObject
{
   public GameObject FakeLevelPrefab;
   [ListDrawerSettings(ShowIndexLabels = true,ListElementLabelName = "Environment")]
   public List<LevelVo> List = new List<LevelVo>();
   public float FeedSpawnTime;
}

