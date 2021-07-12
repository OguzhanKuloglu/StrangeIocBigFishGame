using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Data.Vo;
using Assets.Scripts.Enums;
using Assets.Scripts.Extensions;
using UnityEngine;

namespace Assets.Scripts.Model
{

    public class IndicatorModel : IIndicatorModel
    {
        private RD_IndicatorData _indicatorData;

        public RD_IndicatorData IndicatorData
        {
            get
            {
                if (_indicatorData == null)
                    OnPostConstruct();
                return _indicatorData;
            }
            set { }
        }

        [PostConstruct]
        public void OnPostConstruct()
        {
            _indicatorData = Resources.Load<RD_IndicatorData>("Data/IndicatorData");
        }

        #region Func

        public void Reset()
        {   
            _indicatorData.ActiveEnemyList = new Dictionary<Transform, CharacterVo>();
        }

        public void AddPlayer(GameObject player)
        {
            _indicatorData.Player = player;
        }

        public void AddCharacter(Transform transform, CharacterVo vo)
        {
            _indicatorData.ActiveEnemyList.AddOrUpdate(transform, vo);
        }

        public void RemoveCharacter(Transform transform)
        {
            _indicatorData.ActiveEnemyList.Remove(transform);
        }

        private Transform[] positionEnemy;
        public Transform[] GetActiveEnemys()
        {
            int i = 0;
            positionEnemy = new Transform[_indicatorData.ActiveEnemyList.Count];
            foreach (var item in _indicatorData.ActiveEnemyList)
            {
                positionEnemy[i] = item.Key;
                i++;
            }

            return positionEnemy;
        }

        public Transform GetPlayer()
        {
            return _indicatorData.Player.transform;
        }

        
        #endregion
    }
}
