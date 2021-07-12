using System.Collections.Generic;
using Assets.Scripts.Data.Vo;
using Sirenix.OdinInspector;
using UnityEngine;
using CameraType = Assets.Scripts.Enums.CameraType;

[CreateAssetMenu(menuName = "RoofGames/Data/Config/CameraData",order = 2)]
public class CD_CameraData : SerializedScriptableObject
{
  [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.CollapsedFoldout)]
  public Dictionary<CameraType, CameraVo> List = new Dictionary<CameraType, CameraVo>();
  public float DefaultZoom;
  public float FollowDump;

}

