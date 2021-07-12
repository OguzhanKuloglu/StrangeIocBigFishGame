using System.Collections.Generic;
using Assets.Scripts.Data.Vo;
using Assets.Scripts.Enums;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;
using System.Collections;

namespace Assets.Scripts.Views
{
    public class GamePlayView : View
    {
        public event UnityAction<Vector2,float> onJoystikButton;
        public event UnityAction<SkillType> onSpeedButton;
        public event UnityAction<SkillType> onElectricButton;
        public event UnityAction<SkillType> onInkButton;
        public event UnityAction onPauseButton;
        public event UnityAction onFirstClick;

        public VariableJoystick JoystickData;

        public Button SpeedButton;
        public Button ElectricButton;
        public Button InkButton;
        public Button PauseButton;
        public Slider TimerSlider;
        public Slider EvolveSlider;
        public float Angle;

        private float horizontalDir = 1;
        private float verticalDir = 0;

        public GameObject TutorialObject;
        public GameObject SkillTutorialObject;
        public GameObject TimerTutorialObject;
        public GameObject EnemyTutorialObject;

        private void Start()
        {
            //JoystickData.gameObject.SetActive(false);
        }

        public void  UseSkillSpeed()
        {
            onSpeedButton?.Invoke(SkillType.Speed);
            StartCoroutine(corDoSkillAnim(SpeedButton,10f));
        }

        public void UseSkillElectric()
        {
            onElectricButton?.Invoke(SkillType.Electro);
            StartCoroutine(corDoSkillAnim(ElectricButton, 10f));
        }

        public void UseSkillInk()
        {
            onInkButton?.Invoke(SkillType.Inc);
            InkButton.interactable = false;
            StartCoroutine(corDoSkillAnim(InkButton, 15f));
        }

        IEnumerator corDoSkillAnim(Button skill,float time)
        {
            skill.GetComponent<Image>().DOFade(0, 0f);
            skill.GetComponent<Image>().DOFade(0.5f, time);
            yield return new WaitForSeconds(time);
            skill.GetComponent<Image>().DOFade(1f, 0);
            skill.interactable = true;
            yield return null;
        }

        public void PauseGame()
        {
            onPauseButton?.Invoke();
        }

        private Transform PlayerTransform;
        private bool fistClick = false;
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!fistClick)
                {
                    fistClick = true;
                    onFirstClick?.Invoke();
                }
            }

            if (Input.GetMouseButton(0))
            {
                horizontalDir = (JoystickData.Direction.y >= 0) ? 1 : -1;
                if (horizontalDir > 0)
                {
                    verticalDir = (JoystickData.Direction.x <= 0) ? 1 : -1;
                }
                else
                {
                    verticalDir = (JoystickData.Direction.x >= 0) ? 1 : -1;
                }
                Angle = Vector2.Angle(Vector2.up,JoystickData.Direction)*horizontalDir*verticalDir;
                onJoystikButton?.Invoke(JoystickData.Direction,Angle);
            }
        }

       
        public void SetTimerProgress(float time,float levelTime)
        {
            TimerSlider.maxValue = levelTime;
            TimerSlider.value = time;
        }

        public void SetEvolveProgress(int time, float levelTime)
        {
            if (EvolveSlider.value == 0 && time == -1)
                return;

            EvolveSlider.maxValue = 10;
            EvolveSlider.value = time;
        }



        public void CheckSkill(List<SkillVo> SkillList)
        {
            ElectricButton.gameObject.SetActive(false);
            InkButton.gameObject.SetActive(false);
            SpeedButton.gameObject.SetActive(false);
            
            foreach (var skillVo in SkillList)
            {
                switch (skillVo.type)
                {
                    case SkillType.Electro:
                        ElectricButton.gameObject.SetActive(true);
                        break;
                    case SkillType.Inc:
                        InkButton.gameObject.SetActive(true);
                        break;
                    case SkillType.Speed:
                        SpeedButton.gameObject.SetActive(true);
                        break;
                }
            }
        }

        public void Tutorial(bool value)
        {
            TutorialObject.SetActive(value);
        }

        public void SkillTutorial(bool value)
        {
            SkillTutorialObject.SetActive(value);
        }
        public void TimerTutorial(bool value)
        {
            TimerTutorialObject.SetActive(value);
        }
        public void EnemyTutorial(bool value)
        {
            float wait = 0;
            EnemyTutorialObject.SetActive(value);
            DOTween.To(() => wait, x => wait = x, 1, 5f).OnComplete(() => EnemyTutorialObject.SetActive(false));

        }
    }

}
