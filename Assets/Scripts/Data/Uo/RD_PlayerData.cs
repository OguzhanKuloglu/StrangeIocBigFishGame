using System.Collections.Generic;
using Assets.Scripts.Data.Vo;
using Assets.Scripts.Enums;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "RoofGames/Data/Runtime/PlayerData",order = 1)]
public class RD_PlayerData : SerializedScriptableObject
{
    public int CurrentLevel;
    public int CurrentScore;
    public int CurrentPlayingLevel;
    public float DefaultSpeed;
    public float CurrentSpeed;
    public float SpeedFactor;
    public float RotateSensivity;
    public CharacterType ChracterType;
    [ProgressBar(0,10)]
    public float ProccessValue;
    public float Scale;
    public List<SkillVo> CurrentSkills;
    public EvolveType EvolveType;
    public bool FistLoad;
    public bool TutorialCompleted;
    public bool SkillTutorialCompleted;
    public bool Evolved;
    
    
    [Title("Settings")] 
    public bool Music;
    public bool SFX;
    public bool Haptic;
    public bool NoAds;

}
