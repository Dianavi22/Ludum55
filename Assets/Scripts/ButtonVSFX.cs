using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonVSFX : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] SfxManager sfxManager;

    [SerializeField] bool disableSound = false;
    [SerializeField] bool disableAnimation = false;

    RectTransform rectTransform;
    Vector3 baseScale;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        baseScale = rectTransform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
       if(!disableAnimation) rectTransform.DOScale(baseScale * 1.1f, 0.25f);
       if(!disableSound) sfxManager.HoverButton();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
       if(!disableSound) sfxManager.SelectButton();
    }
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
      if(!disableAnimation) rectTransform.DOScale(baseScale, 0.25f);
    }
}
