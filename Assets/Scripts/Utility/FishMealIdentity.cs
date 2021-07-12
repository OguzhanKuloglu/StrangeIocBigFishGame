using System;
using Assets.Scripts.Data.Vo;
using UnityEngine;

public class FishMealIdentity : MonoBehaviour
{
    public FeedVo Vo;
    private Transform Parent;

    private void Start()
    {
        Parent = this.transform.parent;
    }

    public void GetFeed()
    {
        this.transform.parent = Parent;
        this.gameObject.SetActive(true);
    }
    public void TurnPool(Transform parent)
    {
        this.gameObject.SetActive(false);
        this.transform.parent = parent;
    }
}
