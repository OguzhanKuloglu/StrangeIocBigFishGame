using Assets.Scripts.Data.Vo;
using Assets.Scripts.Enums;
using Assets.Scripts.Model;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Assets.Scripts.Views
{
    public class TestMediator : Mediator
    {
        [Inject] public TestView view{ get; set;}
        
        


        public override void OnRegister()
        {
            base.OnRegister();
            view.onTestEvent += OnTestEvent;
        }

        public override void OnRemove()
        {
            base.OnRemove();
            view.onTestEvent -= OnTestEvent;
        }

        public void OnTestEvent(string value)
        {
            Debug.Log("*** TEST MEDIATOR START : "+value);
          
            
        }
    }    
}

