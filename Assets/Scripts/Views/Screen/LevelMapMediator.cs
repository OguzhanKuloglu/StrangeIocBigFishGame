using Assets.Scripts.Data.Vo;
using Assets.Scripts.Enums;
using Assets.Scripts.Model;
using Assets.Scripts.Views;
using strange.extensions.mediation.impl;
using zModules.AudioManager.Enums;
using zModules.AudioManager.Signals;


public class LevelMapMediator : Mediator
{
    [Inject] public LevelMapManager View { get; set; }
    [Inject] public IPlayerModel PlayerModel { get; set; }
    [Inject] public ScreenPanelSignals ScreenSignals{ get; set;}
    [Inject] public GameSignals GameSignals{ get; set;}
    [Inject] public AudioSignals AudioSignals { get; set; }

    

    public override void OnRegister()
    {
        base.OnRegister();
        View.onMapingCompleted += OnMapCompleted;
        View.onPlay += OnPlay;
        View.onLoadLeaderBoard += LoadLeaderBoard;
        View.onGameHapticButton += OnGameHapticButton;
        View.onGameMusicButton += OnGameMusicButton;

        View.Maping();
        if(!PlayerModel.PlayerData.TutorialCompleted)
            View.Tutorial(true);
    }

    public override void OnRemove()
    {
        base.OnRemove();
        View.onMapingCompleted -= OnMapCompleted;
        View.onPlay -= OnPlay;
        View.onLoadLeaderBoard -= LoadLeaderBoard;
        View.onGameHapticButton -= OnGameHapticButton;
        View.onGameMusicButton -= OnGameMusicButton;
    }
    private void OnGameHapticButton(bool value)
    {
        PlayerModel.SetHapticValue(value);
    }

    private void OnGameMusicButton(bool value)
    {
        PlayerModel.SetMusicValue(value);
        PlayerModel.SetSFXValue(value);
        AudioSignals.MuteMusic.Dispatch(!value);
        AudioSignals.MuteSfx.Dispatch(!value);
    }

    private void LoadLeaderBoard()
    {
        AudioSignals.Play.Dispatch(4,AudioTypes.ButtonClick);
        ScreenSignals.OpenPanel.Dispatch(new PanelVo()
        {
            Layer = 1,
            PanelName = GameScreen.Leaderboard.ToString()
        });
    }

    private void OnPlay(int arg0)
    {
        GameSignals.CreateFakeLevel.Dispatch();
        AudioSignals.Play.Dispatch(4,AudioTypes.ButtonClick);
        ScreenSignals.ClearPanel.Dispatch(0);
        ScreenSignals.ClearPanel.Dispatch(2);
        PlayerModel.SetPlayingCurretLevel(arg0);
        PlayerModel.Reset();
        ScreenSignals.OpenPanel.Dispatch(new PanelVo()
        {
            Layer = 1,
            PanelName = GameScreen.SkillPopup.ToString()
        });
    }

    private void OnMapCompleted()
    {
        View.SetButtons(PlayerModel.GetCurrentLevel());
    }
}
