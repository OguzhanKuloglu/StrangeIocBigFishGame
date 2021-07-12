using Assets.Scripts.Data.Vo;
using Assets.Scripts.Enums;
using Assets.Scripts.Model;
using Sirenix.OdinInspector;
using strange.extensions.mediation.impl;
using UnityEngine;


namespace Assets.Scripts.Views
{
    public class IndicatorMediator : Mediator
    {
        [Inject] public IndicatorManager view { get; set; }
        [Inject] public GameSignals GameSignals { get; set; }
        [Inject] public IPlayerModel PlayerModel { get; set; }
        [Inject] public IIndicatorModel IndicatorModel { get; set; }
        


        public override void OnRegister()
        {
            base.OnRegister();
            GameSignals.CreateFakeLevel.AddListener(RestartIndicator);
            GameSignals.LevelStarted.AddListener(RestartIndicator);
            GameSignals.EnemyLoaded.AddListener(EnemyLoaded);
            GameSignals.PlayerLoaded.AddListener(PlayerLoaded);
            GameSignals.DestroyEnemy.AddListener(EnemyDestroy);
        }

        public override void OnRemove()
        {
            base.OnRemove();
            GameSignals.CreateFakeLevel.RemoveListener(RestartIndicator);
            GameSignals.LevelStarted.RemoveListener(RestartIndicator);
            GameSignals.EnemyLoaded.RemoveListener(EnemyLoaded);
            GameSignals.PlayerLoaded.RemoveListener(PlayerLoaded);
            GameSignals.DestroyEnemy.RemoveListener(EnemyDestroy);
        }

        public void RestartIndicator()
        {
            view.DeleteIndicator();
        }

        public void EnemyDestroy(Transform enemy)
        {
            view.DestoryIndicator(enemy);
        }


        public void EnemyLoaded(Transform enemy)
        {
            view.CreateEnemyIndicator(enemy);

        }

        public void PlayerLoaded(Transform player)
        {
            view.SetPlayer(player);
        }

        /* public void LevelStarted()
         {
             view.CreateIndicators(IndicatorModel.GetActiveEnemys(),IndicatorModel.GetPlayer());
         }*/


        [Button]
        public void SetIndicator()
        {
            GameSignals.StartIndicatorManager.Dispatch();
        }
    }

}
