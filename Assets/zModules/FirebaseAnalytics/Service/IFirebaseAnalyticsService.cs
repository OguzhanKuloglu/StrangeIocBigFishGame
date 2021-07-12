namespace zModules.FirebaseAnalytics.Service
{
    public interface IFirebaseAnalyticsService
    {
        void Initialize();
        void EventLevelStart(string user_id, string level_id, string character_id, string item_id, string date_time,string country_id);
        void EventLevelComplated(string user_id, string level_id, string date_time, string score, string country_id);
        void EventLevelFailed(string user_id, string level_id, string character_id, string item_id, string date_time,string score,string country_id);
        void EventCharacterBuySuccess(string user_id, string character_id, string country_id, string date_time);
        void EventItemBuySuccess(string item_id, string soft_currency, string country_id, string date_time);
        void EventSessionStart(string user_id, string date_time, string country_id, string device_type,string version);
        void EventSessionEnd(string user_id, string date_time, string country_id);
        void EventLevelAttempts(string user_id, string level_id, string character_id, string item_id,string date_time);
        void EventTransactionHard(string user_id, string transaction_id, string hard_currency, string date_time,string country_id);
        void EventAdvInfo(string user_id, string level_id, string adv_placement_id, string adv_type,string timespent_ad, bool is_click, string country_id, string date_time);
        void EventBuyAttempt(string user_id, string item_id, string country_id, string soft_currency,string hard_currency, string date_time);
        void EventBuyAttemptFail(string user_id, string item_id, string country_id, string soft_currency,string hard_currency, string date_time);
        void EventContinueInfo(string user_id, string level_id, string score, bool is_contsoft, bool is_contadv,bool is_replay, bool is_home, string date_time);
        void EventLeaderboardAttempt(string user_id, string having_currency, string leaderboard_rank);
        /*void EventLevelEnd(bool isSuccess, int level);
        
        void EventLevelStart(int level);
        void RewardedAdStarted(string location);
        void RewardedAdFinished(string location);
        void RewardedAdCancelled(string location);
    
        void InterstitialAdStarted(string location);*/
    }
}