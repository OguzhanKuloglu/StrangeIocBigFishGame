using Assets.Scripts.Model;
using Sirenix.OdinInspector;
using UnityEngine;
using zModules.FirebaseRealtimeDatabase.Model;

namespace zModules.SaveSystemModule.Services
{
    public class SaveSystemService : ISaveSystemService
    {

        private CD_SaveData SaveDataList = ScriptableObject.CreateInstance<CD_SaveData>();

        public void SaveData()
        {
            SaveDataList = Resources.Load<CD_SaveData>("Data/SaveData");
            ES3.Save("PlayerData",SaveDataList.PlayerData,"PlayerData.es3");
            ES3.Save("LevelStatusData",SaveDataList.LevelStatusData,"LevelStatusData.es3");            
            ES3.Save("FirebaseDBData",SaveDataList.FirebaseDBData,"FirebaseDBData.es3");


        }

        public void LoadData()
        {
            SaveDataList = Resources.Load<CD_SaveData>("Data/SaveData");
            if (ES3.FileExists("PlayerData.es3"))
            {
                RD_PlayerData playerData = Resources.Load<RD_PlayerData>("Data/PlayerData");
                RD_PlayerData tempData = ES3.Load<RD_PlayerData>("PlayerData", "PlayerData.es3");
                playerData.Haptic = tempData.Haptic;
                playerData.Music = tempData.Music;
                playerData.SFX = tempData.SFX;
                playerData.NoAds = tempData.NoAds;
                playerData.FistLoad = tempData.FistLoad;
                playerData.CurrentLevel = tempData.CurrentLevel;
                playerData.CurrentScore = tempData.CurrentScore;
                playerData.CurrentPlayingLevel = tempData.CurrentPlayingLevel;
                playerData.TutorialCompleted = tempData.TutorialCompleted;
                playerData.SkillTutorialCompleted = tempData.SkillTutorialCompleted;

            }
            if(ES3.FileExists("LevelStatusData.es3"))
            {
                RD_LevelStatusData leveldata = Resources.Load<RD_LevelStatusData>("Data/LevelStatusData");
                RD_LevelStatusData tempdata =  ES3.Load<RD_LevelStatusData>("LevelStatusData", "LevelStatusData.es3");
                leveldata.List = tempdata.List;
                leveldata.TotalScore = tempdata.TotalScore;

            }
            if(ES3.FileExists("FirebaseDBData.es3"))
            {
                CD_FirebaseDBData firebaedata = Resources.Load<CD_FirebaseDBData>("Data/FirebaseDBData");
                CD_FirebaseDBData tempData = ES3.Load<CD_FirebaseDBData>("FirebaseDBData", "FirebaseDBData.es3");
                firebaedata.Columns = tempData.Columns;
                firebaedata.Tables = tempData.Tables;
                firebaedata.RemovedTables = tempData.RemovedTables;
                firebaedata.UserId = tempData.UserId;
                firebaedata.UserName = tempData.UserName;
                firebaedata.FirsTimeUser = tempData.FirsTimeUser;

            }
        }
    }

}