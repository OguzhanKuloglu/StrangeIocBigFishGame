using strange.extensions.context.impl;
using zModules.FirebaseRealtimeDatabase.Context;

namespace zModules.FirebaseRealtimeDatabase.Root
{
    public class FirebaseDBRoot : ContextView
    {
        void Awake()
        {
            context = new FirebaseDBContext(this);
        }
    }

}
