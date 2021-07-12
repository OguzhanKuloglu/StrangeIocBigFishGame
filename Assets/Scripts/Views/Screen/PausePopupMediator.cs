using Assets.Scripts.Enums;
using Assets.Scripts.Model;
using strange.extensions.mediation.impl;
using Assets.Scripts.Data.Vo;
using zModules.AudioManager.Enums;
using zModules.AudioManager.Signals;

namespace Assets.Scripts.Views
{
    public class PausePopupMediator : Mediator
    {
        [Inject] public PausePopupView View { get; set; }
        [Inject] public GameSignals GameSignals { get; set; }
        [Inject] public ScreenPanelSignals ScreenSignals { get; set; }
        [Inject] public AudioSignals AudioSignals { get; set; }
        [Inject] public IGameModel GameModel { get; set; }

        public override void OnRegister()
        {
            base.OnRegister();
            View.onGameRestartButton += ToRestartGame;
            View.onGameReturnButton  += ToUnPauseGame;
            View.onReturnHomeButton  += ToLevelMap;
            View.onSettingMenuButton += ToSettings;
        }

        public override void OnRemove()
        {
            base.OnRemove();
            View.onGameRestartButton -= ToRestartGame;
            View.onGameReturnButton -= ToUnPauseGame;
            View.onReturnHomeButton -= ToLevelMap;
            View.onSettingMenuButton -= ToSettings;
        }

        public void ToRestartGame()
        {
            GameSignals.ResetData.Dispatch(); 
            AudioSignals.Play.Dispatch(4,AudioTypes.ButtonClick);
            GameSignals.CreateFakeLevel.Dispatch();
            ScreenSignals.OpenPanel.Dispatch(new PanelVo()
            {
                Layer = 0,
                PanelName = GameScreen.SkillPopup.ToString()
            });
            
            GameModel.SetStatus(GameStatus.UIScreen);
            GameSignals.StatusChange.Dispatch();
            
        }

        public void ToUnPauseGame()
        {
            GameSignals.LevelRestart.Dispatch();
            AudioSignals.Play.Dispatch(4,AudioTypes.ButtonClick);
            ScreenSignals.OpenPanel.Dispatch(new PanelVo()
            {
                Layer = 0,
                PanelName = GameScreen.GamePlay.ToString()
            });
            GameModel.SetStatus(GameStatus.Play);
            GameSignals.StatusChange.Dispatch();
        }

        public void ToLevelMap()
        {
            AudioSignals.Play.Dispatch(4,AudioTypes.ButtonClick);
            ScreenSignals.OpenPanel.Dispatch(new PanelVo()
            {
                Layer = 0,
                PanelName = GameScreen.LevelMap.ToString()
            });
            GameModel.SetStatus(GameStatus.UIScreen);
            GameSignals.StatusChange.Dispatch();
        }

        public void ToSettings()
        {
            AudioSignals.Play.Dispatch(4,AudioTypes.ButtonClick);
            ScreenSignals.OpenPanel.Dispatch(new PanelVo()
            {
                Layer = 0,
                PanelName = GameScreen.SettingsPopup.ToString()
            });
            
        }


    }
}