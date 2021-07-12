using Assets.Scripts.Model;
using Assets.Scripts.Controller;
using Assets.Scripts.Views;
using zModules.FirebaseRealtimeDatabase.Model;
using zModules.FirebaseRealtimeDatabase.Views;
using zModules.FirebaseAnalytics.Service;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using UnityEngine;
using zModules.AudioManager.Signals;
using zModules.AudioManager.Views;
using zModules.SaveSystemModule.Services;

namespace Assets.Scripts.Context
{
    public class GameContext : MVCSContext
    {
        private GameSignals _gameSignals;
        private FirebaseAnalyticsSignals _firebaseAnalyticSignals;
        private FirebaseDBSignals _dBSignals;
        private AudioSignals _audioSignals;
        private ISaveSystemService _saveService;

        public GameContext (MonoBehaviour view) : base(view)
        {
        }

        public GameContext (MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
        {
        }

        protected override void mapBindings()
        {
         
            //*** Signals injection bind
            injectionBinder.Bind<GameSignals>().CrossContext().ToSingleton();
            _gameSignals = injectionBinder.GetInstance<GameSignals>();
            injectionBinder.Bind<FirebaseAnalyticsSignals>().CrossContext().ToSingleton();
            _firebaseAnalyticSignals = injectionBinder.GetInstance<FirebaseAnalyticsSignals>();
            injectionBinder.Bind<FirebaseDBSignals>().CrossContext().ToSingleton();
            _dBSignals = injectionBinder.GetInstance<FirebaseDBSignals>();
            injectionBinder.Bind<AudioSignals>().CrossContext().ToSingleton();
            _audioSignals = injectionBinder.GetInstance<AudioSignals>();

            //*** Data injection bind
            injectionBinder.Bind<IGameModel>().To<GameModel>().CrossContext().ToSingleton();
            injectionBinder.Bind<IPlayerModel>().To<PlayerModel>().CrossContext().ToSingleton();
            injectionBinder.Bind<IEvolveModel>().To<EvolveModel>().CrossContext().ToSingleton();
            injectionBinder.Bind<ISkillModel>().To<SkillModel>().CrossContext().ToSingleton();
            
            injectionBinder.Bind<ICameraModel>().To<CameraModel>().CrossContext().ToSingleton();
            injectionBinder.Bind<ILevelModel>().To<LevelModel>().CrossContext().ToSingleton();
            injectionBinder.Bind<IScaleModel>().To<ScaleModel>().CrossContext().ToSingleton();
            injectionBinder.Bind<IFeedModel>().To<FeedModel>().CrossContext().ToSingleton();
            injectionBinder.Bind<IEnemyModel>().To<EnemyModel>().CrossContext().ToSingleton();
            injectionBinder.Bind<ITimerModel>().To<TimerModel>().CrossContext().ToSingleton();
            injectionBinder.Bind<IIndicatorModel>().To<IndicatorModel>().CrossContext().ToSingleton();
            injectionBinder.Bind<ILeaderBoard>().To<LeaderBoardModel>().CrossContext().ToSingleton();
            injectionBinder.Bind<IPlayerPoint>().To<PlayerPointModel>().CrossContext().ToSingleton();
            injectionBinder.Bind<IFirebaseDBModel>().To<FirebaseDBModel>().CrossContext().ToSingleton();
            injectionBinder.Bind<ILevelStatusModel>().To<LevelStatusModel>().CrossContext().ToSingleton();


            mediationBinder.Bind<LevelManager>().To<LevelManagerMediator>();
            mediationBinder.Bind<PlayerManager>().To<PlayerMediator>();
            mediationBinder.Bind<CameraManager>().To<CameraMediator>();
            mediationBinder.Bind<EnemyView>().To<EnemyMediator>();
            mediationBinder.Bind<TestView>().To<TestMediator>();
            mediationBinder.Bind<TimerManager>().To<TimerMediator>();
            mediationBinder.Bind<IndicatorView>().To<IndicatorViewMediator>();
            mediationBinder.Bind<IndicatorManager>().To<IndicatorMediator>();
            mediationBinder.Bind<PrefsManager>().To<PrefsMediator>();
            mediationBinder.Bind<GameCenterView>().To<GameCenterMediator>();
            mediationBinder.Bind<FirebaseDBManager>().To<FirebaseDBMediator>();
            mediationBinder.Bind<AudioManagerView>().To<AudioManagerMediator>();
            mediationBinder.Bind<AdmobManager>().To<AdsMediator>();


            //*** Service injection bind
            injectionBinder.Bind<IFirebaseAnalyticsService>().To<FirebaseAnalyticsService>().CrossContext().ToSingleton();
            injectionBinder.Bind<ISaveSystemService>().To<SaveSystemService>().CrossContext().ToSingleton();
            _saveService = injectionBinder.GetInstance<ISaveSystemService>();

            //*** Command bind
            commandBinder.Bind(_gameSignals.GameStarted).To<StartCommand>();
            commandBinder.Bind(_gameSignals.ResetData).To<ResetDataCommand>();
            commandBinder.Bind(_gameSignals.CheckEvolve).To<CheckEvolveCommand>();
            commandBinder.Bind(_gameSignals.Feed).To<FeedCommand>();
            commandBinder.Bind(_gameSignals.StartSpeedSkill).To<SpeedSkillCommand>();
            commandBinder.Bind(_gameSignals.StartElectricSkill).To<ElectricSkillComannd>();
            commandBinder.Bind(_gameSignals.StartIncSkill).To<IncSkillCommand>();
            commandBinder.Bind(_gameSignals.StartUnicornSkill).To<UnicornSkillCommand>();
            commandBinder.Bind(_gameSignals.DestroyEnemy).To<CheckEnemyCountCommand>();
            commandBinder.Bind(_gameSignals.LevelFinish).To<CheckLevelFinishCommand>();
            
            
       
         
        }

        public override void Launch()
        {
            base.Launch();
            Application.targetFrameRate = 60;
            _gameSignals.GameStarted.Dispatch();

        }
        
        public void SaveData()
        {
            _saveService.SaveData();
        }
        public void LoadData()
        {
            _saveService.LoadData();
        }
    }
 
}
