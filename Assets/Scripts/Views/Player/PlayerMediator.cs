using System;
using Assets.Scripts.Data.Vo;
using Assets.Scripts.Enums;
using Assets.Scripts.Model;
using Sirenix.OdinInspector;
using strange.extensions.mediation.impl;
using UnityEngine;
using zModules.AudioManager.Enums;
using zModules.AudioManager.Signals;
using zModules.SaveSystemModule.Services;

namespace Assets.Scripts.Views
{
    public class PlayerMediator : Mediator
    {
        [Inject] public PlayerManager view{ get; set;}
        [Inject] public GameSignals GameSignals{ get; set;}
        [Inject] public IPlayerModel PlayerModel { get; set; }
        [Inject] public ILeaderBoard LeaderBoard { get; set; }
        [Inject] public IGameModel GameModel { get; set; }
        [Inject] public IInputModel InputModel { get; set; }
        [Inject] public IScaleModel ScaleModel { get; set; }
        [Inject] public IEvolveModel EvolveModel { get; set; }
        [Inject] public IPlayerPoint PlayerPoint { get; set; }
        [Inject] public AudioSignals AudioSignals { get; set; }
        [Inject] public ISaveSystemService SaveSystem { get; set; }



        public override void OnRegister()
        {
            base.OnRegister();
            view.onFeed += Feed;
            view.onCharacterLoaded += CharacterLoaded;
            view.onCrash += Crash;
            view.onCharacterToLeaderboard += PlayerToLeaderboard;

            GameSignals.LevelStarted.AddListener(OnLevelStarted);
            GameSignals.LevelRestart.AddListener(OnRestartLevel);
            GameSignals.InputChanged.AddListener(OnRotate);
            GameSignals.Evolve.AddListener(Evolve);
            GameSignals.SetPlayerSpeed.AddListener(SetSpeedBoost);
            GameSignals.SetElectricSkill.AddListener(SetElectricSkill);
            GameSignals.SetInkSkill.AddListener(SetInkSkill);
            GameSignals.SetUnicornSkill.AddListener(SetUnicornSkill);
            GameSignals.StatusChange.AddListener(CheckStatus);
            GameSignals.ResetData.AddListener(OnResetData);
            GameSignals.SkillSelected.AddListener(OnSkillSelected);
            GameSignals.AfterRewardedSetFeedTrigger.AddListener(SetFeedTriggerAfterRewarded);
            GameSignals.SetTutorialSpeed.AddListener(SetTutorialSpeed);
        }

        public override void OnRemove()
        {
            base.OnRemove();
            view.onFeed -= Feed;
            view.onCharacterLoaded -= CharacterLoaded;
            view.onCrash -= Crash;
            view.onCharacterToLeaderboard -= PlayerToLeaderboard;

            GameSignals.LevelStarted.RemoveListener(OnLevelStarted);
            GameSignals.LevelRestart.RemoveListener(OnRestartLevel);
            GameSignals.InputChanged.RemoveListener(OnRotate);
            GameSignals.Evolve.RemoveListener(Evolve);
            GameSignals.SetPlayerSpeed.RemoveListener(SetSpeedBoost);
            GameSignals.SetElectricSkill.RemoveListener(SetElectricSkill);
            GameSignals.SetInkSkill.RemoveListener(SetInkSkill);
            GameSignals.SetUnicornSkill.RemoveListener(SetUnicornSkill);
            GameSignals.StatusChange.RemoveListener(CheckStatus);
            GameSignals.ResetData.RemoveListener(OnResetData);
            GameSignals.SkillSelected.RemoveListener(OnSkillSelected);
            GameSignals.AfterRewardedSetFeedTrigger.RemoveListener(SetFeedTriggerAfterRewarded);
            GameSignals.SetTutorialSpeed.RemoveListener(SetTutorialSpeed);

        }
        [Button("SAVE DATA")]
        public void SaveData()
        {
            SaveSystem.SaveData();
        }
        [Button("LOAD DATA")]
        public void LoadData()
        {
            SaveSystem.LoadData();
        }

        public void OnSkillSelected()
        {
            var skillList = PlayerModel.GetCurrentSkill();
            for (int i = 0; i < skillList.Count; i++)
            {
                if (skillList[i].type == SkillType.Unicorn)
                    view.isUnicornSkill = true;
            }
        }
        public void OnRestartLevel()
        {
            PlayerModel.Reset();
            view.isUnicornSkill = false;
        }
        public void OnResetData()
        {
            view.isUnicornSkill = false;
        }
        public void PlayerToLeaderboard(CharacterVo vo)
        {
            LeaderBoard.AddCharacter(this.transform, vo);
        }
        private void CheckStatus()
        {
            view.CheckStatus(GameModel.GetStatus());
        }
        public void OnLevelStarted()
        {
            var skillList = PlayerModel.GetCurrentSkill();
            foreach (var skill in skillList)
            {
                if (skill.type == SkillType.Unicorn)
                    view.SetIsUnicornSkill(true);
            }
            var evolveVo = EvolveModel.GetEvolve(PlayerModel.GetEvolve());
            if(view.GetIsUnicornSkill())
                view.LoadPlayer(evolveVo.PrefabUnicorn);
            else
                view.LoadPlayer(evolveVo.Prefab);
            
            view.RestartPlayer();
            view.SetSimulate(true);
            if(!PlayerModel.PlayerData.TutorialCompleted)
                view.SetSpeed(0);
            else
                view.SetSpeed(PlayerModel.GetSpeed());
            view.SetIsStarted(true);
            PlayerPoint.ResetData();
        }

