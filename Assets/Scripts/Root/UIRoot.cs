using Assets.Scripts.Context;
using strange.extensions.context.impl;

namespace Assets.Scripts.Root
{
    public class UIRoot : ContextView
    {
        void Awake()
        {
            context = new UIContext(this);
        }
    }

}
