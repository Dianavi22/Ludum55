using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Ingredient : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private INGREDIENT_TYPE _type;
    [SerializeField] public Sprite sprite;
    [SerializeField] CanvasGroup _noteCanvas;

    private Drag _drag;
    private Image _image;
    private bool _inUse = false;
    private bool _hovered;
    private bool _onNote;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _drag = GetComponent<Drag>();

        _image.sprite = sprite;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) & _noteCanvas != null)
        {
            if (_hovered && !_onNote)
            {
                Debug.Log("SHOW NOTE");
                _noteCanvas.alpha = 1f;
                _onNote = true;
            }
            else
            {
                Debug.Log("HIDE NOTE");
                _noteCanvas.alpha = 0f;
                _onNote = false;
            }

            
        }
    }
    public void setUse(bool isInUse)
    {
        _inUse = isInUse;
        if (isInUse)
        {
            _image.color = Color.gray;
            _drag.DisableDrag();
        }
        else
        {
            _image.color = Color.white;
            _drag.EnableDrag();
        }
        _drag.BackToDefaultPos();   
    }

    public INGREDIENT_TYPE getType()
    {
        return _type;
    }

    public bool isUsed()
    {
        return _inUse;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            Emplacement emplacement = eventData.pointerDrag.GetComponent<Emplacement>();
            if ( emplacement && _inUse && _type == emplacement.getIngredientInSlot().getType() && emplacement.GetComponent<Drag>().CanDrag() )
            {
                emplacement.emptyEmplacement();
                setUse(false);
            }
            else
            {
                eventData.pointerDrag.GetComponent<Drag>().BackToDefaultPos();
            }
        }
    }

    private void _hoverStart()
    {
        Debug.Log("HOVER START");
        _hovered = true;
        transform.DOScale(1.1f, 0.25f);
    }
    private void _hoverEnd()
    {
        Debug.Log("HOVER END");
        _hovered = false;
        transform.DOScale(1f, 0.25f);
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        _hoverStart();
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        _hoverEnd();
    }
}
