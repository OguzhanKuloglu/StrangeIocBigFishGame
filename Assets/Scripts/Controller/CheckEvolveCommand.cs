using Assets.Scripts.Data.Vo;
using Assets.Scripts.Enums;
using Assets.Scripts.Model;
using DG.Tweening;
using strange.extensions.command.impl;
using UnityEngine;
using zModules.AudioManager.Enums;
using zModules.AudioManager.Signals;

namespace Assets.Scripts.Controller
{
    public class CheckEvolveCommand : Command
    {
        [Inject] public GameSignals GameSignals { get; set; }
        [Inject] public ScreenPanelSignals ScreenSignals { get; set; }
        [Inject] public IEvolveModel EvolveModel { get; set; }
        [Inject] public IGameModel GameModel { get; set; }
        [Inject] public IPlayerModel PlayerModel { get; set; }
        [Inject] public AudioSignals AudioSignals { get; set; }

        private float wait = 0;
        public override void Execute()
        {
            wait = 0;
            if(PlayerModel.PlayerData.Evolved)
             return;
                
            if (PlayerModel.PlayerData.ProccessValue >= 10)
            {
                bool nextEvolve = false;
                foreach (var obj in EvolveModel.EvolveData.List)
                {
                    if (nextEvolve)
                    {
                        PlayerModel.SetEvolve(obj.Key);
                        GameSignals.Evolve.Dispatch();
                        DOTween.To(() => wait, x => wait = x, 1, 0.5f).OnComplete(() => SkillPopup());
                        GameModel.SetStatus(GameStatus.Blocked);
                        GameSignals.StatusChange.Dispatch();
                        AudioSignals.Play.Dispatch(3,AudioTypes.Evolve);
                        //PlayerModel.PlayerData.Evolved = true;
                        return;
                    }
                    if (PlayerModel.GetCurrentLevel() >= obj.Value.MinLevel &&
                        PlayerModel.GetCurrentLevel() <= obj.Value.MaxLevel)
                    {
                        nextEvolve = true;
                    }
                }
            }
            

            if (PlayerModel.PlayerData.FistLoad)
            {
                return;
            }

            foreach (var obj in EvolveModel.EvolveData.List)
            {
                if (PlayerModel.GetCurrentLevel() >= obj.Value.MinLevel &&
                    PlayerModel.GetCurrentLevel() <= obj.Value.MaxLevel)
                {
                    PlayerModel.SetEvolve(obj.Key);
                    GameSignals.Evolve.Dispatch();
                    PlayerModel.PlayerData.FistLoad = true;
                    
                    return;
                }
            }
        }

        public void SkillPopup()
        {
            ScreenSignals.OpenPanel.Dispatch(new PanelVo()
            {
                Layer = 0,
                PanelName = GameScreen.SkillPopupEvolve.ToString()
            });
        }
    }
}