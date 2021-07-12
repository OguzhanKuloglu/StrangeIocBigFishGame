using System.Collections.Generic;
using Sirenix.OdinInspector;
using strange.extensions.mediation.impl;
using UnityEngine;
using zModules.FirebaseRealtimeDatabase.Constant;
using zModules.FirebaseRealtimeDatabase.Data.Vo;

namespace zModules.FirebaseRealtimeDatabase.Views
{
    public class FirebaseDBMediator : Mediator
    {
        [Inject] public FirebaseDBManager view{ get; set;}
        [Inject] public FirebaseDBSignals FirebaseDBSignals { get; set; }

        public override void OnRegister()
        {
            base.OnRegister();
            FirebaseDBSignals.SendData.AddListener(OnSendData);
            FirebaseDBSignals.GetData.AddListener(OnGetData);
            InitDB();
            view.onDataLoaded += OnDataLoaded;
            view.onGeneretedUserId += OnGeneretedUserId;
        }

        public override void OnRemove()
        {
            base.OnRemove();
            FirebaseDBSignals.SendData.RemoveListener(OnSendData);
            FirebaseDBSignals.GetData.RemoveListener(OnGetData);
            view.onDataLoaded -= OnDataLoaded;
            view.onGeneretedUserId -= OnGeneretedUserId;

        }

        private void OnGeneretedUserId()
        {
            Debug.Log("*** GENERETED USER ID DB WRITE***");
            Dictionary<string, object> data = new Dictionary<string, object>();
            data[Leaderboard_Columns.UserName] = "Guest8098687";
            data[Leaderboard_Columns.TotalPoint] = 0;
                

            FirebaseDBSignals.SendData.Dispatch(new SenderVo()
            {
                Data = data,
                TableName = TableName.Leaderboard
            });
        }

        public void InitDB()
        {
            view.Init();
        }
       
        private void OnSendData(SenderVo vo)
        {
            view.PushRuntimeData(vo);
        }
        
        private void OnGetData(GetDataVo vo)
        {
            view.GetData(vo);
        }

        private void OnDataLoaded(Dictionary<string, List<DataResultVo>> value)
        {
            FirebaseDBSignals.DataLoaded.Dispatch(value);
        }

    }    
}

