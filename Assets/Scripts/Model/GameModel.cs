using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Data.Vo;
using Assets.Scripts.Enums;
using Assets.Scripts.Extensions;
using UnityEngine;

namespace Assets.Scripts.Model
{
 
    public class GameModel : IGameModel
    {
        private RD_GameData _gameData;

        public RD_GameData GameData
        {
            get
            {
                if (_gameData == null)
                    OnPostConstruct();
                return _gameData;
            }
            set { }
        }

        [PostConstruct]
        public void OnPostConstruct()
        {
            _gameData = Resources.Load<RD_GameData>("Data/GameData");
        }

        #region Func

        public void SetStatus(GameStatus type)
        {
            _gameData.GameStatus = type;
        }
        public GameStatus GetStatus()
        {
            return _gameData.GameStatus;
        }
        public void SetTime(float time)
        {
            _gameData.CurrentTime = time;
        }
        public float GetTime()
        {
            return _gameData.CurrentTime;
        }

        public void Reset()
        {
            _gameData.GameStatus = GameStatus.Blocked;
            _gameData.CurrentTime = -1;
            foreach (var character in _gameData.ActiveCharacterList)
            {
                if (character.Value.ChracterType == CharacterType.Player)
                {
                    var Player = character;
                    _gameData.ActiveCharacterList = new Dictionary<Transform, CharacterVo>();
                    _gameData.ActiveCharacterList.AddOrUpdate(Player.Key,Player.Value);
                    return;
                }
            }
        }
        
        public void AddCharacter(Transform transform, CharacterVo vo)
        {
            _gameData.ActiveCharacterList.AddOrUpdate(transform,vo);
        }
        public void RemoveCharacter(Transform transform)
        {
            _gameData.ActiveCharacterList.Remove(transform);
        }

        public Transform GetNewTarget(float processValue)
        {
            //        Transform RandomTransform = null;
            List<Transform> list = new List<Transform>();
            foreach (var character in _gameData.ActiveCharacterList)
            {
                //          if(RandomTransform == null)
                //            RandomTransform = character.Key;
                if(processValue > character.Value.ProccessValue)
                    list.Add(character.Key);
            }

            if (list.Count > 0)
            {
                int index = Random.Range(0, list.Count);
                return list[index];
            }
            return null;
        }

        public int GetPlayerOrder()
        {
            float playerPoint = 0;
            int playerOrder = 0;

            foreach (var character in _gameData.ActiveCharacterList)
            {
                if (character.Value.ChracterType == CharacterType.Player)
                {
                    playerPoint = character.Value.ProccessValue;
                } 
            }

            foreach (var character in _gameData.ActiveCharacterList)
            {
                if (character.Value.ProccessValue > playerPoint && character.Value.ChracterType != CharacterType.Player )
                {
                    playerOrder++;
                }
            }

            return playerOrder;
        }

        public Transform FindPlayer()
        {
            foreach (var character in _gameData.ActiveCharacterList)
            {
                if(character.Value.ChracterType == CharacterType.Player)
                    return character.Key;
            }
            return null;
        }
        
        public float GetProcess(Transform character)
        {
            return _gameData.ActiveCharacterList[character].ProccessValue;
        }

        #endregion
    }   
}
