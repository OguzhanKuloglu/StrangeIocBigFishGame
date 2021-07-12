using Assets.Scripts.Data.Vo;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Model
{
 
    public class SkillModel : ISkillModel
    {
        private CD_SkillData _skillData;

        public CD_SkillData SkillData
        {
            get
            {
                if (_skillData == null)
                    OnPostConstruct();
                return _skillData;
            }
            set { }
        }
        
        [PostConstruct]
        public void OnPostConstruct()
        {
            _skillData = Resources.Load<CD_SkillData>("Data/SkillData");
        }

        #region Func

        public SkillVo GetSkill(SkillType type)
        {
            return _skillData.List[type];
        }
        #endregion
    }   
}
