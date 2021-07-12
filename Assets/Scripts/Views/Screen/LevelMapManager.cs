using System.Collections.Generic;
using Assets.Scripts.Data.Vo;
using Sirenix.OdinInspector;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;




namespace Assets.Scripts.Views
{
    public class LevelMapManager : View
    {
        public event UnityAction onMapingCompleted;
        public event UnityAction<int> onPlay;
        public event UnityAction onLoadLeaderBoard;
        public UnityAction<bool> onGameMusicButton;
        public UnityAction<bool> onGameHapticButton;
        public Button ButtonSound;
        public Button ButtonHaptic;

        public Sprite[] Actives;
        public Sprite[] InActives;

        public RD_LevelStatusData LevelStatusData;
        

        [ListDrawerSettings(ShowIndexLabels = true)]
        public List<LevelIdenhtity> Map;

        public GameObject TutorialObject;
        public int SelectedIndex;


        private const string levelPrefsName = "LevelData-";
        private const string KeyHaptic = "prefs-key-haptic";
        private const string KeyMusic = "prefs-key-haptic";

        private void OnEnable()
        {
            bool musicStatu = PlayerPrefs.GetString(KeyMusic) == "True" ? true : false;
            bool hapticStatu = PlayerPrefs.GetString(KeyHaptic) == "True" ? true : false;

            ButtonSound.GetComponent<Image>().sprite = musicStatu == true ? Actives[0] : InActives[0];
            ButtonHaptic.GetComponent<Image>().sprite = hapticStatu == true ? Actives[0] : InActives[0];

        }

        public void Maping()
        {
            var LevelIdentitys = GetComponentsInChildren<LevelIdenhtity>();
            foreach (var li in LevelIdentitys)
            {
                Map.Add(li);
            }
            onMapingCompleted?.Invoke();
        }
        private string loadDataKey;
        private LevelDataVo afterload = new LevelDataVo();
        public void SetButtons(int CurrentLevel)
        {

            for (int i = 0; i < CurrentLevel; i++)
            {
                if (LevelStatusData.List.Count > i)
                {
                    Map[i].SetStars(LevelStatusData.List[i].StarCount);
                }
                Map[i].SetState(0);
                Map[i].SetActive();
            }

            SelectedIndex = CurrentLevel;
            Map[CurrentLevel].SetState(2);
            Map[CurrentLevel].SetActive();

        }
        public void SelectedChange(int currentIndex)
        {
            Map[SelectedIndex].SetState(0);
            SelectedIndex = currentIndex;
        }
        
        public void PlayButton(int LevelIndex)
        {
            onPlay?.Invoke(LevelIndex);
        }

        public void LoadLeaderBoardPanel()
        {
            onLoadLeaderBoard?.Invoke();
        }

        public void SetGameMusic()
        {
            bool musicStatu = PlayerPrefs.GetString(KeyMusic) == "True" ? true : false;


            ButtonSound.GetComponent<Image>().sprite = !musicStatu == true ? Actives[0] : InActives[0];

            PlayerPrefs.SetString(KeyMusic, (!musicStatu).ToString());
            onGameMusicButton?.Invoke(!musicStatu);
        }


        public void SetHaptic()
        {
                
            bool hapticStatu = PlayerPrefs.GetString(KeyHaptic) == "True" ? true : false;

            ButtonHaptic.GetComponent<Image>().sprite = hapticStatu == true ? Actives[0] : InActives[0];

            PlayerPrefs.SetString(KeyHaptic, (!hapticStatu).ToString());
            onGameHapticButton?.Invoke(hapticStatu);
        }

        public void Tutorial(bool value)
        {
            TutorialObject.SetActive(value);
        }

    }
}
