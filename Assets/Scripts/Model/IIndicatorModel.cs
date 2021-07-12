using System.Collections.Generic;
using Assets.Scripts.Data.Vo;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Model
{
    public interface IIndicatorModel
    {
        RD_IndicatorData IndicatorData { get; set; }
        void Reset();
        void AddCharacter(Transform transform, CharacterVo vo);
        void AddPlayer(GameObject player);
        void RemoveCharacter(Transform transform);
        Transform[] GetActiveEnemys();
        Transform GetPlayer();
    }
}

