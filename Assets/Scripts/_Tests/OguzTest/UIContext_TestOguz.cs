using Assets.Scripts.Mediators;
using Assets.Scripts.Model;
using Assets.Scripts.Views;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using UnityEngine;

namespace Assets.Scripts.Context
{
    public class UIContext_TestOguz : MVCSContext
    {
        public UIContext_TestOguz (MonoBehaviour view) : base(view)
        {
        }

        public UIContext_TestOguz (MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
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
            
            // **** new ***
            
            
            
        }

        public override void Launch()
        {
            base.Launch();
        }
    }
 
}
