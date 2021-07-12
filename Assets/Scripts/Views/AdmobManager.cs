using System;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
//using GoogleMobileAdsMediationTestSuite.Api;
using UnityEngine;
using UnityEngine.Events;
//using GoogleMobileAds.Api.Mediation.UnityAds;
using strange.extensions.mediation.impl;



namespace Assets.Scripts.Views
{
    public class AdmobManager : View
    {

        private int tryLoadInterstitial = 0;
        private int tryLoadInterstitialAfterGame = 0;
        private int tryLoadRewardedAds = 0;

        public event UnityAction<bool> OnRewardedClose;
        public event UnityAction OnInterstitialClose;


        public InterstitialAd interstitial;
        public RewardedAd rewardedAd;

        public event UnityAction OnInterstitialShow;
        public event UnityAction OnRewardedShow;

        public bool isRewardedVideoReady = false;
        public bool isVideoRewarded = false;
        public bool isInitialized = false;
        public bool isInitializedRewarded = false;

        private bool isSubscribeInterstitial = false;




        private void Start()
        {
            if (Application.internetReachability != NetworkReachability.NotReachable)
            {
                InitAdsManager();
            }
        }

        public void InitAdsManager()
        {
            MobileAds.Initialize((initStatus) =>
            {
                Dictionary<string, AdapterStatus> map = initStatus.getAdapterStatusMap();
                foreach (KeyValuePair<string, AdapterStatus> keyValuePair in map)
                {
                    string className = keyValuePair.Key;
                    AdapterStatus status = keyValuePair.Value;
                    switch (status.InitializationState)
                    {
                        case AdapterState.NotReady:
                            // The adapter initialization did not complete.
                            Debug.Log("Adapter: " + className + " not ready.");
                            break;
                        case AdapterState.Ready:
                            // The adapter was successfully initialized.
                            Debug.Log("Adapter: " + className + " is initialized.");
                            break;
                    }
                }
            });

            RequestInterstitial();
            requestRewardedAds();
        }


        //***********************************************  Rewarded Ads Code Side   ***********************************************  

        public void setMediationsGdpr(bool index)
        {
            //UnityAds.SetGDPRConsentMetaData(index);
            //AppLovin.SetHasUserConsent(true);
        }


        public void ShowRWAds()
        {
            rewardedAd.Show();
        }


        public void requestRewardedAds()
        {

            rewardedAd = new RewardedAd(Conf.AdmobRewardedId);

            // Called when an ad request has successfully loaded.
            rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
            //rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
            rewardedAd.OnAdOpening += HandleRewardedAdOpening;
            rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
            rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
            rewardedAd.OnAdClosed += HandleRewardedAdClosed;

            AdRequest request = new AdRequest.Builder()
                   .Build();
            Debug.Log("*** REWARDED LOADED ***");
            rewardedAd.LoadAd(request);
            //if (Prefs.getBool(Prefs.key_gdpr_ads) || !Prefs.getBool(Prefs.key_is_gdpr_country))
            //{
            //    AdRequest request = new AdRequest.Builder()
            //       .Build();
            //    rewardedAd.LoadAd(request);
            //}
            //else
            //{
            //    AdRequest request = new AdRequest.Builder()
            //    .AddExtra("npa", "1")
            //    .Build();
            //    rewardedAd.LoadAd(request);
            //}

        }


        public void HandleRewardedAdLoaded(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleRewardedAdLoaded event received");
            isRewardedVideoReady = true;
        }

        public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
        {
            Debug.Log("Rewarded Video Failed : {0}" + args.Message);
            isRewardedVideoReady = false;
            if (isRewardedVideoReady)
            {
                if (tryLoadRewardedAds < 3)
                {
                    tryLoadRewardedAds++;
                    requestRewardedAds();
                }
                else
                {
                    Debug.Log("Request RewardedAds stop after try 3 Times");
                }
            }
        }

        public void HandleRewardedAdOpening(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleRewardedAdOpening event received");
        }

        public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
        {
            MonoBehaviour.print("HandleRewardedAdFailedToShow event received with message: " + args.Message);
            requestRewardedAds();
        }

