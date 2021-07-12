using Assets.Scripts.Data.Vo;
using Assets.Scripts.Enums;
using Assets.Scripts.Model;
using strange.extensions.mediation.impl;
using UnityEditor;

namespace Assets.Scripts.Views
{
    public class SplashMediator : Mediator
    {
        [Inject] public SplashView view{ get; set;}
        
        [Inject] public ScreenPanelSignals ScreenSignals{ get; set;}
        [Inject] public IGameModel GameModel{ get; set;}


        public override void OnRegister()
        {
            base.OnRegister();
            view.onCompleted += OnCompleted;
        }

        public override void OnRemove()
        {
            base.OnRemove();
            view.onCompleted -= OnCompleted;

        }
        private void OnCompleted()
        {
            ScreenSignals.OpenPanel.Dispatch(new PanelVo()
            {
                Layer = 0,
                PanelName = GameScreen.MainMenu.ToString()
            });
            GameModel.SetStatus(GameStatus.UIScreen);

        }
        
    }    
}

