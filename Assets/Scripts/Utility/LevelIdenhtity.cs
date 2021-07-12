using System.Collections.Generic;
using Assets.Scripts.Views;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelIdenhtity : MonoBehaviour
{
    public int LevelIndex;
    public TextMeshProUGUI LevelNumber;
    public List<GameObject> State;
    public bool isActive = false;
    public Image[] Stars;
    public Sprite ActiveStar;
    public Sprite NoneActiveStar;

    public void SetActive()
    {
        isActive = true;
    }
    public void SetState(int state)
    {
        for (int j = 0; j < State.Count; j++)
        {
            if (state == j)
                State[j].SetActive(true);
            else
                State[j].SetActive(false);
        }
    }

    public void SetStars(int count)
    {
        if(count == 0)
            return;
        for (int i = 3; i > 0; i--)
        {
            Stars[3-i].sprite = ActiveStar;
            if((3-i) == count-1)
                return;
        }
    }


    public void Selected()
    {
        if(!isActive)
            return;
        LevelMapManager manager = GetComponentInParent<LevelMapManager>();
        manager.SelectedChange(LevelIndex);
        SetState(2);
        
    }
    public void PlayButton()
    {
        LevelMapManager manager = GetComponentInParent<LevelMapManager>();
        manager.PlayButton(LevelIndex);
    }
 
}
