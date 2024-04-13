using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Emplacement : MonoBehaviour, IDropHandler
{
    [SerializeField] Sprite _emptySprite;
    [SerializeField] INGREDIENT_TYPE _accept;

    private Image _image;
    private Drag _drag;
    private bool _isEmpty;
    private INGREDIENT_TYPE _ingredientTypeInSlot;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _image.sprite = _emptySprite;
        _drag = GetComponent<Drag>();
        _drag.DisableDrag();

        _isEmpty = true;
    }

    private void useEmplacement(Ingredient ingredient)
    {
        if (_isEmpty)
        {
            _image.sprite = ingredient.sprite;
            _isEmpty = false;
            _drag.EnableDrag();
            _ingredientTypeInSlot = ingredient.getType();

            ingredient.setUse(true);
        }

    }

    public void hoverStart()
    {
        if (!_isEmpty)
        {
            transform.DOScale(1.1f, 0.25f);
        }
        
    }
    public void hoverEnd()
    {

        if (!_isEmpty)
        {
            transform.DOScale(1f, 0.25f);
        }
        
    }

    public void emptyEmplacement()
    {
        if (!_isEmpty)
        {
            _isEmpty = true;
            _image.sprite = _emptySprite;
            _drag.DisableDrag();
            _drag.BackToDefaultPos();   
            _ingredientTypeInSlot = INGREDIENT_TYPE.NONE;
        }
    }

    public bool isOk()
    {
        return _accept == _ingredientTypeInSlot;
    }

    public INGREDIENT_TYPE getIngredientTypeInSlot()
    {
        return _ingredientTypeInSlot;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            if (eventData.pointerDrag.GetComponent<Ingredient>())
            {
                useEmplacement(eventData.pointerDrag.GetComponent<Ingredient>());
            }
            else
            {
                eventData.pointerDrag.GetComponent<Drag>().BackToDefaultPos();
            }
        }
    }
}
