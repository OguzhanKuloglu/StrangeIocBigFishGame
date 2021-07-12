using Assets.Scripts.Model;
using Assets.Scripts.Controller;
using Assets.Scripts.Views;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using UnityEngine;

namespace Assets.Scripts.Context
{
    public class GameContext_TestOguzhan : MVCSContext
    {
        private GameSignals _gameSignals;
        public GameContext_TestOguzhan (MonoBehaviour view) : base(view)
        {
        }

        public GameContext_TestOguzhan (MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
        {
        }

        protected override void mapBindings()
        {
            //*** Signals injection bind
            injectionBinder.Bind<GameSignals>().CrossContext().ToSingleton();
            _gameSignals = injectionBinder.GetInstance<GameSignals>();
            
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


            mediationBinder.Bind<LevelManager>().To<LevelManagerMediator>();
            mediationBinder.Bind<PlayerManager>().To<PlayerMediator>();
            mediationBinder.Bind<CameraManager>().To<CameraMediator>();

            //*** Command bind
            commandBinder.Bind(_gameSignals.GameStarted).To<StartCommand>();
            commandBinder.Bind(_gameSignals.CheckEvolve).To<CheckEvolveCommand>();
            commandBinder.Bind(_gameSignals.Feed).To<FeedCommand>();
            commandBinder.Bind(_gameSignals.StartSpeedSkill).To<SpeedSkillCommand>();
            commandBinder.Bind(_gameSignals.StartElectricSkill).To<ElectricSkillComannd>();
            commandBinder.Bind(_gameSignals.StartIncSkill).To<IncSkillCommand>();
            commandBinder.Bind(_gameSignals.StartUnicornSkill).To<UnicornSkillCommand>();
            
            
            //**** new ****
            
            //*************
        }

        public override void Launch()
        {
            base.Launch();
            Application.targetFrameRate = 60;
            _gameSignals.GameStarted.Dispatch();
        }
    }
 
}
