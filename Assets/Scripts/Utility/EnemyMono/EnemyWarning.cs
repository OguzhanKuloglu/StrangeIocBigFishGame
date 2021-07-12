using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Views;
using Constans;
using UnityEngine;

public class EnemyWarning : MonoBehaviour
{
    // Start is called before the first frame update
    public EnemyView view;

    private void Start()
    {
        view = GetComponentInParent<EnemyView>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == Tags.Enemy || other.tag == Tags.Player)
        {
            view.AddEnemyWarning(other);
        }

        if (other.tag == "TutorialObject")
        {
            
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == Tags.Enemy || other.tag == Tags.Player)
        {
            view.AddEnemyWarning(other);
        }
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == Tags.Enemy || other.tag == Tags.Player)
            view.RemoveEnemyWarning(other);
    }
    
   
}
