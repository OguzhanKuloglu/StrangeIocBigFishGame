using strange.extensions.context.impl;
using zModules.FirebaseAnalytics.Context;

namespace zModules.FirebaseAnalytics.Root
{
    public class FirebaseAnalyticsRoot : ContextView
    {
        void Awake()
        {
            context = new FirebaseAnalyticsContext(this);
        }
    }

}
