using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Emplacement : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] GameManager _gameManager;
    [SerializeField] Sprite _emptySprite;
    [SerializeField] INGREDIENT_TYPE _accept;
    [SerializeField] ParticleSystem _dropParticle;
    [SerializeField] ParticleSystem _consumeParticle;

    private Image _image;
    private Drag _drag;
    private bool _isEmpty = true;
    private Ingredient _ingredient;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _image.sprite = _emptySprite;
        _drag = GetComponent<Drag>();
        _drag.DisableDrag();
    }

    private void useEmplacement(Ingredient ingredient)
    {
        if (_isEmpty)
        {
            _image.sprite = ingredient.sprite;
            _isEmpty = false;
            _drag.EnableDrag();
            _ingredient = ingredient;

            ingredient.setUse(true);

            _gameManager.OnEmplacementUsed(this);
        }

    }

    private void moveEmplacement(Emplacement emplacement)
    {
        if (_isEmpty)
        {
            _image.sprite = emplacement.getIngredientInSlot().sprite;
            _isEmpty = false;
            _drag.EnableDrag();
            _ingredient = emplacement.getIngredientInSlot();

            emplacement.emptyEmplacement(true);
        }

    }

    public void emptyEmplacement(bool moved = false)
    {
        if (!_isEmpty)
        {
            _isEmpty = true;
            _image.sprite = _emptySprite;
            _drag.DisableDrag();
            _drag.BackToDefaultPos();

            if (!moved)
            {
                _ingredient.setUse(false);
                _ingredient = null;
            }

            _gameManager.OnEmplacementEmpty(this);
        }
    }

    public void consumeEmplacement()
    {
        _consumeParticle.Play();
        _image.sprite = _emptySprite;
    }

    public bool isOk()
    {
        Debug.Log(_accept + " " + _ingredient);
        if (_ingredient)
        {
            Debug.Log(_accept+" "+_ingredient.getType());
            return _accept == _ingredient.getType();
        }
        return false;
    }

    public bool isEmpty()
    {
        return _isEmpty;
    }

    public Ingredient getIngredientInSlot()
    {
        return _ingredient;
    }
    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            if (eventData.pointerDrag.GetComponent<Ingredient>() && eventData.pointerDrag.GetComponent<Drag>().CanDrag())
            {
                useEmplacement(eventData.pointerDrag.GetComponent<Ingredient>());
                _dropParticle.Play();
            } else if (eventData.pointerDrag.GetComponent<Emplacement>() && _isEmpty && eventData.pointerDrag.GetComponent<Drag>().CanDrag())
            {
                moveEmplacement(eventData.pointerDrag.GetComponent<Emplacement>());
                _dropParticle.Play();
            }
            else
            {
                eventData.pointerDrag.GetComponent<Drag>().BackToDefaultPos();
            }
        }
    }
    private void _hoverStart()
    {
        if (!_isEmpty)
        {
            transform.DOScale(1.1f, 0.25f);
        }

    }
    private void _hoverEnd()
    {

        if (!_isEmpty)
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

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            emptyEmplacement();
        }
    }
}
