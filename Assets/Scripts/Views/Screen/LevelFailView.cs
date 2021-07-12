using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.Views
{
    public class LevelFailView : View
    {

        public event UnityAction onRestartButton;
        public event UnityAction onRewardedButton;
        public event UnityAction onBackToHomeButton;
        public Button btnShowRewarded;
        

        public int RewardedShowingCounter = 0;

        public void RestartGame()
        {
            onRestartButton?.Invoke();
        }

        public void WatchRewardedAds()
        {
            onRewardedButton?.Invoke();
        }

        private void OnEnable()
        {
            if (RewardedShowingCounter == 0)
            {
                RewardedShowingCounter++;
                btnShowRewarded.gameObject.SetActive(true);
                
            }
            else
            {
                btnShowRewarded.gameObject.SetActive(false);
                
            }
        }



        public void ResetCounter()
        {
            RewardedShowingCounter = 0;
        }

      

        public void BackToHome()
        {
            onBackToHomeButton?.Invoke();
        }
    }

}
