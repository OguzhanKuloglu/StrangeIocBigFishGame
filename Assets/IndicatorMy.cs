using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorMy : MonoBehaviour
{
    public Transform trCamera;
    public Transform Target;
    public Transform Player;
    public GameObject Arrow;
    public bool isIndicatorSet;

    public void SetTarget(Transform target, Transform player)
    {
        Target = target;
        Player = player;
        Arrow = this.transform.GetChild(0).gameObject;
        trCamera = Camera.main.transform;
    }

    float difx = 0f;
    float dify = 0f;
    private void Update()
    {

        if (isIndicatorSet)
        {
            difx = trCamera.position.x - Player.position.x;
            dify = trCamera.position.y - Player.position.y;
            //difx = difx < 0 ? -difx : difx + 3;
            //Arrow.transform.position = new Vector3(difx , 0,0);
            this.transform.position = Player.transform.position;
            var dir = Target.transform.position - Player.transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


            if (Vector2.Distance(Target.transform.position, Player.transform.position) > 5)
            {
                Arrow.SetActive(true);
            }
            else
            {
                Arrow.SetActive(false);
            }

        }

    }


    public void StartIndicator()
    {
        isIndicatorSet = true;
    }

    public void StopIndicator()
    {
        isIndicatorSet = false;
    }
}
