
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
    public class SettingsView : View
    {
        public UnityAction<bool> onGameMusicButton;
        public UnityAction onGameSoundButton;
        public UnityAction<bool> onGameHapticButton;
        public UnityAction onPopupCloseButton;

        public Button ButtonMusic;
        public Button ButtonHaptic;

        public Text[] ButtonTexts;
        public Sprite[] StatuImages;
        public Image[] Icons;
        public Image[] IconsInside;
        public Image[] IconsButton;

        public GameObject objCloseBtn;

        public SkeletonGraphic RuloPaper;
        [SpineAnimation]
        public string startAnim;
        [SpineAnimation]
        public string loopAnim;
        SkeletonGraphic anim;

        private const string KeyHaptic = "prefs-key-haptic";
        private const string KeyMusic = "prefs-key-haptic";

        public void Start()
        {
            anim = RuloPaper;
            anim.AnimationState.ClearTracks();
            anim.AnimationState.SetAnimation(0, startAnim, false);
            StartCoroutine(corAnim());

            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetString(KeyHaptic, true.ToString());
            PlayerPrefs.SetString(KeyMusic, true.ToString());
            Debug.Log("prefs get : " + PlayerPrefs.GetString(KeyHaptic));
            AnimButtons(ButtonMusic, true, ButtonTexts[0]);
            AnimButtons(ButtonHaptic, true, ButtonTexts[1]);
        }

        public void ClosePopup()
        {
            onPopupCloseButton?.Invoke();
    }
     
        public void SetGameMusic()
        {
            bool musicStatu = PlayerPrefs.GetString(KeyMusic) == "True" ? true : false;

            AnimButtons(ButtonMusic, !musicStatu, ButtonTexts[0]);
            PlayerPrefs.SetString(KeyMusic, (!musicStatu).ToString());
            onGameMusicButton?.Invoke(musicStatu);
        }

    
        public void SetHaptic()
        {
            
            bool hapticStatu = PlayerPrefs.GetString(KeyHaptic) == "True" ? true : false;
            Debug.Log("haptic statu :  " + PlayerPrefs.GetString(KeyHaptic));

            AnimButtons(ButtonHaptic, !hapticStatu, ButtonTexts[1]);
            PlayerPrefs.SetString(KeyHaptic, (!hapticStatu).ToString());
            onGameHapticButton?.Invoke(hapticStatu);
        }

        public void AnimButtons(Button button,bool value,Text text)
        {
            if (value)
            {
                button.image.sprite = StatuImages[0];
                button.transform.DOLocalMoveX(166.788f,0.5f);
                text.text = "ON";
                
            }
            else
            {
                button.image.sprite = StatuImages[1];
                button.transform.DOLocalMoveX(-31.462f, 0.5f);
                text.text = "OFF";
            }
        }

        
        IEnumerator corAnim()
        {
            for (int i = 0; i < 2; i++)
            {
                Icons[i].DOFade(0f, 0f);
                IconsInside[i].DOFade(0f, 0f);
                IconsButton[i].DOFade(0f, 0f);
                ButtonTexts[i].DOFade(0f, 0f);
            }
            objCloseBtn.gameObject.SetActive(false);

            yield return new WaitForSeconds(1);
            for (int i = 0; i < 2; i++)
            {
                Icons[i].DOFade(1f, 0.5f);
                IconsInside[i].DOFade(1f, 0.5f);
                IconsButton[i].DOFade(1f, 0.5f);
                ButtonTexts[i].DOFade(1f, 0.5f);
                yield return new WaitForSeconds(0.25f);
            }
            objCloseBtn.gameObject.SetActive(true);
            yield return null;

        }

    }
}

