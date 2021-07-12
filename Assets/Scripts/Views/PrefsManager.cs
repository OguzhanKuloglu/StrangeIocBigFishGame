using strange.extensions.mediation.impl;
using Assets.Scripts.Extensions;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;


namespace Assets.Scripts.Views
{
    public class PrefsManager : View
    {

        public UnityAction onPrefsSave;
        public UnityAction<int,bool,bool> onPrefsLoad;
        public UnityAction OnQuitSendEvent;
        public UnityAction<string> OnUserNameLoad;
        private const string KeyLevel = "prefs-key-level";
        private const string KeyHaptic = "prefs-key-haptic";
        private const string KeyMusic = "prefs-key-haptic";
        private const string KeyUserName = "prefs-key-username";

        private void Awake()
        {
            loadPrefs();
        }

        private void OnApplicationQuit()
        {
            SavePrefs();
            OnQuitSendEvent?.Invoke();
            
        }

        private void SavePrefs()
        {
            onPrefsSave?.Invoke();
        }

        public void LoadUserName(string username,string userId)
        {
            /**** Save User ****
             
             if (//!ES3.KeyExists(Conf.KeyUserName))
            {
                PlayerPrefs.SetString(KeyUserName, username);
                if(userId != "")
                    PlayerPrefs.SetString(Conf.KeyUserID, userId);

                //ES3.Save(KeyUserName, username);
                
                if(userId != "")
                //ES3.Save(Conf.KeyUserID, userId);
            }*/
            
        }

        /*public string GetUserName()
        {
            return PlayerPrefs.GetString(KeyUserName);
        }*/

        private void loadPrefs()
        {
            /*
            int level = PlayerPrefs.GetInt(KeyLevel);
            bool music = PlayerPrefs.GetString(KeyMusic) == "true" ? true : false;
            bool haptic = PlayerPrefs.GetString(KeyHaptic) == "true" ? true : false;

            if (!PlayerPrefs.HasKey(KeyHaptic))
                haptic = true;
            onPrefsLoad?.Invoke(level,music,haptic);*/
        }

        /*public void SetPrefsData(int levelNo,bool music,bool haptic)
        {
            PlayerPrefs.SetInt(KeyLevel, levelNo);
            PlayerPrefs.SetString(KeyMusic, music.ToString());
            PlayerPrefs.SetString(KeyHaptic, haptic.ToString());
        }*/

        /*
        public bool GetHaptic()
        {
            return PlayerPrefs.GetString(KeyHaptic) == "true" ? true : false;
        }

        public bool GetMusic()
        {
            return PlayerPrefs.GetString(KeyMusic) == "true" ? true : false;
        }*/

    }

}

