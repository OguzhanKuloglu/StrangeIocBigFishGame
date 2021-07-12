using Assets.Scripts.Context;
using strange.extensions.context.impl;

namespace Assets.Scripts.Root
{
    public class GameRoot_TestOguz : ContextView
    {
        void Awake()
        {
            context = new GameContext_TestOguz(this);
        }
    }

}
