using Assets.Scripts.Context;
using strange.extensions.context.impl;

namespace Assets.Scripts.Root
{
    public class UIRoot_TestOguzhan : ContextView
    {
        void Awake()
        {
            context = new UIContext_TestOguzhan(this);
        }
    }

}
