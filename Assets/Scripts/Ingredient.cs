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

    private Drag _drag;
    private Image _image;
    private bool _inUse = false;
    private bool _interactable;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _drag = GetComponent<Drag>();

        _image.sprite = sprite;
        _interactable = true;
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

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            Emplacement emplacement = eventData.pointerDrag.GetComponent<Emplacement>();
            if ( emplacement && _inUse && _type == emplacement.getIngredientInSlot().getType() )
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
        if (!_inUse)
        {
            transform.DOScale(1.1f, 0.25f);
        }

    }
    private void _hoverEnd()
    {

        if (!_inUse)
        {
            transform.DOScale(1f, 0.25f);
        }

    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        _hoverStart();
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        _hoverEnd();
    }

    public void DisableInteractions()
    {
        _interactable = false;
        _drag.DisableDrag();
    }
    public void EnableInteractions()
    {
        _interactable = true;
        if (!_inUse)
        {
            _drag.EnableDrag();
        }
    }
}
