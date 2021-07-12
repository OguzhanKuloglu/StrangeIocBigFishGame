using Assets.Scripts.Model;
using strange.extensions.command.impl;
using UnityEngine;

namespace Assets.Scripts.Controller
{
    public class FeedCommand : Command
    {
        [Inject] public GameSignals GameSignals { get; set; }
        [Inject] public IPlayerModel PlayerModel { get; set; }
        [Inject] public FishMealIdentity fi { get; set; }
        public override void Execute()
        {
            if (PlayerModel.PlayerData.ProccessValue == 0 && fi.Vo.EarningProcess < 0)
                return;
            PlayerModel.PlayerData.ProccessValue += fi.Vo.EarningProcess;
            GameSignals.CheckEvolve.Dispatch();
        }
    }
}