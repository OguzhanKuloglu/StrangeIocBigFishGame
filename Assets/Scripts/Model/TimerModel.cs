using System.Collections.Generic;
using Assets.Scripts.Data.Vo;
using Assets.Scripts.Enums;
using UnityEngine;


namespace Assets.Scripts.Model
{
    public class TimerModel : ITimerModel
    {
        private RD_TimerData _timerData;

        public RD_TimerData TimerData
        {
            get
            {
                if (_timerData == null)
                    OnPostConstruct();
                return _timerData;
            }
            set { }
        }

        [PostConstruct]
        public void OnPostConstruct()
        {
            _timerData = Resources.Load<RD_TimerData>("Data/TimerData");
        }

        public void SetTime(float value)
        {
            _timerData.Time = value;
        }

        public float GetTime()
        {
            return _timerData.Time;
        }
        public void SetLevelTime(float value)
        {
            _timerData.LevelTime = value;
        }

        public float GetLevelTime()
        {
            return _timerData.LevelTime;
        }
        public void Reset()
        {
            _timerData.Time = 0f;
            _timerData.LevelTime = 0f;
            _timerData.TimerStatus = TimerStatus.Blocked;
        }
    }

}
