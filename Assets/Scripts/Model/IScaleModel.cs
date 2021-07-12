using Assets.Scripts.Enums;

namespace Assets.Scripts.Model
{
    public interface IScaleModel
    {
        CD_ScaleData ScaleData { get; set; }
        float GetScaleFactor(FeedType feedType, CharacterType characterType);
    }   
}
