using System;
using Assets.Scripts.Data.Vo;
using Assets.Scripts.Enums;
using Assets.Scripts.Model;
using Sirenix.OdinInspector;
using strange.extensions.mediation.impl;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Views
{
    public class EnemyMediator : Mediator
    {
        [Inject] public EnemyView view{ get; set;}
        [Inject] public IGameModel GameModel { get; set; }
        [Inject] public ILeaderBoard LeaderBoard{ get; set; }
        [Inject] public IIndicatorModel IndicatorModel { get; set; }
        [Inject] public GameSignals GameSignals { get; set; }
        [Inject] public IScaleModel ScaleModel { get; set; }

        public Transform TempPlayer;

        public override void OnRegister()
        {
            base.OnRegister();
            view.onFeed += Feed;
            view.onCharacterLoaded += CharacterLoaded;
            view.onCrash += Crash;
            view.onEnemyTutorial += EnemyTutorial;
            GameSignals.DestroyEnemy.AddListener(SetNewTarget);
            GameSignals.StatusChange.AddListener(CheckStatus);
            GameSignals.HudUpdate.AddListener(HudUpadated);

            TempPlayer = null;
            SetSkill();
          
        }

        public override void OnRemove()
        {
            base.OnRemove();
            view.onFeed -= Feed;
            view.onCharacterLoaded -= CharacterLoaded;
            view.onCrash -= Crash;
            view.onEnemyTutorial -= EnemyTutorial;

            GameSignals.DestroyEnemy.RemoveListener(SetNewTarget);
            GameSignals.StatusChange.RemoveListener(CheckStatus);
            GameSignals.HudUpdate.RemoveListener(HudUpadated);
        }
        public void CharacterLoaded(CharacterVo vo)
        {
            GameModel.AddCharacter(this.transform,vo);
            LeaderBoard.AddCharacter(this.transform, vo);
            GameSignals.EnemyLoaded.Dispatch(this.transform);
            SetNewTarget(null);
        }
        public void TutorialTrigger()
        {
            
        }
        [Button("SET TARGET")]
        public void SetNewTarget(Transform transform)
        {
            if (view.GetProcessValue() < 7)
            {
                view.GetFeedTransform();
                return;
            }
            var target = GameModel.GetNewTarget(view.GetProcessValue());
            if (target == null)
            {
                view.GetFeedTransform();
                return;
            }
            view.SetTarget(target);
        }
        [Button("FEED")]
        public void Feed(FishMealIdentity fi)
        {
            GameSignals.EnemyFeed.Dispatch(fi);
            view.SetScale(ScaleModel.GetScaleFactor(fi.Vo.Type,view.GetCharacterType()));
            view.SetProcess(fi.Vo.EarningProcess);
            GameModel.AddCharacter(this.transform,view.GetCharacterIdentity().Vo);
            
            HudUpadated();
            if (view.GetProcessValue() < 7)
            {
                view.GetFeedTransform();
                return;
            }
            
            var target = GameModel.GetNewTarget(view.GetProcessValue());
            if (target == null)
            {
                view.GetFeedTransform();
                return; 
            }
            view.SetTarget(target);
            
        }
        
        public void Crash(CharacterIdentity ci)
        {
            if (ci.Vo.ProccessValue > view.GetProcessValue())
            {
                GameModel.RemoveCharacter(this.transform);
                GameSignals.DestroyEnemy.Dispatch(this.transform);
                view.Destroy();
            }
        }
        
        public void HudUpadated()
        {
            if (TempPlayer == null)
                TempPlayer = GameModel.FindPlayer();

            if(GameModel.GetProcess(TempPlayer) >= view.GetProcessValue())
                view.SetHud(1);
            
            if (GameModel.GetProcess(TempPlayer) < view.GetProcessValue())
                view.SetHud(2);
            /*if (GameModel.GetProcess(TempPlayer) == view.GetProcessValue())
                view.SetHud(0);*/
        }
        
        private void SetSkill()
        {
            int currentSkill = Random.Range(0, 3);
            switch (currentSkill)
            {
                case 0:
                    view.SetSkill(SkillType.Electro);
                    break;
                case 1:
                    view.SetSkill(SkillType.Inc);
                    break;
                case 2:
                    view.SetSkill(SkillType.Speed);
                    break;
                default:
                    view.SetSkill(SkillType.Speed);
                    break;
            }
        }
        private void CheckStatus()
        {
          view.CheckStatus(GameModel.GetStatus());
        }
        private void EnemyTutorial()
        {
            GameSignals.EnemyTutorial.Dispatch();
        }
    }    
}

