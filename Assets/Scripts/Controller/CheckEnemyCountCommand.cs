using Assets.Scripts.Data.Vo;
using Assets.Scripts.Enums;
using Assets.Scripts.Model;
using Assets.Scripts.Views;
using strange.extensions.command.impl;
using UnityEngine;

namespace Assets.Scripts.Controller
{
    public class CheckEnemyCountCommand : Command
    {
        [Inject] public GameSignals GameSignals { get; set; }
        [Inject] public IGameModel GameModel { get; set; }
        

        public override void Execute()
        {
            if (GameModel.GameData.ActiveCharacterList.Count<=1)
            {
                GameSignals.LevelFinish.Dispatch();
            }
        }
    }
}
