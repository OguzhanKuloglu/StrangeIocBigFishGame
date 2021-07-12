using Assets.Scripts.Context;
using Assets.Scripts.Data.Vo;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Model
{
 
    public class LevelStatusModel : ILevelStatusModel
    {
        private RD_LevelStatusData _levelStatusData;

        public RD_LevelStatusData LevelStatusData
        {
            get
            {
                if (_levelStatusData == null)
                    OnPostConstruct();
                return _levelStatusData;
            }
            set { }
        }
        
        [PostConstruct]
        public void OnPostConstruct()
        {
            _levelStatusData = Resources.Load<RD_LevelStatusData>("Data/LevelStatusData");
        }

        #region Func

        public int GetLevelStatusCount()
        {
            return _levelStatusData.List.Count;
        }

        public void AddLevelStatus(LevelDataVo vo)
        {
            _levelStatusData.List.Add(vo);
        }

        public LevelDataVo GetLevelStatus(int index)
        {
            return _levelStatusData.List[index];
        }

        public void SetLevelStatus(int index, LevelDataVo vo)
        {
            _levelStatusData.List[index] = vo;
        }
        public void SetTotalPoint()
        {
            int tempTotalScore = 0;
            for (int i = 0; i < _levelStatusData.List.Count; i++)
            {
                tempTotalScore += _levelStatusData.List[i].LevelPoint;
            }
            _levelStatusData.TotalScore = tempTotalScore;
        }

        public int GetTotalPoint()
        {
            return _levelStatusData.TotalScore;
        }
        #endregion
    }   
}
