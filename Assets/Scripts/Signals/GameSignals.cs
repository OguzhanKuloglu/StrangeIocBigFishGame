using strange.extensions.signal.impl;
using UnityEngine;

public class GameSignals
{
    public Signal GameStarted = new Signal();
    public Signal LevelStarted = new Signal();
    public Signal CreateFakeLevel = new Signal();
    public Signal LevelFinish = new Signal();
    public Signal LevelRestart = new Signal();
    public Signal InputChanged = new Signal();
    public Signal StatusChange = new Signal();
    public Signal ResetData = new Signal();

    //***** Camera Signals ****
    
    public Signal ZoomOut = new Signal();
    public Signal ZoomIn = new Signal();
    public Signal<Vector2> UpdateWordLimit = new Signal<Vector2>(); 
    public Signal ShakeEffect = new Signal(); 

    //***** Player Evolve *****

    public Signal CheckEvolve = new Signal();
    public Signal Evolve = new Signal();
    public Signal<Transform> PlayerLoaded = new Signal<Transform>();
    public Signal<int> ChangeEvolve = new Signal<int>();
    //***** Feed *************
    public Signal<FishMealIdentity> Feed = new Signal<FishMealIdentity>();
    public Signal<FishMealIdentity> EnemyFeed = new Signal<FishMealIdentity>();
    public Signal HudUpdate = new Signal();

    
    //***** Skills ***********
    public Signal StartSpeedSkill = new Signal();
    public Signal<bool> SetPlayerSpeed = new Signal<bool>();
    public Signal SpeedBoostActive = new Signal();

    public Signal StartElectricSkill = new Signal();
    public Signal<bool> SetElectricSkill = new Signal<bool>();
    public Signal ElectricSkillActive = new Signal();

    public Signal StartIncSkill = new Signal();
    public Signal<bool> SetInkSkill = new Signal<bool>();
    public Signal InkSkillActive = new Signal();

    public Signal StartUnicornSkill = new Signal();
    public Signal<bool> SetUnicornSkill = new Signal<bool>();
    public Signal UnicornSkillActive = new Signal();

    public Signal SkillSelected = new Signal();
    

    //***** Enemy *************
    public Signal<Transform> DestroyEnemy = new Signal<Transform>();
    public Signal PathActive = new Signal();
    public Signal<Transform> EnemyLoaded = new Signal<Transform>();


    //******Tımer******
    public Signal ChangeTime = new Signal();
    public Signal StopTime = new Signal();
    public Signal ContineuTime = new Signal();

    //******Tımer******
    public Signal StartIndicatorManager = new Signal();


    public Signal<string> LoadUserNameGM = new Signal<string>();

    //******Tutorial*******
    public Signal SetTutorialSpeed = new Signal();
    public Signal EnemyTutorial = new Signal();


    ////******AdsManager******

    public Signal ShowInterstitial = new Signal();
    public Signal ShowRewarded = new Signal();
    public Signal<bool> RewardedResult = new Signal<bool>();
    public Signal AfterRewardedSetFeedTrigger = new Signal();
}
