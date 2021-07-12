using System.Collections.Generic;
using Assets.Scripts.Data.Vo;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Model
{

    public class PlayerPointModel : IPlayerPoint
    {
        private RD_PlayerPointData _PlayerPointData;

        public RD_PlayerPointData PlayerData
        {
            get
            {
                if (_PlayerPointData == null)
                    OnPostConstruct();
                return _PlayerPointData;
            }
            set { }
        }

        [PostConstruct]
        public void OnPostConstruct()
        {
            _PlayerPointData = Resources.Load<RD_PlayerPointData>("Data/PlayerPointData");

        }


        public void ResetData()
        {
            _PlayerPointData.BonusFeedCounter = 0;
            _PlayerPointData.NormalFeedCounter = 0;
        }
        public void AddBonusFeedCount()
        {
            _PlayerPointData.BonusFeedCounter++;
        }

        public void AddNormalFeedCount()
        {
            _PlayerPointData.NormalFeedCounter++;
        }

        public int GetNormalFeedCount()
        {
            return  _PlayerPointData.NormalFeedCounter;
        }

        public int GetBonusFeedCount()
        {
            return _PlayerPointData.BonusFeedCounter;
        }
    }
}
