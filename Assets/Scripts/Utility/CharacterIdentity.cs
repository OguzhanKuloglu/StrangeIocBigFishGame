using Assets.Scripts.Data.Vo;
using DG.Tweening;
using UnityEngine;

public class CharacterIdentity : MonoBehaviour
{
    public CharacterVo Vo;

    public void UnicornSkillEffect()
    {
        float diff = Vo.ProccessValue;
        Vo.ProccessValue = Mathf.Floor(Vo.ProccessValue / 2f) < 0f ? 0f : Mathf.Floor(Vo.ProccessValue / 2f);
        diff -= Vo.ProccessValue;
        Vo.Scale = (diff*0.05f) < 1f ? 1f : (diff*0.05f);
        transform.DOScale(Vo.Scale, 0.2f);
    }
}
