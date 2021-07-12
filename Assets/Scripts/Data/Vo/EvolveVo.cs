using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Data.Vo
{
    [System.Serializable]
    public class EvolveVo
    {
        public int MinLevel; // 0
        public int MaxLevel; // 9
        public Sprite FishIcon;
        public GameObject Prefab;
        public GameObject PrefabUnicorn;
    }
}