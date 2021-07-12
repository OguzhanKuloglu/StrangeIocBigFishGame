using Assets.Scripts.Data.Vo;
using Assets.Scripts.Enums;
using Assets.Scripts.Model;
using strange.extensions.command.impl;
using UnityEngine;
using DG.Tweening;
using zModules.AudioManager.Enums;
using zModules.AudioManager.Signals;

namespace Assets.Scripts.Controller
{
    public class StartCommand : Command
    {
        [Inject] public ScreenPanelSignals ScreenSignals { get; set; }
        [Inject] public GameSignals GameSignals { get; set; }
        [Inject] public AudioSignals AudioSignals { get; set; }

        public override void Execute()
        {
            AudioSignals.Play.Dispatch(0,AudioTypes.Music);
            AudioSignals.Play.Dispatch(1,AudioTypes.MusicEffect);

            ScreenSignals.OpenPanel.Dispatch(new PanelVo()
            {
                Layer = 0,
                PanelName = GameScreen.SplashRoofStacks.ToString()
            });

            float progressValue = 0;
            DOTween.To(() => progressValue, x => progressValue = x, 1, 3f).OnComplete(() => ToGameSplash());

            GameSignals.ResetData.Dispatch();
            GameSignals.CheckEvolve.Dispatch();
        }

        public void ToGameSplash()
        {
            ScreenSignals.OpenPanel.Dispatch(new PanelVo()
            {
                Layer = 0,
                PanelName = GameScreen.SplashScreen.ToString()
            });

        }
    }
}