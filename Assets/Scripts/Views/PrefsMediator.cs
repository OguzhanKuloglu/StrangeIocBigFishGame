using Assets.Scripts.Model;
using Assets.Scripts.Views;
using strange.extensions.mediation.impl;
using UnityEngine;
using zModules.FirebaseAnalytics.Service;
using System;
using zModules.FirebaseRealtimeDatabase.Model;


public class PrefsMediator : Mediator
{
    [Inject] public PrefsManager View         { get; set; }
    [Inject] public IGameModel   GameModel    { get; set; }
    [Inject] public IPlayerModel PlayerModel  { get; set; }
    [Inject] public IFirebaseAnalyticsService FirebaseAnalyticsService { get; set; }
    [Inject] public IFirebaseDBModel FirebaseDBModel { get; set; }
    [Inject] public GameSignals GameSignals { get; set; }


    public override void OnRegister()
    {
        base.OnRegister();
        View.onPrefsSave += GetUserData;
        View.onPrefsLoad += SetUserData;
        View.OnQuitSendEvent += OnSessionEnd;

        GameSignals.LoadUserNameGM.AddListener(LoadUserName);

    }

    public override void OnRemove()
    {
        base.OnRemove();
        View.onPrefsSave -= GetUserData;
        View.onPrefsLoad -= SetUserData;
        View.OnQuitSendEvent -= OnSessionEnd;

        GameSignals.LoadUserNameGM.RemoveListener(LoadUserName);
    }

    private string NewuserName; 
    public void LoadUserName(string UserName)
    {
        if (string.IsNullOrEmpty(UserName))
        {
            Debug.Log("game center user name empty cretad new guest");
            NewuserName = "Guest" + FirebaseDBModel.GetUserId();
            View.LoadUserName(NewuserName,FirebaseDBModel.GetUserId());
        }
        else
        {
            Debug.Log("game center user not empty");
            NewuserName = "Guest" + FirebaseDBModel.GetUserId();
            View.LoadUserName(UserName, FirebaseDBModel.GetUserId());
        }
    }

    private void OnSessionEnd()
    {
#if UNITY_EDITOR
        FirebaseAnalyticsService.EventSessionEnd("testUser", DateTime.Now.ToString(), "null");

#else
        FirebaseAnalyticsService.EventSessionEnd(FirebaseDBModel.GetUserId(), DateTime.Now.ToString(), "null");
#endif

    }

    private void GetUserData()
    {
       // View.SetPrefsData(PlayerModel.GetCurrentLevel(), PlayerModel.GetMusicValue(), PlayerModel.GetHapticValue());
    }

  
    private void SetUserData(int levelNo,bool music, bool haptic)
    {
        PlayerModel.SetCurrentLevel(levelNo);
        PlayerModel.SetMusicValue(music);
        PlayerModel.SetHapticValue(haptic);
    }
    /*
    public bool GetHaptic()
    {
       return  View.GetHaptic();
    }

    public bool GetMusic()
    {
        return View.GetMusic();
    }
    */
}
