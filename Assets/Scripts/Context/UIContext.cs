using Assets.Scripts.Mediators;
using Assets.Scripts.Model;
using Assets.Scripts.Views;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using UnityEngine;

namespace Assets.Scripts.Context
{
    public class UIContext : MVCSContext
    {
        public UIContext (MonoBehaviour view) : base(view)
        {
        }

        public UIContext (MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
        {
        }

        protected override void mapBindings()
        {
            injectionBinder.Bind<ScreenPanelSignals>().CrossContext().ToSingleton();
            // injectionBinder.Bind<IGameModel>().To<GameModel>().CrossContext().ToSingleton();
            // mediationBinder.Bind<TesterView>().To<TesterMediator>();
            //commandBinder.Bind(GameSignals.GameStart).To<TestCommand>();
            injectionBinder.Bind<IInputModel>().To<InputModel>().CrossContext().ToSingleton();
            mediationBinder.Bind<ScreenManagerView>().To<ScreenManagerMediator>();
            mediationBinder.Bind<GamePlayView>().To<GamePlayMediator>();
            mediationBinder.Bind<LevelFailView>().To<LevelFailMediator>();
            mediationBinder.Bind<LevelSuccessView>().To<LevelSuccessMediator>();
            mediationBinder.Bind<SplashView>().To<SplashMediator>();
            mediationBinder.Bind<MainMenuView>().To<MainMenuMediator>();
            mediationBinder.Bind<SkillPopupView>().To<SkillPopupMediator>();
            mediationBinder.Bind<SettingsView>().To<SettingsViewMediator>();
            mediationBinder.Bind<PausePopupView>().To<PausePopupMediator>();
            mediationBinder.Bind<LevelMapManager>().To<LevelMapMediator>();
            mediationBinder.Bind<LeaderboardView>().To<LeaderBoardMediator>();

        }

        public override void Launch()
        {
            base.Launch();
        }
    }
 
}
