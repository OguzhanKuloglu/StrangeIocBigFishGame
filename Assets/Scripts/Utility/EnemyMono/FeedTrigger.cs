using Assets.Scripts.Views;
using Constans;
using UnityEngine;

public class FeedTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    private EnemyView view;
    void Start()
    {
        view = GetComponentInParent<EnemyView>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == Tags.Feed || other.tag == Tags.Bad || other.tag == Tags.Bonus || other.tag == Tags.Enemy || other.tag == Tags.InkSkill)
            view.FeedTrigger(other);
        if(other.tag == Tags.ElectricSkill)
            view.SkillTrigger(other);
    }

    /* private void OnTriggerStay2D(Collider2D other)
     {
         if(other.tag == Tags.ElectricSkill)
             view.SkillTrigger(other);
     }*/

    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.tag == Tags.InkSkill)
        {
            view.TriggerExit(other);
        }
    }
}
