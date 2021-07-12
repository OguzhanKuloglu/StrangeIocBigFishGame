using Assets.Scripts.Model;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Assets.Scripts.Views
{
    public class CameraMediator : Mediator
    {
        [Inject] public CameraManager view{ get; set;}
        [Inject] public GameSignals GameSignals{ get; set;}
        [Inject] public ICameraModel CameraModel { get; set; }
        [Inject] public ILevelModel LevelModel { get; set; }
        [Inject] public IPlayerModel PlayerModel { get; set; }

        public override void OnRegister()
        {
            base.OnRegister();
            GameSignals.LevelStarted.AddListener(OnLevelStarted);
            GameSignals.ZoomOut.AddListener(OnZoomOut);
            GameSignals.ZoomIn.AddListener(OnZoomIn);
            GameSignals.UpdateWordLimit.AddListener(UpdateWorldLimit);
            GameSignals.ShakeEffect.AddListener(ShakeEffect);

        }
        public override void OnRemove()
        {
            base.OnRemove();
            GameSignals.LevelStarted.RemoveListener(OnLevelStarted);
            GameSignals.ZoomOut.RemoveListener(OnZoomOut);
            GameSignals.ZoomIn.RemoveListener(OnZoomIn);
            GameSignals.UpdateWordLimit.RemoveListener(UpdateWorldLimit);
            GameSignals.ShakeEffect.AddListener(ShakeEffect);

        }
        public void OnLevelStarted()
        {
            view.SetFollowDump(CameraModel.GetFollowDump());
            CameraModel.SetLevelMaxZoomIn(LevelModel.GetLevel(PlayerModel.GetCurrentLevel()).CameraType);
            view.SetZoomValue(0.5f,CameraModel.GetZoomValue());
        }
        private void OnZoomOut()
        {
            CameraModel.ZoomOut(LevelModel.GetLevel(PlayerModel.GetCurrentLevel()).CameraType);
            view.SetZoomValue(0.5f,CameraModel.GetZoomValue());
        }

        private void OnZoomIn()
        {
            CameraModel.ZoomIn(LevelModel.GetLevel(PlayerModel.GetCurrentLevel()).CameraType);
            view.SetZoomValue(0.5f,CameraModel.GetZoomValue());
        }
        private void UpdateWorldLimit(Vector2 WorldLimit)
        {
            view.UpdateWorldLimit(WorldLimit);
        }
        private void ShakeEffect()
        {
            view.Shake();
        }
    }    
}

