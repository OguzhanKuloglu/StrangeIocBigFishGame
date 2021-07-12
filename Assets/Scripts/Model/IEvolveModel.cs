
using Assets.Scripts.Data.Vo;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Model
{
    public interface IEvolveModel
    {
        CD_EvolveData EvolveData { get; set; }
        EvolveVo GetEvolve(EvolveType type);
    }   
}
