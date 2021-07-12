using System.Collections.Generic;
using Assets.Scripts.Views;
using Sirenix.OdinInspector;
using strange.extensions.mediation.impl;
using UnityEngine;


namespace Assets.Scripts.Views
{
    public class AdsMediator : Mediator
    {
     
        [Inject] public AdmobManager View { get; set; }
        [Inject] public GameSignals GameSignals { get; set; }

        public override void OnRegister()
        {
            base.OnRegister();
            View.OnInterstitialClose += OnInterstitialClose;
            View.OnRewardedClose += OnRewardedClose;

            GameSignals.ShowInterstitial.AddListener(ShowInterstitialAd);
            GameSignals.ShowRewarded.AddListener(ShowRewardedAd);
        }

        public override void OnRemove()
        {
            base.OnRemove();
            View.OnInterstitialClose -= OnInterstitialClose;
            View.OnRewardedClose -= OnRewardedClose;

            GameSignals.ShowInterstitial.RemoveListener(ShowInterstitialAd);
            GameSignals.ShowRewarded.RemoveListener(ShowRewardedAd);

        }

        public void ShowInterstitialAd()
        {
            View.showInterstitial();
        }

        public void ShowRewardedAd()
        {
            View.ShowRWAds();
        }

        public void OnInterstitialClose()
        {

        }

        public void OnRewardedClose(bool value)
        {
            GameSignals.RewardedResult.Dispatch(value);
        }
    }
}