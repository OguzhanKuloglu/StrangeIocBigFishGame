using Assets.Scripts.Data.Vo;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Model
{
 
    public class EvolveModel : IEvolveModel
    {
        private CD_EvolveData _evolveData;

        public CD_EvolveData EvolveData
        {
            get
            {
                if (_evolveData == null)
                    OnPostConstruct();
                return _evolveData;
            }
            set { }
        }

        [PostConstruct]
        public void OnPostConstruct()
        {
            _evolveData = Resources.Load<CD_EvolveData>("Data/EvolveData");
        }

        #region Func

        public EvolveVo GetEvolve(EvolveType type)
        {
            return _evolveData.List[type];
        }
        #endregion
    }   
}
