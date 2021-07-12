using Runtime.Controller;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using UnityEngine;
using zModules.FirebaseAnalytics.Service;

namespace zModules.FirebaseAnalytics.Context
{
    public class FirebaseAnalyticsContext : MVCSContext
    {
        private FirebaseAnalyticsSignals _signals;
        public FirebaseAnalyticsContext (MonoBehaviour view) : base(view)
        {
        }

        public FirebaseAnalyticsContext (MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
        {
        }

        protected override void mapBindings()
        {
            injectionBinder.Bind<FirebaseAnalyticsSignals>().CrossContext().ToSingleton();
            _signals = injectionBinder.GetInstance<FirebaseAnalyticsSignals>();

            
            //commandBinder.Bind(_signals.InitFirebaseAnalytics).To<InitFirebaseCommand>();
        }

        public override void Launch()
        {
            base.Launch();
            _signals.InitFirebaseAnalytics.Dispatch();
        }
    }
 
}
