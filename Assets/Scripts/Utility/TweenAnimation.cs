using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class TweenAnimation : MonoBehaviour
{

    public bool isScaleAnimation;
    public bool isTransformAnimation;

    [ShowIf("isTransformAnimation")]
    public Vector2 StartPosition;
    [ShowIf("isTransformAnimation")]
    public Vector2 EndPosition;
   
    
    [ShowIf("isScaleAnimation")]
    public float StartScale;
    [ShowIf("isScaleAnimation")]
    public float EndScale;

    public float Duration = 0f;
    public Ease Ease;

    private RectTransform transform;
    void Start()
    {
        transform = GetComponent<RectTransform>();
        GoEnd();
    }

    public void GoEnd()
    {
        if (isScaleAnimation)
            transform.DOScale(EndScale, Duration).SetEase(Ease).OnComplete(GoStart);
        if(isTransformAnimation)
            transform.DOAnchorPos(EndPosition, Duration).SetEase(Ease).OnComplete(GoStart);
      
    }

    public void GoStart()
    {
        if (isScaleAnimation)
            transform.DOScale(StartScale, Duration).SetEase(Ease).OnComplete(GoEnd);
        if(isTransformAnimation)
            transform.DOAnchorPos(StartPosition, Duration).SetEase(Ease).OnComplete(GoEnd);

    }
    private void OnDisable()
    {
        transform.DOKill();
    }
}
