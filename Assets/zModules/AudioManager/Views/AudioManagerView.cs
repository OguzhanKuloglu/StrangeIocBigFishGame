using System.Collections.Generic;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.Audio;
using zModules.AudioManager.Enums;

namespace zModules.AudioManager.Views
{
    public class AudioManagerView : View
    {
        public CD_AudioData AudioData;
        public List<AudioSource> Layers;
        public AudioMixer Mixer;
        public void Play(int Layer,AudioTypes type)
        {
            Clear(Layer);
            Layers[Layer].clip = AudioData.list[type].Clip;
            Layers[Layer].loop = AudioData.list[type].IsLoop;
            Layers[Layer].volume = AudioData.list[type].Value;
            Layers[Layer].Play();
        }
        
        public void Stop(int Layer)
        {
            Layers[Layer].Stop();
        }
        
        public void Clear(int Layer)
        {
            Layers[Layer].Stop();
            Layers[Layer].clip = null;
            Layers[Layer].loop = false;
            Layers[Layer].volume = 0f;
        }
        
        public void MuteMusic(bool value)
        {
            if(value)
                Mixer.SetFloat("MusicVolume", -80f);
            if(!value)
                Mixer.SetFloat("MusicVolume", 0f);
        }
        
        public void MuteSFX(bool value)
        {
            if(value)
                Mixer.SetFloat("SFXVolume", -80f);
            if(!value)
                Mixer.SetFloat("SFXVolume", 0f);
        }
    }
    
}