using System.Collections.Generic;
using strange.extensions.mediation.impl;
using UnityEngine;
using Assets.Scripts.Data.Vo;
using Assets.Scripts.Enums;
using UnityEngine.UI;
using UnityEngine.Events;
using Spine;
using Spine.Unity;
using System.Collections;
using DG.Tweening;

namespace Assets.Scripts.Views
{
    public class SkillPopupView : View
    {
        public UnityAction<SkillType> OnSpeedSkillSelect;
        public UnityAction<SkillType> OnElectricSkillSelect;
        public UnityAction<SkillType> OnInkSkillSelect;
        public UnityAction<SkillType> OnUnicornSkillSelect;
        
        public Button SpeedButton;
        public Button ElectricButton;
        public Button InkButton;
        public Button UnicornButton;

        public Image[] SkillsButton;
        public Text[] SkillsTexts;
        public Text[] SkillsTextsExp;
        public bool Evolve;

        public SkeletonGraphic RuloPaper;
        [SpineAnimation]
        public string startAnim;
        [SpineAnimation]
        public string loopAnim;
        SkeletonGraphic anim;

        public GameObject TutorialObject;
        private float wait = 0f;
        private float wait2 = 0f;

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

        IEnumerator corAnim()
        {
            for (int i = 0; i < 4; i++)
            {
                SkillsButton[i].DOFade(0f, 0f);
                SkillsTexts[i].DOFade(0f, 0f);
                SkillsTextsExp[i].DOFade(0f, 0f);
            }

            yield return new WaitForSeconds(1);
            for (int i = 0; i < 4; i++)
            {
                SkillsButton[i].DOFade(1f,0.5f);
                SkillsTexts[i].DOFade(1f, 0.5f);
                SkillsTextsExp[i].DOFade(1f, 0.5f);
                yield return new WaitForSeconds(0.25f);
            }
            yield return null;

        }


        public void SelectedSkillSpeed()
        {
            OnSpeedSkillSelect?.Invoke(SkillType.Speed);
        }

        public void SelectedSkillElectric()
        {
            OnElectricSkillSelect?.Invoke(SkillType.Electro);   
        }

        public void SelectedSkillInc()
        {
            OnInkSkillSelect?.Invoke(SkillType.Inc);
        }


        public void SelectedSkillUnicorn()
        {
            OnUnicornSkillSelect?.Invoke(SkillType.Unicorn);
        }

        public void CheckSkill(List<SkillVo> SkillList)
        {
            
            ElectricButton.interactable = true;
            InkButton.interactable = true;
            SpeedButton.interactable = true;
            UnicornButton.interactable = true;
            
            foreach (var skillVo in SkillList)
            {
                switch (skillVo.type)
                {
                    case SkillType.Electro:
                        ElectricButton.interactable = false;
                        ElectricButton.gameObject.SetActive(false);
                        break;
                    case SkillType.Inc:
                        InkButton.interactable = false;
                        InkButton.gameObject.SetActive(false);
                        break;
                    case SkillType.Speed:
                        SpeedButton.interactable = false;
                        SpeedButton.gameObject.SetActive(false);
                        break;
                    case SkillType.Unicorn:
                        UnicornButton.interactable = false;
                        UnicornButton.gameObject.SetActive(false);
                        break;
                }
            }
            
        }

        public void Tutorial(bool value)
        {
            wait = 0f;
            DOTween.To(() => wait, x => wait = x, 1, 1.5f).OnComplete(() => SetTutorial(value));
            if (value)
            {
                ElectricButton.interactable = false;
                InkButton.interactable = false;
                UnicornButton.interactable = false;
            }
            
        }
        public void SecTutorial(bool value)
        {
            wait2 = 0f;
            DOTween.To(() => wait2, x => wait2 = x, 1, 2f).OnComplete(() => SetTutorial(value));
            if (value)
            {
                //ElectricButton.interactable = false;
                InkButton.interactable = false;
                UnicornButton.interactable = false;
            }
            
        }

        private void SetTutorial(bool value)
        {
            TutorialObject.SetActive(value);
        }
    }

}
