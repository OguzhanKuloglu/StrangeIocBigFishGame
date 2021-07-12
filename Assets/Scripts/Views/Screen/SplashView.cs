using DG.Tweening;
using strange.extensions.mediation.impl;
using UnityEngine.Events;
using UnityEngine.UI;


namespace Assets.Scripts.Views
{
    public class SplashView : View
    {
        public event UnityAction onCompleted;
        public Slider ProgressBar;
        public float Duration;
        private float progressValue = 0;
        protected override void Start()
        {
            base.Start();
            DOTween.To(()=> progressValue, x=> progressValue = x, 1, 3f).OnUpdate(()=>ProgressBar.value = progressValue).OnComplete(()=>onCompleted?.Invoke());
        }
    }

}
