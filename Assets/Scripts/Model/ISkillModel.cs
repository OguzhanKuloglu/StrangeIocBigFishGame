
using Assets.Scripts.Data.Vo;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Model
{
    public interface ISkillModel
    {
        CD_SkillData SkillData { get; set; }
        SkillVo GetSkill(SkillType type);
    }   
}
