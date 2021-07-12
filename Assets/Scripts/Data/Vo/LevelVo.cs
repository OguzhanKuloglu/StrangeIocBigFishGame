using UnityEngine;
using CameraType = Assets.Scripts.Enums.CameraType;

namespace Assets.Scripts.Data.Vo
{
    [System.Serializable]
    public class LevelVo
    {
        public CameraType CameraType;
        public GameObject Environment;
        public int EnemyCount;
        public int FishMealCount;
        public Vector2 MapSize;
        public float Time;
    }
}