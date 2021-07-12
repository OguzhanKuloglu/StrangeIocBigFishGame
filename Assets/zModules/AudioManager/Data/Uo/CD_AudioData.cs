using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using zModules.AudioManager.Data.Vo;
using zModules.AudioManager.Enums;

[CreateAssetMenu(menuName = "AudioManager/Data/AudioData",order = 0)]
public class CD_AudioData : SerializedScriptableObject
{
    public Dictionary<AudioTypes,AudioVo> list = new Dictionary<AudioTypes,AudioVo>();
}