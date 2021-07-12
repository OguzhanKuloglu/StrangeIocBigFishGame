using strange.extensions.mediation.impl;
using Assets.Scripts.Extensions;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace Assets.Scripts.Views
{
    public class LevelManager : View
    {
        public event UnityAction<FishMealIdentity> onSpawnFeed;
        public event UnityAction onPathActive;
        
        public Transform FeedPool;
        public GameObject PathFinder;
        
        private float spawnWait = 0;
        private float mapScan = 0f;
        [HideInInspector]
        public float SpawnTime;
        
        protected override void Start()
        {
            base.Start();
            SpawnTimer();
        }

        public void CreateLevelPrefab(GameObject levelPrefab)
        {
            this.gameObject.transform.DestroyChildren();
            GameObject levelMap = Instantiate(levelPrefab, this.gameObject.transform);
            DOTween.To(() => mapScan, x => mapScan = x, 1, 1f).OnComplete(() => SetActivePathFinder());
        }

        private void SpawnTimer()
        {
            DOTween.To(()=> spawnWait, x=> spawnWait = x, 1, SpawnTime).OnComplete(()=>SpawnFeedObject());
        }
        public void SpawnFeedObject()
        {
            spawnWait = 0;
            if (FeedPool.transform.childCount > 0)
            {
                onSpawnFeed?.Invoke(FeedPool.transform.GetChild(0).GetComponent<FishMealIdentity>());
            }
            SpawnTimer();
        }

        [Button("SCAN")]
        public void SetActivePathFinder()
        {
             PathFinder.SetActive(true);
             PathFinder.GetComponent<AstarPath>().Scan();
             onPathActive?.Invoke();
        }
        
    }

}