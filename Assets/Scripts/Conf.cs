using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public static class Conf 
    {
        public const string KeyLevel = "prefs-key-level";
        public const string KeyHaptic = "prefs-key-haptic";
        public const string KeyMusic = "prefs-key-haptic";
        public const string KeyUserName = "prefs-key-username";
        public const string KeyUserID = "prefs-key-keyuserid";


        /********* Admob Ids *********/

#if UNITY_EDITOR

        public const string AdmobAppId = "ca-app-pub-7990050205408700~2503374569";
        public const string AdmobBannerId = "";
        public const string AdmobInterstitialId = "ca-app-pub-3940256099942544/4411468910"; // "ca-app-pub-7990050205408700/4182593214";
        public const string AdmobRewardedId = "ca-app-pub-3940256099942544/1712485313";     // "ca-app-pub-7990050205408700/2835574725";

#elif UNITY_IOS
    public const string AdmobAppId = "ca-app-pub-7990050205408700~2503374569";
    public const string AdmobBannerId = "";
    public const string AdmobInterstitialId = "ca-app-pub-7990050205408700/4182593214"; // "ca-app-pub-7990050205408700/4182593214";
    public const string AdmobRewardedId = "ca-app-pub-7990050205408700/2835574725";     // "ca-app-pub-7990050205408700/2835574725";
#elif UNITY_ANDROID
    public const string AdmobAppId = "ca-app-pub-7990050205408700~4447011620";
    public const string AdmobBannerId = "";
    public const string AdmobInterstitialId = "ca-app-pub-7990050205408700/8028385174";
    public const string AdmobRewardedId = "ca-app-pub-7990050205408700/5872801606";
#endif

    }
}
