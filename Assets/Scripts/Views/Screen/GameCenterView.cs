using strange.extensions.mediation.impl;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.SocialPlatforms;


namespace Assets.Scripts.Views
{
    public class GameCenterView : View
    {
        public UnityAction<string> OnLoginEvent;

        private bool loginSuccessful;
        private string IOSLeaderboardID = "com.roofgames.bigfishgame.scoreboard";
        private string AndroidLeaderboardID = "CgkIxJnt7YYGEAIQAA";

        private ILeaderboard _leaderboard;

        protected override void Awake()
        {
            DontDestroyOnLoad(this);
        }

        protected override void Start()
        {
            AuthenticateUser(); 
        }

        void AuthenticateUser()
        {
#if UNITY_IOS

            Social.localUser.Authenticate((bool success) => {

                if (success)
                {
                    loginSuccessful = true;
                    GetLeaderboardScores();
                    OnLoginEvent?.Invoke(Social.localUser.userName);
                }
                else
                {
                    //unsuccessful
                }
                // handle success or failure            
            });
#elif UNITY_ANDROID
        //PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        //PlayGamesPlatform.InitializeInstance(config);
        //PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate((bool success) => {

            if (success)
            {
                loginSuccessful = true;
                //success
                Debug.Log("success");
                GetLeaderboardScores();
                OnLoginEvent?.Invoke(Social.localUser.userName);
            }
            else
            {
                //unsuccessful
                Debug.Log("unsuccessful");
            }
        });
#endif
        }


        public void GetLeaderboardScores()
        {
#if UNITY_IOS

            _leaderboard = Social.CreateLeaderboard();
            _leaderboard.id = IOSLeaderboardID;


            _leaderboard.LoadScores(result =>
            {

                //Social.LoadScores(IOSLeaderboardID, scores =>
                //{
                foreach (IScore score in _leaderboard.scores)
                {

                }
                

            });
#elif UNITY_ANDROID
        _leaderboard = Social.CreateLeaderboard();
        _leaderboard.id = AndroidLeaderboardID;


        _leaderboard.LoadScores(result =>
        {

            foreach (IScore score in _leaderboard.scores)
            {

            }
            Debug.Log("Load scores finished");

        });
#endif
        }

        public void PostScoreOnLeaderBoard(int myScore)
        {
#if UNITY_EDITOR

#elif UNITY_IOS

        if(loginSuccessful)
        {
            Social.ReportScore(myScore, IOSLeaderboardID, (bool success) => {

            if(success) Debug.Log("Successfully uploaded");

            // handle success or failure

            });
        }
        else    
        {
            Social.localUser.Authenticate((bool success) => {

                if(success)
                {
                    loginSuccessful = true;

                        Social.ReportScore(myScore , IOSLeaderboardID, (bool successful) => {

                            // handle success or failure
                        });
                }
                else
                {
                    Debug.Log("unsuccessful");
                }

                // handle success or failure
            });
        }
#elif UNITY_ANDROID
        if (Social.localUser.authenticated)
        {
            Social.ReportScore(myScore, AndroidLeaderboardID, (bool success) => { });
        }
        else
        {
            // PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
            //PlayGamesPlatform.InitializeInstance(config);
            //PlayGamesPlatform.DebugLogEnabled = true;
            PlayGamesPlatform.Activate();
        Social.localUser.Authenticate((bool success) => {

            if (success)
            {
                loginSuccessful = true;
                //success
                Debug.Log("success");
                Social.ReportScore(myScore, AndroidLeaderboardID, (bool success2) => { });
            }
            else
            {
                //unsuccessful
                Debug.Log("unsuccessful");
            }
        });
        }
#endif
        }

        public void ShowLeaderboard()
        {
#if UNITY_IOS
            if (Social.localUser.authenticated)
            {

                Social.ShowLeaderboardUI();
            }
            else
            {
                Social.localUser.Authenticate((bool success) => {

                    if (success)
                    {
                        loginSuccessful = true;
                        //success
                        Debug.Log("success");

                        //Social.ShowLeaderboardUI();
                    }
                    else
                    {
                        //unsuccessful
                        Debug.Log("unsuccessful");
                    }
                    // handle success or failure            
                });
            }

#elif UNITY_ANDROID
        if (Social.localUser.authenticated)
        {

            PlayGamesPlatform.Instance.ShowLeaderboardUI();
        }
        else
        {
            // PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
            //PlayGamesPlatform.InitializeInstance(config);
            //PlayGamesPlatform.DebugLogEnabled = true;
            PlayGamesPlatform.Activate();
            Social.localUser.Authenticate((bool success) => {

                if (success)
                {
                    loginSuccessful = true;
                    //success
                    Debug.Log("success");
                    
                    PlayGamesPlatform.Instance.ShowLeaderboardUI();
                }
                else
                {
                    //unsuccessful
                    Debug.Log("unsuccessful");
                }
            });
        }
#endif
        }

    }


    
}