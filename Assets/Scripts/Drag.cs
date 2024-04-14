using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas _canvas;
    private Vector2 _defaultPos;
    private bool _canDrag = true;
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _defaultPos = _rectTransform.anchoredPosition;
    }

    public void setCanvas(Canvas canvas)
    {
        _canvas = canvas;
    }

    public void EnableDrag()
    {
        _canDrag = true;
    }

    public void DisableDrag()
    {
        _canDrag = false;
    }

    public void BackToDefaultPos()
    {
        _rectTransform.anchoredPosition = _defaultPos;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(_canDrag)
        {
            _canvasGroup.alpha = .6f;
            _canvasGroup.blocksRaycasts = false;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_canDrag)
        {
            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;  
        }
    }
}
