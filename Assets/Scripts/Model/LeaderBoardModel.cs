using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Data.Vo;
using Assets.Scripts.Enums;
using Assets.Scripts.Extensions;
using UnityEngine;

namespace Assets.Scripts.Model
{

    public class LeaderBoardModel : ILeaderBoard
    {
        private RD_LeaderBoardData _leaderBoardData;

        public RD_LeaderBoardData LeaderBoardData
        {
            get
            {
                if (_leaderBoardData == null)
                    OnPostConstruct();
                return _leaderBoardData;
            }
            set { }
        }

        [PostConstruct]
        public void OnPostConstruct()
        {
            _leaderBoardData = Resources.Load<RD_LeaderBoardData>("Data/LeaderBoardData");
        }

        #region Func

        public void Reset()
        {
            _leaderBoardData.ActiveCharacterList = new Dictionary<Transform, CharacterVo>();
        }

        public void AddCharacter(Transform transform, CharacterVo vo)
        {
            _leaderBoardData.ActiveCharacterList.AddOrUpdate(transform, vo);
        }

        public int GetPlayerOrder()
        {
            float playerPoint = 0;
            int playerOrder = 0;

            foreach (var character in _leaderBoardData.ActiveCharacterList)
            {
                if (character.Value.ChracterType == CharacterType.Player)
                {
                    playerPoint = character.Value.ProccessValue;
                
                }

            }

            foreach (var character in _leaderBoardData.ActiveCharacterList)
            {

                if (character.Value.ProccessValue >= playerPoint && character.Value.ChracterType != CharacterType.Player)
                {
                    playerOrder++;
                }
            }
            return playerOrder;
        }

        public void SetPlayerDead(bool isDead)
        {
            _leaderBoardData.PlayerDead = isDead;
        }

        #endregion
    }
}