        public void SetSpeedBoost(bool value)
        {
            view.SetSpeed(PlayerModel.GetSpeed());
            view.PlaySpeedParticle(value);
        }

        public void SetElectricSkill(bool value)
        {
            view.PlayElectricParticle(value);
        }

        public void SetInkSkill(bool value)
        {
            view.PlayInkParticle(value);
        }

        public void SetUnicornSkill(bool value)
        {
            view.SetIsUnicornSkill(value);
        }

        public void CharacterLoaded(CharacterVo vo)
        {
            GameModel.AddCharacter(this.transform,vo);
            GameSignals.PlayerLoaded.Dispatch(this.transform);   
        }

        [Button("FEED")]
        public void Feed(FishMealIdentity fi)
        {
            if (fi.Vo.Type == FeedType.Bonus)
            {
                PlayerPoint.AddBonusFeedCount();
                view.PlayFeedParticle();
            }
            else if (fi.Vo.Type == FeedType.Bomb)
            {
                view.PlayBombParticle();
            }
            else if (fi.Vo.Type == FeedType.Bottle)
            {
                view.PlayBadOjbParticle();
            }
            else
            {
                PlayerPoint.AddNormalFeedCount();
                view.PlayFeedParticle();
            }

            GameSignals.Feed.Dispatch(fi);
            view.SetProcess(PlayerModel.PlayerData.ProccessValue);
            GameSignals.ChangeEvolve.Dispatch((int)PlayerModel.PlayerData.ProccessValue);
            GameModel.AddCharacter(this.transform,view.GetCharacterIdentity().Vo);
            OnSetScale(fi.Vo.Type, CharacterType.Player);
            GameSignals.HudUpdate.Dispatch();
            if (fi.Vo.Type == FeedType.Bomb)
            {
                GameSignals.ShakeEffect.Dispatch();
                if(PlayerModel.GetHapticValue())
                    Vibration.VibratePeek();
                return;
            }
            if(PlayerModel.GetHapticValue())
               Vibration.VibratePop(); 
        }
        
        public void Evolve()
        {
            var evolveVo = EvolveModel.GetEvolve(PlayerModel.GetEvolve()); 
            view.ResetScale();
            if(view.GetIsUnicornSkill())
                view.Evolve(evolveVo.PrefabUnicorn);
            else
                view.Evolve(evolveVo.Prefab);
        }
        public void Crash(CharacterIdentity ci)
        {
            if (ci.Vo.ProccessValue > view.GetProcess())
            {
                if(view.isInkSkill)
                    return;
                
                if (view.GetIsUnicornSkill())
                {
                    GameSignals.StartUnicornSkill.Dispatch();
                    ci.UnicornSkillEffect();
                    view.PlayUnicornParticle();
                    GameSignals.HudUpdate.Dispatch();
                    return;
                }    
                view.SetIsStarted(false);
                view.SetSimulate(false);
                GameModel.SetStatus(GameStatus.Blocked);
                GameSignals.StatusChange.Dispatch();

                
                GameSignals.LevelFinish.Dispatch();
            }
            if (ci.Vo.ProccessValue < view.GetProcess())
            {
                GameModel.RemoveCharacter(ci.transform);
                GameSignals.DestroyEnemy.Dispatch(ci.transform);
                AudioSignals.Play.Dispatch(2,AudioTypes.FishFeed);
                Destroy(ci.gameObject);
                
                //**** Evolve *****
                PlayerModel.PlayerData.ProccessValue += 1;
                view.SetProcess(PlayerModel.PlayerData.ProccessValue);
                GameSignals.ChangeEvolve.Dispatch((int)PlayerModel.PlayerData.ProccessValue);
                OnSetScale(FeedType.Enemy, CharacterType.Player);

            }
            if(PlayerModel.GetHapticValue())
                Vibration.VibratePeek();
        }
        public void OnRotate()
        {
            if (GameModel.GameData.GameStatus != GameStatus.Play)
            {
                return;
            }
            if(InputModel.GetInputDegree()==0)
                return;
            view.SetRotate(InputModel.GetInputDegree(),PlayerModel.GetRotateSensivity());
        }
        
        [Button("SET ADD SCALE")]
        public void OnSetScale(FeedType feedType,CharacterType characterType)
        {
            view.SetScale(ScaleModel.GetScaleFactor(feedType,characterType));
            if(ScaleModel.GetScaleFactor(feedType,characterType) > 0)
                GameSignals.ZoomOut.Dispatch();
            else
                GameSignals.ZoomIn.Dispatch();
        }


        public void SetFeedTriggerAfterRewarded()
        {
            view.ReturnGameAfterRewardedAd();
        }
        public void SetTutorialSpeed()
        {
            view.SetSpeed(PlayerModel.GetSpeed());
        }
    }    
}

