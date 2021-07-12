using strange.extensions.command.impl;
using UnityEngine;
using zModules.FirebaseAnalytics.Service;

namespace Runtime.Controller
{
    public class InitFirebaseCommand : Command
    {
        [Inject] public IFirebaseAnalyticsService FbaseAnalytics { get; set; }
        public override void Execute()
        {
            Debug.Log("/InitFirebaseCommand/ --> Execute");
            FbaseAnalytics.Initialize();
        }
    }
}