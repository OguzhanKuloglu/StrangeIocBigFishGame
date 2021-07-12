using System.Collections;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

namespace Assets.Scripts.Views
{
    public class MainMenuView : View
    {
        public event UnityAction onPlay;

        public GameObject ObjPlayButton;

        private void Start()
        {
            ObjPlayButton.transform.DOScale(1.15f, 0.40f).SetLoops(-1, LoopType.Yoyo);
        }
        public void PlayButton()
        {
            onPlay?.Invoke();
            
        }

    }
    
}