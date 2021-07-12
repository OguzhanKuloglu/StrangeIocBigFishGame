using Assets.Scripts.Enums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets.Scripts.Data.Vo
{
    [System.Serializable]
    public class CharacterVo
    {
        public CharacterType ChracterType;
        [ProgressBar(0,10)]
        public float ProccessValue;
        public float Scale;
        public GameObject Prefab;
    }
}