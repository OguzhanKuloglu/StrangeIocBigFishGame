using Assets.Scripts.Data.Vo;
using Assets.Scripts.Enums;
using Assets.Scripts.Model;
using Sirenix.OdinInspector;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Assets.Scripts.Views
{
    public class TimerMediator : Mediator
    {
        [Inject] public TimerManager TimerManager  { get; set; }
        [Inject] public GameSignals GameSignals { get; set; }
        
        [Inject] public ScreenPanelSignals ScreenSignals { get; set; }
        [Inject] public ILevelModel levelModel { get; set; }
        [Inject] public IGameModel GameModel { get; set; }

        [Inject] public IPlayerModel PlayerModel { get; set; }
        [Inject] public ITimerModel TimerModel{ get; set; }

        public override void OnRegister()
        {
            base.OnRegister();
            TimerManager.onCompletedTime += StopLevel;
            TimerManager.onChangedTime += TimeUpdated;
            GameSignals.LevelStarted.AddListener(StartTimer);
            GameSignals.StopTime.AddListener(StopTimer);
            GameSignals.ContineuTime.AddListener(ContinueTimer);
            GameSignals.StatusChange.AddListener(StatusChange);
        }

        public override void OnRemove()
        {
            base.OnRemove();
            TimerManager.onCompletedTime -= StopLevel;
            TimerManager.onChangedTime   -= TimeUpdated;
            GameSignals.LevelStarted.RemoveListener(StartTimer);
            GameSignals.StopTime.RemoveListener(StopTimer);
            GameSignals.ContineuTime.RemoveListener(ContinueTimer);
            GameSignals.StatusChange.RemoveListener(StatusChange);
        }

        private void StatusChange()
        {
            if(GameModel.GetStatus() == GameStatus.Play)
                ContinueTimer();
            else
                StopTimer();
        }
        [Button]
        public void StartTimer()
        {
            TimerManager.StartTimer(true,levelModel.GetTime(PlayerModel.GetCurrentLevel()));
            TimerModel.SetLevelTime(levelModel.GetTime(PlayerModel.GetCurrentLevel()));
        }

        [Button]
        public void StopTimer()
        {
            TimerManager.PauseTimer();
        }

        [Button]
        public void ContinueTimer()
        {
            TimerManager.ContinueTimer(TimerModel.GetTime());
        }

        public void StopLevel()
        {
            Debug.Log("Stop level");
            GameSignals.LevelFinish.Dispatch();
        }

        public void TimeUpdated(float value)
        {
            TimerModel.SetTime(value);
            GameSignals.ChangeTime.Dispatch();
        }
    }
}