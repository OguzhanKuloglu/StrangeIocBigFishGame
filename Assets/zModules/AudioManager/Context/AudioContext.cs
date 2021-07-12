using strange.extensions.context.api;
using strange.extensions.context.impl;
using UnityEngine;
using zModules.AudioManager.Signals;
using zModules.AudioManager.Views;

namespace zModules.AudioManager.Context
{
    public class AudioContext : MVCSContext
    {
        public AudioContext (MonoBehaviour view) : base(view)
        {
        }

        public AudioContext (MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
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
