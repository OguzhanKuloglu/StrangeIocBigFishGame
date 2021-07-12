using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Views;
using Constans;
using UnityEngine;

public class PlayerFeedTrigger : MonoBehaviour
{
    public PlayerManager Pm;
    // Start is called before the first frame update
    void Start()
    {
        Pm = GetComponentInParent<PlayerManager>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == Tags.Feed || other.tag == Tags.Bad || other.tag == Tags.Bonus || other.tag == Tags.Enemy || other.tag == Tags.InkSkill)
            Pm.FeedTrigger(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if ( other.tag == Tags.InkSkill)
            Pm.OnExitTrigger(other);
    }
}
