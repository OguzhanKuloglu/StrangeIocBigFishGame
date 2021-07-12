
using Assets.Scripts.Enums;
using Assets.Scripts.Model;
using strange.extensions.mediation.impl;
using UnityEngine;
using DG.Tweening;
using Assets.Scripts.Data.Vo;
using zModules.AudioManager.Enums;
using zModules.AudioManager.Signals;

namespace Assets.Scripts.Views
{
    public class GamePlayMediator : Mediator
    {
        [Inject] public GamePlayView View           { get; set; }
        [Inject] public IInputModel InputModel      { get; set; }
        [Inject] public GameSignals GameSignals     { get; set;}
        [Inject] public ScreenPanelSignals ScreenSignals { get; set; }
        [Inject] public IPlayerModel PlayerModel    { get; set; }
        [Inject] public AudioSignals AudioSignals { get; set; }
        [Inject] public ITimerModel TimerModel { get; set; }
        [Inject] public IGameModel GameModel { get; set; }
        [Inject] public ILevelModel LevelModel { get; set; }

        private bool hurryUp = false;

        public override void OnRegister()
        {
            base.OnRegister();
            View.onJoystikButton    += SetInputData;
            View.onSpeedButton      += UseBoost;
            View.onElectricButton   += UseBoost;
            View.onInkButton        += UseBoost;
            View.onPauseButton      += PauseGame;
            View.onFirstClick      += FirstClick;

            GameSignals.SpeedBoostActive.AddListener(EnableSpeedSkill);
            GameSignals.ElectricSkillActive.AddListener(EnableElectricSkill);
            GameSignals.InkSkillActive.AddListener(EnableInkSkill);
            GameSignals.ChangeTime.AddListener(SetTime);
            GameSignals.ChangeEvolve.AddListener(SetEvolveProgress);
            GameSignals.EnemyTutorial.AddListener(EnemyTutorial);
            SetEvolveProgress((int)PlayerModel.PlayerData.ProccessValue);

            CheckSkill();
            if(!PlayerModel.PlayerData.SkillTutorialCompleted)
                Tutorial();
        }

        public override void OnRemove()
        {
            base.OnRemove();
            View.onJoystikButton    -= SetInputData;
            View.onSpeedButton      -= UseBoost;
            View.onElectricButton   -= UseBoost;
            View.onInkButton        -= UseBoost;
            View.onPauseButton      -= PauseGame;
            View.onFirstClick      -= FirstClick;

            GameSignals.SpeedBoostActive.RemoveListener(EnableSpeedSkill);
            GameSignals.ElectricSkillActive.RemoveListener(EnableElectricSkill);
            GameSignals.InkSkillActive.RemoveListener(EnableInkSkill);
            GameSignals.ChangeTime.RemoveListener(SetTime);
            GameSignals.ChangeEvolve.RemoveListener(SetEvolveProgress);
            GameSignals.EnemyTutorial.RemoveListener(EnemyTutorial);
        }

        public void SetTime()
        {
            View.SetTimerProgress(TimerModel.GetTime(),LevelModel.GetTime(PlayerModel.GetPlayingCurretLevel()));
            if (LevelModel.GetTime(PlayerModel.GetPlayingCurretLevel()) - TimerModel.GetTime() < 30 && !hurryUp && !PlayerModel.PlayerData.TutorialCompleted)
            {
                View.TimerTutorial(true);
                hurryUp = true;
            }
        }

        public void SetEvolveProgress(int fi)
        {
            View.SetEvolveProgress((int)fi, 10f);
        }

        public void PauseGame()
        {
            AudioSignals.Play.Dispatch(4,AudioTypes.ButtonClick);
            ScreenSignals.OpenPanel.Dispatch(new PanelVo()
            {
                Layer = 0,
                PanelName = GameScreen.PausePopup.ToString()
            });
            GameModel.SetStatus(GameStatus.Blocked);
            GameSignals.StatusChange.Dispatch();
        }

       
        public void SetInputData(Vector2 direction,float angle)
        {
            InputModel.SetInput(direction);
            InputModel.SetInputDegree(angle);
            GameSignals.InputChanged.Dispatch();
        }
        public void UseBoost(SkillType type)
        {
            AudioSignals.Play.Dispatch(4,AudioTypes.ButtonClick);
            var CurrentSkill = PlayerModel.GetCurrentSkill();
            foreach (var skillVo in CurrentSkill)
            {
                if (skillVo.type == type)
                {
                    switch (skillVo.type)
                    {
                        case SkillType.Speed:
                            GameSignals.StartSpeedSkill.Dispatch();
                            View.SpeedButton.interactable = false;
                            if (!PlayerModel.PlayerData.SkillTutorialCompleted)
                            {
                                PlayerModel.PlayerData.SkillTutorialCompleted = true;
                                SkillTutorial(GameStatus.Play, false);
                            }
                            break;
                        case SkillType.Electro:
                            GameSignals.StartElectricSkill.Dispatch();
                            View.ElectricButton.interactable = false;
                            break;
                        case SkillType.Inc:
                            GameSignals.StartIncSkill.Dispatch();
                            View.InkButton.interactable = false;
                            break;
                    }
                    return;
                }
              
            }
        }

       /* public void UseSpeedBoost()
        {
            var CurrentSkill = PlayerModel.GetCurrentSkill();
            if (CurrentSkill[0].type != Enums.SkillType.Speed)
            {
                return;
            }
            GameSignals.StartSpeedSkill.Dispatch();
            View.SpeedButton.interactable = false;
            
        }
        public void UseElectricSkill() {
            var CurrentSkill = PlayerModel.GetCurrentSkill();
            if (CurrentSkill[0].type != Enums.SkillType.Electro)
            {
                return;
            }
            GameSignals.StartElectricSkill.Dispatch();
            View.ElectricButton.interactable = false;

        }
        public void UseInkSkill()
        {
            var CurrentSkill = PlayerModel.GetCurrentSkill();
            if (CurrentSkill[0].type != Enums.SkillType.Inc)
            {
                return;
            }
            GameSignals.StartIncSkill.Dispatch();
            View.InkButton.interactable = false;

        }*/

        public void EnableSpeedSkill()
        {
            View.SpeedButton.interactable = true;
        }
        public void EnableElectricSkill()
        {
            View.ElectricButton.interactable = true;   
        }
        public void EnableInkSkill()
        {
            View.InkButton.interactable = true;
        }
        public void CheckSkill()
        {
            View.CheckSkill(PlayerModel.GetCurrentSkill());
        }
        public void Tutorial()
        {
            View.Tutorial(true);
        }
        public void FirstClick()
        {
            GameSignals.SetTutorialSpeed.Dispatch();
            float wait = 0f;
            if (!PlayerModel.PlayerData.TutorialCompleted)
            {
                View.Tutorial(false);
                DOTween.To(() => wait, x => wait = x, 1, 2f).OnComplete(() => SkillTutorial(GameStatus.Blocked,true));
            }
        }
        
        public void SkillTutorial(GameStatus status,bool value)
        {
                if(value && PlayerModel.PlayerData.SkillTutorialCompleted)
                    return;
                GameModel.SetStatus(status);
                GameSignals.StatusChange.Dispatch();
                View.SkillTutorial(value);
        }
        public void EnemyTutorial()
        {
            View.EnemyTutorial(true);
        }
    }
}




