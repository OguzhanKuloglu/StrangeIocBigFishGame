using strange.extensions.context.impl;
using zModules.AudioManager.Context;

namespace zModules.AudioManager.Root
{
    public class AudioRoot : ContextView
    {
        void Awake()
        {
            context = new AudioContext(this);
        }
    }

}
