using Assets.Scripts.Model;
using strange.extensions.command.impl;
using UnityEngine;
using DG.Tweening;

namespace Assets.Scripts.Controller
{
    public class ElectricSkillComannd : Command
    {
        [Inject] public GameSignals GameSignals { get; set; }
        [Inject] public IPlayerModel PlayerModel { get; set; }

        private float progressValue = 0;
        private float wait = 0f;
        public override void Execute()
        {
            GameSignals.SetElectricSkill.Dispatch(true);
            DOTween.To(() => progressValue, x => progressValue = x, 1, 3f).OnComplete(() => Completed());
            DOTween.To(() => wait, x => wait = x, 1, 10f).OnComplete(() => GameSignals.ElectricSkillActive.Dispatch());
        }

        public void Completed()
        {
            GameSignals.SetElectricSkill.Dispatch(false);
        }
    }
}