using System.Collections.Generic;
using Assets.Scripts.Data.Vo;
using Assets.Scripts.Enums;


namespace Assets.Scripts.Model
{
    public interface ITimerModel
    {
        RD_TimerData TimerData { get; set; }

        void SetTime(float value);
        float GetTime();
        float GetLevelTime();
        void SetLevelTime(float value);
        void Reset();
    }
}

