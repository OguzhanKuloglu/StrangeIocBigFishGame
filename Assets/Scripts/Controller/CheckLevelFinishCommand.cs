using Assets.Scripts.Data.Vo;
using Assets.Scripts.Enums;
using Assets.Scripts.Model;
using Assets.Scripts.Views;
using strange.extensions.command.impl;
using UnityEngine;
using zModules.FirebaseAnalytics.Service;
using System;
using zModules.AudioManager.Enums;
using zModules.AudioManager.Signals;
using zModules.FirebaseRealtimeDatabase.Model;

namespace Assets.Scripts.Controller
{
    public class CheckLevelFinishCommand : Command
    {
        [Inject] public GameSignals GameSignals { get; set; }
        [Inject] public ScreenPanelSignals ScreenSignals{ get; set; }
        [Inject] public IPlayerModel PlayerModel{ get; set; }
        [Inject] public ILeaderBoard LeaderBoard { get; set; }
        [Inject] public IGameModel GameModel { get; set; }
        [Inject] public IFirebaseDBModel FirebaseDBModel { get; set; }
        [Inject] public IFirebaseAnalyticsService FirebaseAnalyticsService { get; set; }
        [Inject] public AudioSignals AudioSignals { get; set; }


        private int InterstitialCounter = 0; 

        public override void Execute()
        {
            InterstitialCounter++;
            if (GameModel.GameData.ActiveCharacterList.Count > 3)
            {
#if UNITY_EDITOR
                FirebaseAnalyticsService.EventLevelFailed("testUser", PlayerModel.GetCurrentLevel().ToString(), "null", "null", DateTime.Now.ToString(), "null", "null");
#endif
                FirebaseAnalyticsService.EventLevelFailed(FirebaseDBModel.GetUserName(), PlayerModel.GetCurrentLevel().ToString(),"null", "null",DateTime.Now.ToString(),"null","null");
                ScreenSignals.OpenPanel.Dispatch(new PanelVo()
                {
                    Layer = 0,
                    PanelName = GameScreen.LevelFail.ToString()
                });
                AudioSignals.Play.Dispatch(2,AudioTypes.LevelFail);
                GameModel.SetStatus(GameStatus.UIScreen);
                GameSignals.StatusChange.Dispatch();
                
                LeaderBoard.Reset();

                InterstitialCounter++;
                if (InterstitialCounter == 2)
                {
                    InterstitialCounter = 0;
                    GameSignals.ShowInterstitial.Dispatch();
                }
                
                

                return;
            }
            if (LeaderBoard.GetPlayerOrder() <= 3)
            {
                ScreenSignals.OpenPanel.Dispatch(new PanelVo()
                {
                    Layer = 0,
                    PanelName = GameScreen.LevelSuccess.ToString()
                });
                AudioSignals.Play.Dispatch(2,AudioTypes.LevelCompleted);
                GameModel.SetStatus(GameStatus.UIScreen);
                GameSignals.StatusChange.Dispatch();

                InterstitialCounter++;
                if (InterstitialCounter == 2)
                {
                    InterstitialCounter = 0;
                    GameSignals.ShowInterstitial.Dispatch();
                }
            }
            else
            {
                ScreenSignals.OpenPanel.Dispatch(new PanelVo()
                {
                    Layer = 1,
                    PanelName = GameScreen.LevelFail.ToString()
                });

                AudioSignals.Play.Dispatch(2,AudioTypes.LevelFail);
                GameModel.SetStatus(GameStatus.UIScreen);
                GameSignals.StatusChange.Dispatch();

                InterstitialCounter++;
                if (InterstitialCounter == 2)
                {
                    InterstitialCounter = 0;
                    GameSignals.ShowInterstitial.Dispatch();
                }

            }
        }
    }
}
