using Assets.Scripts.Model;
using Assets.Scripts.Views;
using strange.extensions.mediation.impl;
using zModules.FirebaseAnalytics.Service;
using System;



public class LevelManagerMediator : Mediator
{

    [Inject] public LevelManager view { get; set; }
    [Inject] public IPlayerModel PlayerModel { get; set; }
    [Inject] public ILevelModel LevelModel { get; set; }
    [Inject] public GameSignals GameSignals{ get; set; }
    [Inject] public ScreenPanelSignals ScreenSignals { get; set; }
    [Inject] public IFirebaseAnalyticsService FirebaseAnalyticsService { get; set; }


    public override void OnRegister()
    {
        base.OnRegister();
        GameSignals.LevelStarted.AddListener(CreateLevel); 
        GameSignals.CreateFakeLevel.AddListener(CreateFakeLevel);
        GameSignals.Feed.AddListener(TurnPool);
        GameSignals.EnemyFeed.AddListener(TurnPool);
        
        view.onSpawnFeed += GetFeed;
        view.onPathActive += PathActive;
        
        view.SpawnTime = LevelModel.LevelData.FeedSpawnTime;
    }

    public override void OnRemove()
    {
        base.OnRemove();
        GameSignals.LevelStarted.RemoveListener(CreateLevel);
        GameSignals.CreateFakeLevel.RemoveListener(CreateFakeLevel);
        GameSignals.Feed.RemoveListener(TurnPool);
        GameSignals.EnemyFeed.RemoveListener(TurnPool);
        
        view.onSpawnFeed -= GetFeed;
        view.onPathActive -= PathActive;
    }

    public void CreateLevel()
    {
        int curLevel = PlayerModel.GetPlayingCurretLevel();
        view.CreateLevelPrefab(LevelModel.GetLevel(curLevel).Environment);
        GameSignals.UpdateWordLimit.Dispatch(LevelModel.GetLevel(curLevel).MapSize);
        FirebaseAnalyticsService.EventLevelStart("testUser",PlayerModel.GetCurrentLevel().ToString(),"null","null", DateTime.Now.ToString(),"null");
    }
    
    public void CreateFakeLevel()
    {
        view.CreateLevelPrefab(LevelModel.GetFakeLevel());
    }

    private void TurnPool(FishMealIdentity obj)
    {
        obj.TurnPool(view.FeedPool);
    }
    private void GetFeed(FishMealIdentity fi)
    {
        fi.GetFeed();
    }
    private void PathActive()
    {
        GameSignals.PathActive.Dispatch();
    }

    public void FinishLevel()
    {
        
    }

}
