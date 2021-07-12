using System.Collections.Generic;
using Assets.Scripts.Data.Vo;
using Assets.Scripts.Enums;
using Assets.Scripts.Model;
using strange.extensions.mediation.impl;
using UnityEngine;
using zModules.FirebaseAnalytics.Service;
using zModules.FirebaseRealtimeDatabase.Model;
using System;
using zModules.AudioManager.Enums;
using zModules.AudioManager.Signals;
using zModules.FirebaseRealtimeDatabase.Constant;
using zModules.FirebaseRealtimeDatabase.Data.Vo;




namespace Assets.Scripts.Views
{
    public class LevelSuccessMediator : Mediator
    {
        [Inject] public LevelSuccessView view { get; set; }
        [Inject] public ScreenPanelSignals ScreenSignals{ get; set; }
        [Inject] public GameSignals GameSignals{ get; set; }
        [Inject] public IPlayerModel PlayerModel { get; set; }
        [Inject] public ILeaderBoard LeaderBoard{ get; set; }
        [Inject] public ITimerModel TimerModel{ get; set; }
        [Inject] public IPlayerPoint PlayerPoint { get; set; }
        [Inject] public ILevelModel LevelModel { get; set; }
        [Inject] public ILevelStatusModel LevelStatusModel{ get; set; }
        [Inject] public IFirebaseAnalyticsService FirebaseAnalyticsService { get; set; }
        [Inject] public IFirebaseDBModel FirebaseDBModel{ get; set; }
        [Inject] public FirebaseDBSignals FirebaseDBSignals{ get; set; }
        [Inject] public AudioSignals AudioSignals{ get; set; }
        [Inject] public IGameModel GameModel { get; set; }

        public override void OnRegister()
        {
            base.OnRegister();
            view.onRestartGame  += RestartGame;
            view.onContinueGame += ContineuGame;

            view.SetStars(3 - GameModel.GameData.ActiveCharacterList.Count);
            
            int feedPoint = (PlayerPoint.GetBonusFeedCount() * 10) + (PlayerPoint.GetNormalFeedCount() * 5);
            int timerPoint = (int)(TimerModel.GetTime()) * 20 - (int)(LevelModel.GetTime(PlayerModel.GetPlayingCurretLevel()));
            int totalPoint = feedPoint + timerPoint;
            view.SetTimer(totalPoint);
            PlayerModel.PlayerData.TutorialCompleted = true;
            PlayerModel.PlayerData.SkillTutorialCompleted = true;
        }

        public override void OnRemove()
        {
            base.OnRemove();
            view.onRestartGame  -= RestartGame;
            view.onContinueGame -= ContineuGame;
            
        }

        public void RestartGame()
        {
            AudioSignals.Play.Dispatch(4,AudioTypes.ButtonClick);
            GameSignals.ResetData.Dispatch();
            ScreenSignals.OpenPanel.Dispatch(new PanelVo()
            {
                Layer = 0,
                PanelName = GameScreen.SkillPopup.ToString()
            });
            
        }

        public void RestartLeaderBoard()
        {
            LeaderBoard.Reset();
        }

