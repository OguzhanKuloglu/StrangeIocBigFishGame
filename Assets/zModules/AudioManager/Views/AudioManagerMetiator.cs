using Sirenix.OdinInspector;
using strange.extensions.mediation.impl;
using zModules.AudioManager.Enums;
using zModules.AudioManager.Signals;

namespace zModules.AudioManager.Views
{
    public class AudioManagerMediator : Mediator
    {
        [Inject] public AudioManagerView view{ get; set;}
        [Inject] public AudioSignals AudioSignals { get; set; }
        
        public override void OnRegister()
        {
            base.OnRegister();
            AudioSignals.Play.AddListener(OnPlay);
            AudioSignals.Stop.AddListener(OnStop);
            AudioSignals.Clear.AddListener(OnClear);
            AudioSignals.MuteMusic.AddListener(OnMuteMusic);
            AudioSignals.MuteSfx.AddListener(OnMuteSFX);
        }

        public override void OnRemove()
        {
            base.OnRemove();
            AudioSignals.Play.RemoveListener(OnPlay);
            AudioSignals.Stop.RemoveListener(OnStop);
            AudioSignals.Clear.RemoveListener(OnClear);
            AudioSignals.MuteMusic.RemoveListener(OnMuteMusic);
            AudioSignals.MuteSfx.RemoveListener(OnMuteSFX);
        }

        [Button("PLAY")]
        public void OnPlay(int layer,AudioTypes type)
        {
            view.Play(layer,type);
        }
        
        [Button("STOP")]
        public void OnStop(int layer)
        {
            view.Stop(layer);
        }
        
        [Button("CLEAR")]
        public void OnClear(int layer)
        {
            view.Clear(layer);
        }

        [Button("MUTE MUSIC")]
        public void OnMuteMusic(bool value)
        {
            view.MuteMusic(value);
        }
        
        [Button("MUTE SFX")]
        public void OnMuteSFX(bool value)
        {
            view.MuteSFX(value);
        }
    }    
}