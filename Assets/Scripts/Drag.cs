using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] SfxManager _sfxManager;

    private Vector2 _defaultPos;
    private bool _canDrag = true;
    private bool _dragged = false;
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

    public bool CanDrag()
    {
        return _canDrag;
    }

    public void BackToDefaultPos()
    {
        _rectTransform.anchoredPosition = _defaultPos;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(_canDrag)
        {
            _dragged = true;
            _sfxManager.TakeItem();
            _canvasGroup.alpha = .6f;
            _canvasGroup.blocksRaycasts = false;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
       if(_dragged) _sfxManager.DropItem();
        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;
        _dragged = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_canDrag)
        {
            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;  
        }
    }
}
