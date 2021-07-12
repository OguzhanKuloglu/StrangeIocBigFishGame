using Assets.Scripts.Model;
using Assets.Scripts.Views;
using Assets.Scripts.Enums;
using strange.extensions.mediation.impl;
using UnityEngine;
using zModules.FirebaseAnalytics.Service;
using System;
using Assets.Scripts.Data.Vo;
using zModules.FirebaseRealtimeDatabase.Model;
using zModules.FirebaseRealtimeDatabase.Constant;
using zModules.FirebaseRealtimeDatabase.Data.Vo;
using System.Collections.Generic;
using zModules.AudioManager.Enums;
using zModules.AudioManager.Signals;

public class LeaderBoardMediator : Mediator
{
    [Inject] public LeaderboardView View { get; set; }
    [Inject] public FirebaseDBSignals FirebaseDBSignals { get; set; }
    [Inject] public ScreenPanelSignals ScreenSignals { get; set; }
    [Inject] public AudioSignals AudioSignals { get; set; }
    [Inject] public IFirebaseDBModel FirebaseDBModel { get; set; }


    public override void OnRegister()
    {
        base.OnRegister();
        View.onCloseLeaderBoard += ClosePopup;
        View.onLoadDb += LoadFirebaseDb;
        FirebaseDBSignals.DataLoaded.AddListener(LoadDataToPopup);
    }

    public override void OnRemove()
    {
        base.OnRemove();
        View.onCloseLeaderBoard -= ClosePopup;
        View.onLoadDb -= LoadFirebaseDb;
        FirebaseDBSignals.DataLoaded.RemoveListener(LoadDataToPopup);
    }

    public void LoadFirebaseDb()
    {
        
        FirebaseDBSignals.GetData.Dispatch(new GetDataVo(){
            TableName = TableName.Leaderboard,
            OrderBy = Leaderboard_Columns.TotalPoint
        });
        
    }

    public void ClosePopup()
    {
        AudioSignals.Play.Dispatch(4,AudioTypes.ButtonClick);
        ScreenSignals.ClearPanel.Dispatch(1);
    }

    public void LoadDataToPopup(Dictionary<string, List<DataResultVo>> value)
    {
        View.LoadBoard(value,FirebaseDBModel.GetUserId());
    }
}