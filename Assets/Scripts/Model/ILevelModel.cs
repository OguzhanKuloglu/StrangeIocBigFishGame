
using Assets.Scripts.Data.Vo;
using UnityEngine;

namespace Assets.Scripts.Model
{
    public interface ILevelModel
    {
        CD_LevelData LevelData { get; set; }
        LevelVo GetLevel(int CurrentLevel);
        float GetTime(int CurrentLevel);
        GameObject GetFakeLevel();
    }   
}
