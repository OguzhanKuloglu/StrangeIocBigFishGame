using System;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Model
{
 
    public class ScaleModel : IScaleModel
    {
        private CD_ScaleData _scaleData;

        public CD_ScaleData ScaleData
        {
            get
            {
                if (_scaleData == null)
                    OnPostConstruct();
                return _scaleData;
            }
            set { }
        }
        
        [PostConstruct]
        public void OnPostConstruct()
        {
            _scaleData = Resources.Load<CD_ScaleData>("Data/ScaleData");
        }

        #region Func

        public float GetScaleFactor(FeedType feedType,CharacterType characterType)
        {
            try
            {
                for (int i = 0; i < _scaleData.List[feedType].Count; i++)
                {
                    if (_scaleData.List[feedType][i].CharacterType == characterType)
                        return _scaleData.List[feedType][i].ScaleFactor;
                }
            }
            catch (Exception e)
            {
                Debug.Log("*** BUG *** = " + feedType.ToString());
            }//    _scaleData.List[fishMealType][0].ScaleFactor
            return -1f;
        }
       
        #endregion
    }   
}
