using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Data.Vo
{
    [System.Serializable]
    public class FeedVo
    {
        public FeedType Type;
        public ContentType ContentType;
        public float EarningProcess;
        public float EarningScore;
        public GameObject Prefab;
    }
}