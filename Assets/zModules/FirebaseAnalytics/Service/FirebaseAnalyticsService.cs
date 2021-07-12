using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
using UnityEngine;

namespace zModules.FirebaseAnalytics.Service
{
    public class FirebaseAnalyticsService : IFirebaseAnalyticsService
    {
        private DependencyStatus _dependencyStatus = DependencyStatus.UnavailableOther;
        public void Initialize()
        {
            /*var options = new AppOptions();
            options.ApiKey = "AIzaSyB76lKQep3_MFGQS-oPmNcmGeah4N6BRlE";
            options.AppId = "1:681051331823:ios:d8cbd610744eeead9dcf1a";
            options.ProjectId = "trivia-clash-3d";
            FirebaseApp.Create(options);*/
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
                _dependencyStatus = task.Result;
                if (_dependencyStatus == DependencyStatus.Available)
                {
                    InitializeFirebase();
                } 
                else 
                {
                    Debug.LogError(
                        "Could not resolve all Firebase dependencies: " + _dependencyStatus);
                }
            });
        }
        

        private void InitializeFirebase() {
            Debug.Log("Enabling data collection.");
            var app = FirebaseApp.DefaultInstance;
            Firebase.Analytics.FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
            /*
            Debug.Log("Set user properties.");
            // Set the user's sign up method.
            FirebaseAnalytics.SetUserProperty(
                FirebaseAnalytics.UserPropertySignUpMethod,
                "Google");
            // Set the user ID.
            FirebaseAnalytics.SetUserId("uber_user_510");
            // Set default session duration values.
            FirebaseAnalytics.SetMinimumSessionDuration(new TimeSpan(0, 0, 10));
            FirebaseAnalytics.SetSessionTimeoutDuration(new TimeSpan(0, 30, 0));*/
        }

        public void EventLevelStart(string user_id,string level_id,string character_id,string item_id,string date_time,string country_id)
        {
            Firebase.Analytics.FirebaseAnalytics.LogEvent("level_start",
                new Parameter("user_id",user_id),
                new Parameter("level_id",level_id),
                new Parameter("character_id",character_id),
                new Parameter("item_id",item_id),
                new Parameter("date_time",date_time),
                new Parameter("country_id",country_id));
        }
        public void EventLevelComplated(string user_id,string level_id,string date_time,string score,string country_id)
        {
            Firebase.Analytics.FirebaseAnalytics.LogEvent("level_complate",
                new Parameter("user_id",user_id),
                new Parameter("level_id",level_id),
                new Parameter("date_time",date_time),
                new Parameter("score",score),
                new Parameter("country_id",country_id));
        }
        public void EventLevelFailed(string user_id,string level_id,string character_id,string item_id,string date_time,string score,string country_id)
        {
            Firebase.Analytics.FirebaseAnalytics.LogEvent("level_failed",
                new Parameter("user_id",user_id),
                new Parameter("level_id",level_id),
                new Parameter("character_id",character_id),
                new Parameter("item_id",item_id),
                new Parameter("date_time",date_time),
                new Parameter("score",score),
                new Parameter("country_id",country_id)
                );
        }
        public void EventCharacterBuySuccess(string user_id,string character_id,string country_id,string date_time)
        {
            Firebase.Analytics.FirebaseAnalytics.LogEvent("character_buy_success",
                new Parameter("user_id",user_id),
                new Parameter("character_id",character_id),
                new Parameter("country_id",country_id),
                new Parameter("date_time",date_time)
            );
        }
        public void EventItemBuySuccess(string item_id,string soft_currency,string country_id,string date_time)
        {
            Firebase.Analytics.FirebaseAnalytics.LogEvent("item_buy_success",
                new Parameter("item_id",item_id),
                new Parameter("soft_currency",soft_currency),
                new Parameter("country_id",country_id),
                new Parameter("date_time",date_time)
            );
        }
        public void EventSessionStart(string user_id,string date_time,string country_id,string device_type,string version)
        {
            Firebase.Analytics.FirebaseAnalytics.LogEvent("session_start",
                new Parameter("user_id",user_id),
                new Parameter("date_time",date_time),
                new Parameter("country_id",country_id),
                new Parameter("device_type",device_type),
                new Parameter("version",version)
            );
        }
        public void EventSessionEnd(string user_id,string date_time,string country_id)
        {
            Firebase.Analytics.FirebaseAnalytics.LogEvent("session_end",
                new Parameter("user_id",user_id),
                new Parameter("date_time",date_time),
                new Parameter("country_id",country_id)
            );
        }
        public void EventLevelAttempts(string user_id,string level_id,string character_id,string item_id,string date_time)
        {
            Firebase.Analytics.FirebaseAnalytics.LogEvent("level_attempts",
                new Parameter("user_id",user_id),
                new Parameter("level_id",level_id),
                new Parameter("character_id",character_id),
                new Parameter("item_id",item_id),
                new Parameter("date_time",date_time)
            );
        }
        public void EventTransactionHard(string user_id,string transaction_id,string hard_currency,string date_time,string country_id)
        {
            Firebase.Analytics.FirebaseAnalytics.LogEvent("transaction_hard",
                new Parameter("user_id",user_id),
                new Parameter("transaction_id",transaction_id),
                new Parameter("hard_currency",hard_currency),
                new Parameter("date_time",date_time),
                new Parameter("country_id",country_id)
            );
        }
        public void EventAdvInfo(string user_id,string level_id,string adv_placement_id,string adv_type,string timespent_ad,bool is_click,string country_id,string date_time)
        {
            Firebase.Analytics.FirebaseAnalytics.LogEvent("adv_info",
                new Parameter("user_id",user_id),
                new Parameter("level_id",level_id),
                new Parameter("adv_placement_id",adv_placement_id),
                new Parameter("adv_type",adv_type),
                new Parameter("timespent_ad",timespent_ad),
                new Parameter("is_click",is_click.ToString()),
                new Parameter("country_id",country_id),
                new Parameter("date_time",date_time)
            );
        }
        public void EventBuyAttempt(string user_id,string item_id,string country_id,string soft_currency,string hard_currency,string date_time)
        {
            Firebase.Analytics.FirebaseAnalytics.LogEvent("buy_attempt",
                new Parameter("user_id",user_id),
                new Parameter("item_id",item_id),
                new Parameter("country_id",country_id),
                new Parameter("adv_type",soft_currency),
                new Parameter("hard_currency",hard_currency),
                new Parameter("date_time",date_time)
            );
        }
        public void EventBuyAttemptFail(string user_id,string item_id,string country_id,string soft_currency,string hard_currency,string date_time)
        {
            Firebase.Analytics.FirebaseAnalytics.LogEvent("buy_attempt_fail",
                new Parameter("user_id",user_id),
                new Parameter("item_id",item_id),
                new Parameter("country_id",country_id),
                new Parameter("adv_type",soft_currency),
                new Parameter("hard_currency",hard_currency),
                new Parameter("date_time",date_time)
            );
        }
        public void EventContinueInfo(string user_id,string level_id,string score,bool is_contsoft,bool is_contadv,bool is_replay,bool is_home,string date_time)
        {
            Firebase.Analytics.FirebaseAnalytics.LogEvent("continue_info",
                new Parameter("user_id",user_id),
                new Parameter("level_id",level_id),
                new Parameter("score",score),
                new Parameter("is_contsoft",is_contsoft.ToString()),
                new Parameter("is_contadv",is_contadv.ToString()),
                new Parameter("is_replay",is_replay.ToString()),
                new Parameter("is_home",is_home.ToString()),
                new Parameter("date_time",date_time)
            );
        }
        public void EventLeaderboardAttempt(string user_id,string having_currency,string leaderboard_rank)
        {
            Firebase.Analytics.FirebaseAnalytics.LogEvent("continue_info",
                new Parameter("user_id",user_id),
                new Parameter("having_currency",having_currency),
                new Parameter("leaderboard_rank",leaderboard_rank)
            );
        }
        /*public void EventLevelEnd(bool isSuccess,int level)
        {
            Firebase.Analytics.FirebaseAnalytics.LogEvent(
                Firebase.Analytics.FirebaseAnalytics.EventLevelEnd,
                new Parameter(Firebase.Analytics.FirebaseAnalytics.ParameterLevelName, level.ToString()),
                new Parameter(Firebase.Analytics.FirebaseAnalytics.ParameterSuccess , isSuccess.ToString()));
        }
        public void EventLevelStart(int level)
        {
            Firebase.Analytics.FirebaseAnalytics.LogEvent(
                Firebase.Analytics.FirebaseAnalytics.EventLevelStart,
                new Parameter(Firebase.Analytics.FirebaseAnalytics.ParameterLevelName,level.ToString()));
        }

        public void RewardedAdStarted(string location)
        {
            Firebase.Analytics.FirebaseAnalytics.LogEvent("rewardedAdStarted","ad_location",location);
        }
        public void RewardedAdFinished(string location)
        {
            Firebase.Analytics.FirebaseAnalytics.LogEvent("rewardedAdFinished", "ad_location",location);
        }
        public void RewardedAdCancelled(string location)
        {
            Firebase.Analytics.FirebaseAnalytics.LogEvent("rewardedAdCancelled", "ad_location",location);
        }
        
        public void InterstitialAdStarted(string location)
        {
            Firebase.Analytics.FirebaseAnalytics.LogEvent("interstitial_started", "ad_location",location);
        }*/
    }
}