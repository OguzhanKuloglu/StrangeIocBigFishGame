using Assets.Scripts.Data.Vo;
using Assets.Scripts.Extensions;
using Constans;
using DG.Tweening;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Assets.Scripts.Views
{
    public class IndicatorManager : View
    {
        public UnityAction onEnemyLoad;
        public UnityAction onPlayerSet;
        public GameObject IndicatorPrefab;
        public Transform Player;
        public Dictionary<Transform, IndicatorMy> IndicatorMap;

        private void Start()
        {
            IndicatorMap = new Dictionary<Transform, IndicatorMy>();

        }

        public void SetPlayer(Transform player)
        {
            Player = player;
        }
       

        public void DeleteIndicator()
        {
            if (this.transform.childCount != 0)
            {
                for (int i = 0; i < this.transform.childCount; i++)
                {
                    Destroy(this.transform.GetChild(i).gameObject);
                }
            }
            
        }

        public void CreateEnemyIndicator(Transform enemy)
        {
            var indicator = Instantiate(IndicatorPrefab, this.transform);
            var indicatorMono = indicator.GetComponent<IndicatorMy>();
            IndicatorMap.AddOrUpdate(enemy, indicatorMono);
            indicatorMono.SetTarget(enemy, Player);
            indicatorMono.StartIndicator();
        }

        public void DestoryIndicator(Transform enemy)
        {
            Destroy(IndicatorMap[enemy].gameObject);
            IndicatorMap.Remove(enemy);
        }
    }
}