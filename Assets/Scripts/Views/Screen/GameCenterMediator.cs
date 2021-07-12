using strange.extensions.mediation.impl;
using zModules.FirebaseRealtimeDatabase.Model;
using zModules.SaveSystemModule.Services;

namespace Assets.Scripts.Views
{
    public class GameCenterMediator : Mediator
    {
        [Inject] public GameCenterView view { get; set; }
        [Inject] public GameSignals GameSignals { get; set; }
        [Inject] public IFirebaseDBModel FirebaseDBModel { get; set; }
        [Inject] public ISaveSystemService SaveSystem { get; set; }


        public override void OnRegister()
        {
            base.OnRegister();
            SaveSystem.LoadData();
            view.OnLoginEvent += OnCompleted;
            
        }

        public override void OnRemove()
        {
            base.OnRemove();
            view.OnLoginEvent -= OnCompleted;

        }

        private void OnCompleted(string username)
        {
            GameSignals.LoadUserNameGM.Dispatch(username);
            FirebaseDBModel.SetUserName(username);
        }
    }
}
