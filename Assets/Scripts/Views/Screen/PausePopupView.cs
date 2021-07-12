using System.Collections.Generic;
using Assets.Scripts.Data.Vo;
using Assets.Scripts.Enums;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;
using System.Collections;
using Spine;
using Spine.Unity;

namespace Assets.Scripts.Views
{
    public class PausePopupView : View
    {
        public UnityAction onGameRestartButton;
        public UnityAction onGameReturnButton;
        public UnityAction onReturnHomeButton;
        public UnityAction onSettingMenuButton;


        public Image[] PopupButtons;
        public Image[] PopupButtonInside;

        public SkeletonGraphic RuloPaper;
        [SpineAnimation]
        public string startAnim;
        [SpineAnimation]
        public string loopAnim;
        SkeletonGraphic anim;


        public void Start()
        {
            anim = RuloPaper;
            anim.AnimationState.ClearTracks();
            anim.AnimationState.SetAnimation(0, startAnim, false);
            StartCoroutine(corAnim());
        }

        public void StartAnimation()
        {
            RuloPaper = GetComponent<SkeletonGraphic>();
            anim.AnimationState.ClearTracks();
            anim.AnimationState.SetAnimation(0, startAnim, false);
        }

        public void RestartGame()
        {
            onGameRestartButton?.Invoke();
        }

        public void UnpauseGame()
        {
            onGameReturnButton?.Invoke();

        }

        public void SetHomeScreen()
        {
            onReturnHomeButton?.Invoke();
        }

        public void SetSettingsSCreen()
        {
            onSettingMenuButton?.Invoke();
        }

        IEnumerator corAnim()
        {
            for (int i = 0; i < 4; i++)
            {
                PopupButtons[i].DOFade(0f, 0f);
                PopupButtonInside[i].DOFade(0f, 0f);
                
            }

            yield return new WaitForSeconds(1);
            for (int i = 0; i < 4; i++)
            {
                PopupButtons[i].DOFade(1f, 0.5f);
                PopupButtonInside[i].DOFade(1f, 0.5f);
                yield return new WaitForSeconds(0.25f);
            }
            yield return null;

        }


    }
}

