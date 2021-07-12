using Assets.Scripts.Data.Vo;
using UnityEngine;

namespace Assets.Scripts.Model
{
 
    public class LevelModel : ILevelModel
    {
        private CD_LevelData _levelData;

        public CD_LevelData LevelData
        {
            get
            {
                if (_levelData == null)
                    OnPostConstruct();
                return _levelData;
            }
            set { }
        }
        
        [PostConstruct]
        public void OnPostConstruct()
        {
            _levelData = Resources.Load<CD_LevelData>("Data/LevelData");
        }

        #region Func

        public LevelVo GetLevel(int CurrentLevel)
        {
            return _levelData.List[CurrentLevel];
        }

        public float GetTime(int CurrentLevel)
        {
            return _levelData.List[CurrentLevel].Time;
        }
        public GameObject GetFakeLevel()
        {
            return _levelData.FakeLevelPrefab;
        }


        #endregion
    }   
}
