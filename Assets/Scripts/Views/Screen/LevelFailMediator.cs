using Assets.Scripts.Model;
using Assets.Scripts.Data.Vo;
using Assets.Scripts.Enums;
using strange.extensions.mediation.impl;
using zModules.AudioManager.Enums;
using zModules.AudioManager.Signals;
using UnityEngine;

namespace Assets.Scripts.Views
{
    public class LevelFailMediator : Mediator
    {
        [Inject] public LevelFailView view { get; set; }
        
        [Inject] public GameSignals GameSignals { get; set; }
        [Inject] public ScreenPanelSignals ScreenSignals { get; set; }
        [Inject] public AudioSignals AudioSignals { get; set; }
        [Inject] public IGameModel GameModel { get; set; }
        [Inject] public ILeaderBoard LeaderBoard { get; set; }
        

        public override void OnRegister()
        {
            base.OnRegister();
            view.onRestartButton += RestartGame;
            view.onRewardedButton += ContunuiWithRewarded;
            view.onBackToHomeButton += BackToHome;

            GameSignals.RewardedResult.AddListener(RewardedResultShow);
        }

        public override void OnRemove()
        {
            base.OnRemove();
            view.onRestartButton -= RestartGame;
            view.onRewardedButton -= ContunuiWithRewarded;
            view.onBackToHomeButton -= BackToHome;
            GameSignals.RewardedResult.RemoveListener(RewardedResultShow);
        }

        public void ContunuiWithRewarded()
        {
            GameSignals.ShowRewarded.Dispatch();
        }

        public void RestartGame()
        {
            LeaderBoard.Reset();
            GameSignals.ResetData.Dispatch(); 
            AudioSignals.Play.Dispatch(4,AudioTypes.ButtonClick);
            ScreenSignals.OpenPanel.Dispatch(new PanelVo()
            {
                Layer = 0,
                PanelName = GameScreen.SkillPopup.ToString()
            });
            view.ResetCounter();

        }

        
        public void RewardedResultShow(bool result)
        {
            
            Debug.Log("rewarded closed with : " + result);

            if (result)
            {

                GameSignals.AfterRewardedSetFeedTrigger.Dispatch();
                AudioSignals.Play.Dispatch(4, AudioTypes.ButtonClick);
                ScreenSignals.OpenPanel.Dispatch(new PanelVo()
                {
                    Layer = 0,
                    PanelName = GameScreen.GamePlay.ToString()
                });
                GameModel.SetStatus(GameStatus.Play);
                GameSignals.StatusChange.Dispatch();
                Debug.Log("rewarded closed with : " + result);
                
            }
        }

        public void BackToHome()
        {
            AudioSignals.Play.Dispatch(4, AudioTypes.ButtonClick);
            ScreenSignals.OpenPanel.Dispatch(new PanelVo()
            {
                Layer = 0,
                PanelName = GameScreen.LevelMap.ToString()
            });
            GameModel.SetStatus(GameStatus.UIScreen);
            GameSignals.StatusChange.Dispatch();
        }
    }
}