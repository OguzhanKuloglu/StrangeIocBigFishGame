using strange.extensions.mediation.impl;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Views
{
    public class LevelSuccessView : View
    {
        public event UnityAction onRestartGame;
        public event UnityAction onContinueGame;
        public event UnityAction onRestartLeaderBoard;

        public Text textTimer;
        public Image[] imageStars;


        public void RestartGame()
        {
            onRestartGame?.Invoke();
        }

        public void ContineuGame()
        {
            onContinueGame?.Invoke();
        }

        public void SetTimer(float time)
        {
            textTimer.text = ((int)time).ToString();
        }

        public void SetStars(int starCount)
        {

            StartCoroutine(corAnim(starCount));
            Debug.Log(" star count : " + starCount);

        }

        IEnumerator corAnim(int starCount)
        {
            for (int i = 0; i < imageStars.Length; i++)
            {
                imageStars[i].transform.localScale = Vector3.zero;
            }
            yield return new WaitForSeconds(0.3f);
            for (int i = 3; i > starCount; i--)
            {
                imageStars[3 - i].gameObject.SetActive(true);
                imageStars[3 - i].transform.DOScale(Vector3.one, 0.6f);
                yield return new WaitForSeconds(0.6f);
            }


            yield return null;

        }


    }

}