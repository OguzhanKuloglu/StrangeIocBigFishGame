using System.Collections.Generic;
using Assets.Scripts.Data.Vo;
using Assets.Scripts.Enums;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;
using System.Collections;
using Spine;
using zModules.FirebaseRealtimeDatabase.Data.Vo;
using zModules.FirebaseRealtimeDatabase.Constant;
using Spine.Unity;


namespace Assets.Scripts.Views
{
    public class LeaderboardView : View
    {

        public UnityAction onCloseLeaderBoard;
        public UnityAction onLoadDb;


        public SkeletonGraphic RuloPaper;
        [SpineAnimation]
        public string startAnim;
        [SpineAnimation]
        public string loopAnim;
        SkeletonGraphic anim;

        public GameObject[] prefabs;
        public Text[] UserOrderText;

        public GameObject objClosebtn; 
        

        public void Start()
        {
            onLoadDb?.Invoke();
            anim = RuloPaper;
            anim.AnimationState.ClearTracks();
            anim.AnimationState.SetAnimation(0, startAnim, false);
            StartCoroutine(corAnim());
        }

        public void StartAnimation()
        {
            RuloPaper = GetComponent<SkeletonGraphic>();
            anim.AnimationState.ClearTracks();
            anim.AnimationState.SetAnimation(0, startAnim, false);
        }


        
        public void LoadBoard(Dictionary<string, List<DataResultVo>> data,string userid)
        {

            if (data.ContainsKey(userid))
            {
                Debug.Log("USER FOUND ID "+userid);
            }
            else
            {
                Debug.Log("USE ID NOTFOUND");
            }
            List<string> names = new List<string>();
            List<string> score = new List<string>();
            foreach (var item in data)
            {
                for (int i = 0; i < item.Value.Count; i++)
                {
                    if (item.Value[i].Key == Leaderboard_Columns.TotalPoint)
                    {
                        score.Add(item.Value[i].Value);
                    }
                }
                for (int i = 0; i < item.Value.Count; i++)
                {
                     if (item.Value[i].Key == Leaderboard_Columns.UserName)
                     {
                        //prefabs[j].transform.GetChild(0).GetComponent<Text>().text = item.Value[i].Value;
                        names.Add(item.Value[i].Value);
                    }
                }
            }

            int index = 0;
            bool isFoundMe = false;
            for (int i = names.Count-1; i >= 0; i--)
            {

                if (score[i] == data[userid][0].Value && names[i] == data[userid][1].Value && !isFoundMe)
                {
                    prefabs[index].GetComponent<Image>().color = Color.green;
                    isFoundMe = true;
                }
                
                UserOrderText[index].text = score[i];
                prefabs[index].transform.GetChild(0).GetComponent<Text>().text = names[i];
               
                if (index >= 6 && !isFoundMe)
                {
                    prefabs[index].GetComponent<Image>().color = Color.green;
                    UserOrderText[6].text = data[userid][0].Value;
                    prefabs[6].transform.GetChild(0).GetComponent<Text>().text =data[userid][1].Value;
                    prefabs[6].transform.GetChild(2).GetComponentInChildren<Text>().text =(names.Count - names.IndexOf(data[userid][1].Value)).ToString();
                    return;
                }

                if (index >= 6)
                {
                    return;
                }

                index++;
            }

        }

        IEnumerator corAnim()
        {
            for (int i = 0; i < 7; i++)
            {
                prefabs[i].SetActive(false);
            }
            objClosebtn.SetActive(false);

            yield return new WaitForSeconds(1);
            for (int i = 0; i < 7; i++)
            {
                prefabs[i].gameObject.SetActive(true);
                yield return new WaitForSeconds(0.15f);
            }
            objClosebtn.SetActive(true);
            yield return null;

        }

        public void CloseLeaderBoard()
        {
            for (int i = 0; i < 7; i++)
            {
                prefabs[i].gameObject.SetActive(false);
            }
            onCloseLeaderBoard?.Invoke();
        }
    }
}
