using Assets.Scripts.Data.Vo;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Model
{
    public interface IGameModel
    {
        RD_GameData GameData { get; set; }
        void SetStatus(GameStatus type);
        GameStatus GetStatus();
        void SetTime(float time);
        float GetTime();
        void Reset();
        void AddCharacter(Transform transform, CharacterVo vo);
        void RemoveCharacter(Transform transform);
        Transform GetNewTarget(float processValue);
        int GetPlayerOrder();
        Transform FindPlayer();
        float GetProcess(Transform character);
        
    }   
}
