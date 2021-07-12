using Assets.Scripts.Data.Vo;
using Assets.Scripts.Enums;
using Assets.Scripts.Model;
using Sirenix.OdinInspector;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Assets.Scripts.Views
{
    public class IndicatorViewMediator : Mediator
    {
        [Inject] public IndicatorView view { get; set; }
        

        public override void OnRegister()
        {
            base.OnRegister();
       

        }

        public override void OnRemove()
        {
            base.OnRemove();
        }


    }
}
