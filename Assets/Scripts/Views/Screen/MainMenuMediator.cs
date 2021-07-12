using Assets.Scripts.Data.Vo;
using Assets.Scripts.Enums;
using strange.extensions.mediation.impl;
using zModules.FirebaseAnalytics.Service;
using zModules.FirebaseRealtimeDatabase.Model;
using System;
using Assets.Scripts.Model;
using UnityEngine;
using zModules.AudioManager.Enums;
using zModules.AudioManager.Signals;

namespace Assets.Scripts.Views
{
    public class MainMenuMediator : Mediator
    {
        [Inject] public MainMenuView view{ get; set;}
        [Inject] public ScreenPanelSignals ScreenSignals{ get; set;}
        [Inject] public IFirebaseAnalyticsService FirebaseAnalyticsService { get; set; }
        
        [Inject] public IFirebaseDBModel FirebaseDBModel { get; set; }
        
        [Inject] public IPlayerModel PlayerModel { get; set; }
        [Inject] public AudioSignals AudioSignals{ get; set; }


        public override void OnRegister()
        {
            base.OnRegister();
            view.onPlay += OnPlay;
        }

        public override void OnRemove()
        {
            base.OnRemove();
            view.onPlay -= OnPlay;
        }

        public void OnPlay()
        {
            AudioSignals.Play.Dispatch(4,AudioTypes.ButtonClick);

#if UNITY_EDITOR
            FirebaseAnalyticsService.EventSessionStart("testUser", DateTime.Now.ToString(), "null", "Editor", Application.version.ToString());
#elif UNITY_IOS
            FirebaseAnalyticsService.EventSessionStart(FirebaseDBModel.GetUserId(), DateTime.Now.ToString(), "null", "IOS", Application.version.ToString());
#elif UNITY_ANDROID
            FirebaseAnalyticsService.EventSessionStart(FirebaseDBModel.GetUserId(),DateTime.Now.ToString(),"null","ANDROID",Application.version.ToString());        
#endif
            
            ScreenSignals.OpenPanel.Dispatch(new PanelVo()
            {
                Layer = 0,
                PanelName = GameScreen.LevelMap.ToString()
            });
         
        }
    }    
}

