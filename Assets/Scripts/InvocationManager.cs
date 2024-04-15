using DG.Tweening;
using System;
using UnityEngine;

public class InvocationManager : MonoBehaviour
{
    [SerializeField] CanvasGroup panel;
    [SerializeField] End end;

    private void Awake()
    {
        panel.alpha = 0f;
        panel.blocksRaycasts = false;
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
        Show(() => { });
        end.PlayLoose();
    }

    public void ResetInvocation()
    {
        panel.DOKill();
        panel.alpha = 0f;
    }

    private void Show(Action afterInvocation)
    {
        panel.DOFade(1f, 1f).OnComplete(() =>
        {
            afterInvocation();
        });
    }
}
