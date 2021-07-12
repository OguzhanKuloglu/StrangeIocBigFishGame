using strange.extensions.signal.impl;
using zModules.AudioManager.Enums;

namespace zModules.AudioManager.Signals
{
    public class AudioSignals
    {
        public Signal<int,AudioTypes> Play = new Signal<int,AudioTypes>();
        public Signal<int> Stop = new Signal<int>();
        public Signal<int> Clear = new Signal<int>();
        public Signal<bool> MuteMusic = new Signal<bool>();
        public Signal<bool> MuteSfx = new Signal<bool>();
    }
}