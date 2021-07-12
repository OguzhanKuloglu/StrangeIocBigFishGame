using System.Collections.Generic;
using Assets.Scripts.Data.Vo;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Model
{
 
    public class PlayerModel : IPlayerModel
    {
        private RD_PlayerData _playerData;

        public RD_PlayerData PlayerData
        {
            get
            {
                if (_playerData == null)
                    OnPostConstruct();
                return _playerData;
            }
            set { }
        }

        [PostConstruct]
        public void OnPostConstruct()
        {
            _playerData = Resources.Load<RD_PlayerData>("Data/PlayerData");
            _playerData.CurrentSpeed = _playerData.DefaultSpeed;
        }

        #region Func

        public void AddCurrentLevel()
        {
            _playerData.CurrentLevel += 1;
        }
        public int GetCurrentLevel()
        {
            return _playerData.CurrentLevel;
        }

        public void SetCurrentLevel(int value)
        {
            _playerData.CurrentLevel = value;
        }

        public void AddCurrentScore(int value)
        {
            _playerData.CurrentScore += value;
        }
        
        public int GetCurrentScore()
        {
            return _playerData.CurrentScore;
        }
        
        public void SetSpeed(float value = 1)
        {
            _playerData.CurrentSpeed -= value * _playerData.SpeedFactor;
        }

        public void BoostSpeed()
        {
            _playerData.CurrentSpeed = _playerData.CurrentSpeed * 2;
        }

        public void UnBoostSpeed()
        {
            _playerData.CurrentSpeed = _playerData.CurrentSpeed / 2;
        }

        public float GetSpeed()
        {
            return _playerData.CurrentSpeed;
        }
        
        public float GetRotateSensivity()
        {
            return _playerData.RotateSensivity;
        }
        
        public void SetProccess(float value)
        {
            _playerData.ProccessValue = value;
        }
        
        public void SetScale(float value)
        {
            _playerData.Scale += value;
        }
        public float GetScale()
        {
            return _playerData.Scale;
        }
        
        public void SetCurrentSkill(SkillVo vo)
        {
            _playerData.CurrentSkills.Add(vo);
        }
        public List<SkillVo> GetCurrentSkill()
        {
            return _playerData.CurrentSkills;
        }
        
        public void SetEvolve(EvolveType type)
        {
            _playerData.EvolveType = type;
        }
        public EvolveType GetEvolve()
        {
            return _playerData.EvolveType;
        }


        public void Reset()
        {
            _playerData.CurrentScore = 0;
            _playerData.CurrentSpeed = _playerData.DefaultSpeed;
            _playerData.ProccessValue = 0;
            _playerData.Scale = 1;
            _playerData.CurrentSkills = new List<SkillVo>();
            _playerData.FistLoad = false;
            _playerData.Evolved = false;
        }

        public bool GetMusicValue()
        {
            return _playerData.Music;
        }
        public bool GetSFXValue()
        {
            return _playerData.SFX;
        }
        public void SetMusicValue(bool value)
        {
            _playerData.Music = value;
        }
        public void SetSFXValue(bool value)
        {
            _playerData.SFX = value;
        }
        public bool GetHapticValue()
        {
            return _playerData.Haptic;
        }

        public void SetHapticValue(bool value)
        {
            _playerData.Haptic = value;
        }

        public void SetPlayingCurretLevel(int levelNo)
        {
            _playerData.CurrentPlayingLevel = levelNo;
        }


        public int GetPlayingCurretLevel()
        {
            return _playerData.CurrentPlayingLevel;
        }

        #endregion
    }   
}
