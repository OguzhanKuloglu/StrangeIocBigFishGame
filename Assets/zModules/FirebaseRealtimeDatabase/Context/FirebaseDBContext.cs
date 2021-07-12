using strange.extensions.context.api;
using strange.extensions.context.impl;
using UnityEngine;
using zModules.FirebaseRealtimeDatabase.Model;
using zModules.FirebaseRealtimeDatabase.Views;

namespace zModules.FirebaseRealtimeDatabase.Context
{
    public class FirebaseDBContext : MVCSContext
    {
        private FirebaseDBSignals _dBSignals;
        public FirebaseDBContext (MonoBehaviour view) : base(view)
        {
        }

        public FirebaseDBContext (MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
        {
        }

        protected override void mapBindings()
        {
            //*** Signals injection bind
          
            //*** Meditator & View bind

            //*** Command bind
            //commandBinder.Bind(_gameSignals.GameStarted).To<TestCommand>();
            

        }
    }
 
}
