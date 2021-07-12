using Assets.Scripts.Model;
using strange.extensions.mediation.impl;
using Assets.Scripts.Enums;
using Assets.Scripts.Data.Vo;
using zModules.AudioManager.Enums;
using zModules.AudioManager.Signals;

namespace Assets.Scripts.Views
{
    public class SkillPopupMediator : Mediator
    {
        [Inject] public SkillPopupView View { get; set; }
        [Inject] public IPlayerModel PlayerModel{ get; set; }
        [Inject] public GameSignals GameSignals { get; set; }
        [Inject] public ScreenPanelSignals ScreenSignals { get; set; }
        [Inject] public IGameModel GameModel { get; set; }
        [Inject] public AudioSignals AudioSignals { get; set; }



        public override void OnRegister()
        {
            base.OnRegister();
            View.OnElectricSkillSelect  += SetSelectedSkillToUser;
            View.OnInkSkillSelect       += SetSelectedSkillToUser;
            View.OnUnicornSkillSelect   += SetSelectedSkillToUser;
            View.OnSpeedSkillSelect     += SetSelectedSkillToUser;
            
            View.CheckSkill(PlayerModel.GetCurrentSkill());
            
            if (!PlayerModel.PlayerData.TutorialCompleted && View.Evolve)
            {
                View.SecTutorial(true);
                return;
            }
            if (!PlayerModel.PlayerData.TutorialCompleted && !View.Evolve)
            {
                View.Tutorial(true);
            }
           
        }

        public override void OnRemove()
        {
            base.OnRemove();
            View.OnElectricSkillSelect  -= SetSelectedSkillToUser;
            View.OnInkSkillSelect       -= SetSelectedSkillToUser;
            View.OnUnicornSkillSelect   -= SetSelectedSkillToUser;
            View.OnSpeedSkillSelect     -= SetSelectedSkillToUser;
            
        }

        public void SetSelectedSkillToUser(SkillType model)
        {
            AudioSignals.Play.Dispatch(4,AudioTypes.ButtonClick);
            if (View.Evolve)
            {
                SkillVo PlayerSkillVo = new SkillVo();
                PlayerSkillVo.type = model;
                PlayerModel.SetCurrentSkill(PlayerSkillVo);
                ScreenSignals.OpenPanel.Dispatch(new PanelVo()
                {
                    Layer = 0,
                    PanelName = GameScreen.GamePlay.ToString()
                });
                GameModel.SetStatus(GameStatus.Play);
                GameSignals.StatusChange.Dispatch();
                GameSignals.SkillSelected.Dispatch();
                GameSignals.Evolve.Dispatch();
                PlayerModel.PlayerData.Evolved = true;
            }
            else
            {
                SkillVo PlayerSkillVo = new SkillVo();
                PlayerSkillVo.type = model;
                PlayerModel.Reset();
                PlayerModel.SetCurrentSkill(PlayerSkillVo);
                ScreenSignals.ClearPanel.Dispatch(1);
                ScreenSignals.OpenPanel.Dispatch(new PanelVo()
                {
                    Layer = 0,
                    PanelName = GameScreen.GamePlay.ToString()
                });
                GameModel.SetStatus(GameStatus.Play);
                GameSignals.CheckEvolve.Dispatch();
                GameSignals.LevelStarted.Dispatch();
   
            }
            GameSignals.SkillSelected.Dispatch();

        }
    }
}




