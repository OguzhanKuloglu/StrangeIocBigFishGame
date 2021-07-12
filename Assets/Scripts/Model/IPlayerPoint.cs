using System.Collections.Generic;
using Assets.Scripts.Data.Vo;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Model
{
    public interface IPlayerPoint
    {
        RD_PlayerPointData PlayerData { get; set; }

        void ResetData();
        void AddBonusFeedCount();
        void AddNormalFeedCount();
        int GetNormalFeedCount();
        int GetBonusFeedCount();
    }
}