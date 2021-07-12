
using System.Collections.Generic;
using Assets.Scripts.Data.Vo;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Model
{
    public interface IPlayerModel
    {
        RD_PlayerData PlayerData { get; set; }

        void AddCurrentLevel();
        int GetCurrentLevel();
        void SetPlayingCurretLevel(int value);
        int GetPlayingCurretLevel();
        void SetCurrentLevel(int value);
        void AddCurrentScore(int value);
        int GetCurrentScore();
        void SetSpeed(float value = 1);
        float GetSpeed();
        float GetRotateSensivity();
        void SetProccess(float value);
        void SetScale(float value);
        float GetScale();
        void SetCurrentSkill(SkillVo vo);
        List<SkillVo> GetCurrentSkill();
        void SetEvolve(EvolveType type);
        EvolveType GetEvolve();
        void BoostSpeed();
        void UnBoostSpeed();
        void Reset();
        bool GetHapticValue();
        bool GetMusicValue();
        bool GetSFXValue();
        void SetHapticValue(bool value);
        void SetMusicValue(bool value);
        void SetSFXValue(bool value);

    }   
}
