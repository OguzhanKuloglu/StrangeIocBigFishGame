using Assets.Scripts.Enums;
using Assets.Scripts.Model;
using strange.extensions.mediation.impl;
using UnityEngine;
using DG.Tweening;
using Assets.Scripts.Data.Vo;
using zModules.AudioManager.Enums;
using zModules.AudioManager.Signals;

namespace Assets.Scripts.Views
{
    public class SettingsViewMediator : Mediator
    {
        [Inject] public SettingsView View { get; set; }
        [Inject] public ScreenPanelSignals ScreenSignals { get; set; }
        [Inject] public AudioSignals AudioSignals { get; set; }
        [Inject] public IPlayerModel PlayerModel { get; set; }
        

        public override void OnRegister()
        {
            base.OnRegister();
            View.onPopupCloseButton += ToPausePopup;
            View.onGameHapticButton += OnGameHapticButton;
            View.onGameMusicButton += OnGameMusicButton;

        }

        public override void OnRemove()
        {
            base.OnRemove();
            View.onPopupCloseButton -= ToPausePopup;
            View.onGameHapticButton -= OnGameHapticButton;
            View.onGameMusicButton -= OnGameMusicButton;
        }

        private void OnGameMusicButton(bool value)
        {
            PlayerModel.SetMusicValue(!value);
            PlayerModel.SetSFXValue(!value);
            AudioSignals.MuteMusic.Dispatch(value);
            AudioSignals.MuteSfx.Dispatch(value);
        }

        private void OnGameHapticButton(bool value)
        {
            PlayerModel.SetHapticValue(!value);
        }


        public void ToPausePopup()
        {
            AudioSignals.Play.Dispatch(4,AudioTypes.ButtonClick);
            ScreenSignals.OpenPanel.Dispatch(new PanelVo()
            {
                Layer = 0,
                PanelName = GameScreen.PausePopup.ToString()
            });
        }

    }
}