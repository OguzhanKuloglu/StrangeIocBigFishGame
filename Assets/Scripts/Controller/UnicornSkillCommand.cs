

using Assets.Scripts.Model;
using strange.extensions.command.impl;
using DG.Tweening;

namespace Assets.Scripts.Controller
{
    public class UnicornSkillCommand : Command
    {
        [Inject] public GameSignals GameSignals { get; set; }

        private float progressValue = 0;
        private float wait = 0f;
        
        public override void Execute()
        {
            
            DOTween.To(() => progressValue, x => progressValue = x, 1, 1f).OnComplete(() => GameSignals.SetUnicornSkill.Dispatch(false));
            DOTween.To(() => wait, x => wait = x, 1, 4f).OnComplete(() => Completed());
        }
        public void Completed()
        {
            GameSignals.SetUnicornSkill.Dispatch(true);
            GameSignals.UnicornSkillActive.Dispatch();
        }
    }
}