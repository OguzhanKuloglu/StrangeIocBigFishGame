using Assets.Scripts.Data.Vo;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Model
{
    public interface ILeaderBoard
    {
        RD_LeaderBoardData LeaderBoardData { get; set; }

        void Reset();
        void AddCharacter(Transform transform, CharacterVo vo);
        int GetPlayerOrder();
    }
}
