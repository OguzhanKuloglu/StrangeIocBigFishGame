using Assets.Scripts.Data.Vo;
using Assets.Scripts.Extensions;
using Constans;
using DG.Tweening;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;



namespace Assets.Scripts.Views
{
    public class TimerManager : View
    {
        public UnityAction onCompletedTime;
        public UnityAction<float> onChangedTime;
        public float Duration;
        private float progressValue = 0;
        private float LevelTime = 0;
        private Tweener TweenTimer;

        public void StartTimer(bool restart, float levelTime)
        {
            if (restart)
            {
                progressValue = 0;
                LevelTime = levelTime;
                TweenTimer = DOTween.To(() => progressValue, x => progressValue = x, levelTime, levelTime).OnUpdate(() => onChangedTime?.Invoke(progressValue)).OnComplete(() => onCompletedTime?.Invoke());
            }
        
        }


        public void PauseTimer()
        {
            TweenTimer.Pause();
        }


        public void ContinueTimer(float value)
        {
            TweenTimer.Play();
        }

    }
}