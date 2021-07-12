using Assets.Scripts.Data.Vo;
using Assets.Scripts.Enums;
using Assets.Scripts.Model;
using Assets.Scripts.Views;
using strange.extensions.command.impl;
using UnityEngine;

namespace Assets.Scripts.Controller
{
    public class ResetDataCommand : Command
    {
       
        [Inject] public IPlayerModel PlayerModel{ get; set; }
        [Inject] public ILeaderBoard LeaderBoard { get; set; }
        [Inject] public IGameModel GameModel { get; set; }
        [Inject] public ITimerModel TimerModel { get; set; }
        [Inject] public IInputModel InputModel { get; set; }
        [Inject] public ICameraModel CameraModel { get; set; }
        [Inject] public IIndicatorModel IndicatorModel { get; set; }

        public override void Execute()
        {
          GameModel.Reset();
          PlayerModel.Reset();
          LeaderBoard.Reset();
          TimerModel.Reset();
          InputModel.Reset();
          CameraModel.Reset();
          IndicatorModel.Reset();
        }
    }
}
