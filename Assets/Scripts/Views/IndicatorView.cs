using Assets.Scripts.Data.Vo;
using Assets.Scripts.Extensions;
using Constans;
using DG.Tweening;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Assets.Scripts.Views
{
    public class IndicatorView : View
    {
        public UnityAction onStart;

        public Transform Target;
        public Transform Player;
        public GameObject Arrow;
        public bool isIndicatorSet;

        public void SetTarget(Transform target,Transform player)
        {
            Target = target;
            Player = player;
            Arrow = this.transform.GetChild(0).gameObject;
        }
    
        private void Update()
        {

            //if (isIndicatorSet)
            //{
            //    this.transform.position = Player.transform.position;
            //    var dir = Target.transform.position - Player.transform.position;
            //    var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            //    this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


            //    if (Vector2.Distance(Target.transform.position, Player.transform.position) > 5)
            //    {
            //        Arrow.SetActive(true);
            //    }
            //    else
            //    {
            //        Arrow.SetActive(false);
            //    }

            //}

        }


        public void StartIndicator()
        {
            isIndicatorSet = true;
        }

        public void StopIndicator()
        {
            isIndicatorSet = false;
        }
    }

}
