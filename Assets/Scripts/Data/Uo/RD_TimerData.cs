using System.Collections.Generic;
using Assets.Scripts.Data.Vo;
using Assets.Scripts.Enums;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "RoofGames/Data/Runtime/TimerData", order = 7)]
public class RD_TimerData : SerializedScriptableObject
{
    public float Time;
    public float LevelTime;
    public TimerStatus TimerStatus;

}



