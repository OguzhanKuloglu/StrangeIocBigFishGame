using Assets.Scripts.Data.Vo;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Model
{
 
    public class EnemyModel : IEnemyModel
    {
        private CD_EnemyData _enemyData;

        public CD_EnemyData EnemyData
        {
            get
            {
                if (_enemyData == null)
                    OnPostConstruct();
                return _enemyData;
            }
            set { }
        }
        
        [PostConstruct]
        public void OnPostConstruct()
        {
            _enemyData = Resources.Load<CD_EnemyData>("Data/EnemyData");
        }

    }   
}
