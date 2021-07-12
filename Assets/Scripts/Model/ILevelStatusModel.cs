
using Assets.Scripts.Data.Vo;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Model
{
    public interface ILevelStatusModel
    {
        RD_LevelStatusData LevelStatusData { get; set; }
        int GetLevelStatusCount();
        void AddLevelStatus(LevelDataVo vo);
        LevelDataVo GetLevelStatus(int index);
        void SetLevelStatus(int index,LevelDataVo vo);
        void SetTotalPoint();
        int GetTotalPoint();
    }   
}