        public void HandleRewardedAdClosed(object sender, EventArgs args)
        {
            Debug.Log("reklam kapanddi");
            isRewardedVideoReady = false;
            requestRewardedAds();
            
        }

        public void HandleUserEarnedReward(object sender, Reward args)
        {
            string type = args.Type;
            double amount = args.Amount;
            isVideoRewarded = true;
            OnRewardedClose?.Invoke(isVideoRewarded);
        }

        public void rewardToUser()
        {
            isRewardedVideoReady = false;
            if (isVideoRewarded)
            {
                isVideoRewarded = true;
            }
            else if (!isVideoRewarded)
            {

                isVideoRewarded = false;
            }
        }



        //***********************************************  Interstitial Ads Code side   ***********************************************  



        public void RequestInterstitial(bool showAfterLoad = false)
        {
            // Clean up banner ad before creating a new one.
            if (interstitial != null)
            {
                interstitial.Destroy();
                isSubscribeInterstitial = false;
            }

            interstitial = new InterstitialAd(Conf.AdmobInterstitialId);
           
            if (!isSubscribeInterstitial)
            {
                // Called when an ad request has successfully loaded.
                interstitial.OnAdLoaded += HandleOnAdLoaded;
                interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
                interstitial.OnAdOpening += HandleOnAdOpened;
                interstitial.OnAdClosed += HandleOnAdClosed;
                isSubscribeInterstitial = true;
            }

            AdRequest request = new AdRequest.Builder()
                   .Build();
            Debug.Log("*** INTERSTITIAL LOADED ***");
            interstitial.LoadAd(request);

            //if (Prefs.getBool(Prefs.key_gdpr_ads) || !Prefs.getBool(Prefs.key_is_gdpr_country))
            //{
            //    AdRequest request = new AdRequest.Builder()
            //       .Build();
            //    interstitial.LoadAd(request);
            //}
            //else
            //{
            //    AdRequest request = new AdRequest.Builder()
            //    .AddExtra("npa", "1")
            //    .Build();
            //    interstitial.LoadAd(request);
            //}
        }


        public void showInterstitial()
        {
            if (interstitial.IsLoaded())
            {
                interstitial.Show();

                Debug.Log("reklam gosterildi");

            }
            else
            {

                RequestInterstitial();
                Debug.Log("reklam show gosterilmedi");
            }


        }


        private void HandleOnAdLeavingApplication(object sender, EventArgs e)
        {
            Debug.Log("reklam leave app");
        }


        private void HandleOnAdClosed(object sender, EventArgs e)
        {
            Debug.Log("reklam kapanddi");
            OnInterstitialClose?.Invoke();

        }

        private void OnApplicationPause(bool pause)
        {

            //if (isClosed && !pause)
            //{
            //    if (onInterstitialCloseDelegate != null)
            //    {
            //        onInterstitialCloseDelegate();
            //        onInterstitialCloseDelegate = null;
            //        RequestInterstitial();
            //        isClosed = false;
            //    }
            //}
            //else if (isClosedAfterGame && !pause)
            //{
            //    if (onInterstitialCloseDelegate != null)
            //    {
            //        onInterstitialCloseDelegate();
            //        onInterstitialCloseDelegate = null;
            //        RequestInterstitial();
            //        isClosedAfterGame = false;
            //    }
            //}
            //else if (isClosedRewarded && !pause)
            //{
            //    if (onHandleRewardAds != null)
            //    {
            //        onHandleRewardAds();
            //        onHandleRewardAds = null;

            //    }
            //    requestRewardedAds();
            //    isClosedRewarded = false;
            //}
        }


        private void HandleOnAdOpened(object sender, EventArgs e)
        {
            Debug.Log("reklam acildi");
        }


        private void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
        {
            Debug.Log("reklam failed load : " + e.Message );
            if (tryLoadInterstitial < 3)
            {
                RequestInterstitial();
                tryLoadInterstitial++;
            }
        }


        private void HandleOnAdLoaded(object sender, EventArgs e)
        {
            tryLoadInterstitial = 0;
            Debug.Log("reklam loaded");

            //showInterstitial();

        }

        public void showMediationTestSuite()
        {
           // MediationTestSuite.Show(Conf.AdmobAppId);
            Debug.Log("TestMediation");
        }

    }
}
