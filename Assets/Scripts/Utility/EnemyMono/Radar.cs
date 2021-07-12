using Assets.Scripts.Views;
using Constans;
using UnityEngine;

public class Radar : MonoBehaviour
{
    // Start is called before the first frame update

    public EnemyView view;

    private void Start()
    {
        view = GetComponentInParent<EnemyView>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == Tags.Feed)
            view.AddFeed(other.gameObject);
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == Tags.Feed)
            view.RemoveFeed(other.gameObject);
    }
}
