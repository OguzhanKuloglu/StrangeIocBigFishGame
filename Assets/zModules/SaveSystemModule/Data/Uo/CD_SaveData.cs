using System.Collections.Generic;
using Assets.Scripts.Data.Vo;
using Assets.Scripts.Enums;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "SaveSystem/Data/SaveData", order = 0)]
public class CD_SaveData : SerializedScriptableObject
{
    public RD_PlayerData PlayerData;
    public RD_LevelStatusData LevelStatusData;
    public CD_FirebaseDBData FirebaseDBData;
    
}