        //private const string levelPrefsName = "LevelData-";
        //private const string KeyUserName = "prefs-key-username";
        public void ContineuGame()
        {
            AudioSignals.Play.Dispatch(4,AudioTypes.ButtonClick);
            //string toSaveKey = levelPrefsName + PlayerModel.GetPlayingCurretLevel();
            int feedPoint = (PlayerPoint.GetBonusFeedCount() * 10) + (PlayerPoint.GetNormalFeedCount() * 5);
            int timerPoint = (int)(TimerModel.GetTime()) * 20 - (int)(LevelModel.GetTime(PlayerModel.GetPlayingCurretLevel()));
            int totalPoint = feedPoint + timerPoint;
            LevelDataVo dataVo = new LevelDataVo();

            //if (!ES3.KeyExists(toSaveKey))
            if(LevelStatusModel.GetLevelStatusCount() <= PlayerModel.GetPlayingCurretLevel())
            {
                
                dataVo.StarCount = 3 - LeaderBoard.GetPlayerOrder();
                dataVo.LevelPoint = totalPoint;
                dataVo.LeaderboardPoint += totalPoint;
                LevelStatusModel.AddLevelStatus(dataVo);
                LevelStatusModel.SetTotalPoint();
                Debug.Log("created new prefs on easy save for level " + PlayerModel.GetPlayingCurretLevel());
#if UNITY_EDITOR
                FirebaseAnalyticsService.EventLevelComplated("testUser", PlayerModel.GetCurrentLevel().ToString(), DateTime.Now.ToString(), LevelStatusModel.GetTotalPoint().ToString(), "null");
#endif
                FirebaseAnalyticsService.EventLevelComplated(FirebaseDBModel.GetUserId(), PlayerModel.GetCurrentLevel().ToString(), DateTime.Now.ToString(), LevelStatusModel.GetTotalPoint().ToString(), "null");

                Dictionary<string, object> data = new Dictionary<string, object>();
                data[Leaderboard_Columns.UserName] = FirebaseDBModel.GetUserName();
                data[Leaderboard_Columns.TotalPoint] = LevelStatusModel.GetTotalPoint();
                

                FirebaseDBSignals.SendData.Dispatch(new SenderVo()
                {
                    Data = data,
                    TableName = TableName.Leaderboard
                });
            }
            else
            {
                dataVo = LevelStatusModel.GetLevelStatus(PlayerModel.GetPlayingCurretLevel());

                if (totalPoint > dataVo.LevelPoint)
                {
                    LevelDataVo NewdataVo = new LevelDataVo();
                    NewdataVo.LeaderboardPoint -= dataVo.LevelPoint;
                    NewdataVo.LeaderboardPoint += totalPoint;
                    NewdataVo.StarCount = 3 - LeaderBoard.GetPlayerOrder();
                    NewdataVo.LevelPoint = totalPoint;
                    LevelStatusModel.SetLevelStatus(PlayerModel.GetPlayingCurretLevel(),NewdataVo);
                    LevelStatusModel.SetTotalPoint();
                    Debug.Log("using old prefs on easy save for level and update for more point" + PlayerModel.GetPlayingCurretLevel());
#if UNITY_EDITOR
                    FirebaseAnalyticsService.EventLevelComplated("testUser", PlayerModel.GetCurrentLevel().ToString(), DateTime.Now.ToString(), LevelStatusModel.GetTotalPoint().ToString(), "null");
#endif
                    FirebaseAnalyticsService.EventLevelComplated(FirebaseDBModel.GetUserId(), PlayerModel.GetCurrentLevel().ToString(), DateTime.Now.ToString(), LevelStatusModel.GetTotalPoint().ToString(), "null");

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data[Leaderboard_Columns.UserName] = FirebaseDBModel.GetUserName();
                    data[Leaderboard_Columns.TotalPoint] = LevelStatusModel.GetTotalPoint();


                    FirebaseDBSignals.SendData.Dispatch(new SenderVo()
                    {
                        Data = data,
                        TableName = TableName.Leaderboard
                    });
                }
                Debug.Log("using old prefs on easy save for level " + PlayerModel.GetPlayingCurretLevel());
            }

            

            if (PlayerModel.GetPlayingCurretLevel() == PlayerModel.GetCurrentLevel())
            {
                PlayerModel.AddCurrentLevel();
            }
            else
            {
               //playing old level
            }
            
            GameSignals.ResetData.Dispatch();
            ScreenSignals.OpenPanel.Dispatch(new PanelVo()
            {
                Layer = 0,
                PanelName = GameScreen.LevelMap.ToString()
            });
            PlayerModel.SetPlayingCurretLevel(PlayerModel.GetPlayingCurretLevel() + 1);
            RestartLeaderBoard();
        }
    }
}

