using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvocationManager : MonoBehaviour
{
    [SerializeField] CanvasGroup panel;
    [SerializeField] RectTransform invocation;
    [SerializeField] Image invocationImage;
    [SerializeField] End end;

    private void Awake()
    {
        panel.alpha = 0f;
        panel.blocksRaycasts = false;
        invocation.localScale = Vector3.zero;
    }

    public void InvocationFail(Action afterInvocation)
    {
        Show(afterInvocation);
    }

    public void InvocationSuccess(Action afterInvocation)
    {
        end.PlayWin(afterInvocation);
    }

    public void Defeat(Action afterInvocation)
    {
        Show(afterInvocation);
    }

    public void ResetInvocation()
    {
        panel.DOKill();
        invocation.DOKill();
        panel.alpha = 0f;
        invocation.localScale = Vector3.zero;
    }

    private void Show(Action afterInvocation)
    {
        panel.DOFade(1f, 1f).OnComplete(() =>
        {
            invocation.DOScale(1, 0.8f).SetEase(Ease.InElastic).OnComplete(() =>
            {
                //invocation.DOShakePosition(0.5).SetLoops(4, LoopType.Yoyo);
                afterInvocation();
            });
        });
    }
}
