using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Ingredient : MonoBehaviour, IDropHandler
{
    [SerializeField] INGREDIENT_TYPE _type;
    [SerializeField] public Sprite sprite;

    private Drag _drag;
    private Image _image;
    private bool _inUse = false;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _drag = GetComponent<Drag>();
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
            if ( emplacement && _inUse && _type == emplacement.getIngredientTypeInSlot() )
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
}
