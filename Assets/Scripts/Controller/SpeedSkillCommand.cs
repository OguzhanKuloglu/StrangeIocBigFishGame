using Assets.Scripts.Model;
using strange.extensions.command.impl;
using DG.Tweening;
using UnityEngine;


namespace Assets.Scripts.Controller
{
    public class SpeedSkillCommand : Command
    {
        [Inject] public GameSignals GameSignals{ get; set; }
        [Inject] public IPlayerModel PlayerModel { get; set; }

        private float progressValue = 0;
        private float wait = 0f;
        public override void Execute()
        {
            progressValue = 0;
            PlayerModel.BoostSpeed();
            GameSignals.SetPlayerSpeed.Dispatch(true);
            DOTween.To(() => progressValue, x => progressValue = x, 1, 3f).OnComplete(() => Completed()) ;
            DOTween.To(() => wait, x => wait= x, 1, 10f).OnComplete(() => GameSignals.SpeedBoostActive.Dispatch());
        }

        public void Completed()
        {
            PlayerModel.UnBoostSpeed();
            GameSignals.SetPlayerSpeed.Dispatch(false);
        }
    }
}