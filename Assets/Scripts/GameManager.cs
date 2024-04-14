using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] float _duration;
    [SerializeField] float _timer;
    [SerializeField] Canvas _canvas;
    [SerializeField] Emplacement[] _emplacements;
    [SerializeField] Ingredient[] _ingredients;
    [SerializeField] Button _invokeButton;
    [SerializeField] GameObject _ingredientsContainer;
    [SerializeField] TextMeshProUGUI _timerTxt;
    [SerializeField] MenuManager _menuManager;
    [SerializeField] DialogManager _dialogManager;
    [SerializeField] InvocationManager _invocationManager;

    GAMESTATE _state;

    private void Awake()
    {
        _invokeButton.interactable = false;
        _invokeButton.GetComponent<RectTransform>().localScale = Vector3.zero;
    }

    private void Start()
    {
        Menu();
    }

    private void FixedUpdate()
    {
        if(_state == GAMESTATE.PLAY)
        {
            if(_timer >= 1)
            {
                _timer -= Time.deltaTime;
                _timerTxt.SetText( ((int)_timer).ToString());

                if(_timer < 11)
                {
                    _timerTxt.color = Color.red;
                }
            }
            else
            {
                SetGameState(GAMESTATE.TIMEREND);
                _InvokeFail();
                
            }
        }

        if(_state == GAMESTATE.INIT && Input.GetKeyUp("space"))
        {
            OnIntroEnded();
        }
        
    }

    public void OnIntroEnded()
    {
        Play();
    }

    void Play()
    {
        SetGameState(GAMESTATE.DIALOG);
        _menuManager.GamePanel();

        string[] sentences = new List<string>() { "Invoooque moooiii" }.ToArray();

        _dialogManager.ShowDialog(sentences, Color.red, () =>
        {
            SetGameState(GAMESTATE.PLAY);
        });
    }

    private void Menu()
    {
        SetGameState(GAMESTATE.MENU);
        _menuManager.MenuPanel();
        _DisableInteractions();
    }

    private void Pause()
    {
        SetGameState(GAMESTATE.PAUSE);
        _DisableInteractions(); 
    }

    private void Resume()
    {
        SetGameState(GAMESTATE.PLAY);
        _EnableInteractions();
    }

    public void OnEmplacementUsed(Emplacement emplacement)
    {
        _ConfigureInvokeButton();
    }

    public void OnEmplacementEmpty(Emplacement emplacement)
    {
        _ConfigureInvokeButton();
    }

    public void OnPlayButtonClicked()
    {
        Init();
        _menuManager.IntroPanel();
    }

    public void OnCreditsButtonClicked()
    {
        _menuManager.CreditPanel();
    }

    public void CastInvoke()
    {
        _DisableInteractions();

        if (_emplacements.Count(e => e.isOk()) == _emplacements.Length)
        {
            _InvokeSuccess();
        }
        else
        {
            _InvokeFail();
        }
    }

    private bool _CanCast()
    {
        return _emplacements.Count(e => !e.isEmpty()) == _emplacements.Length;
    }

    private void _ConfigureInvokeButton()
    {
        if (_CanCast())
        {
            _invokeButton.GetComponent<RectTransform>().DOScale(1f, 0.8f).SetEase(Ease.OutBounce)
            .OnComplete(() =>
            {
                _invokeButton.interactable = true;
                _invokeButton.GetComponent<RectTransform>().DOScale(1.02f, 1f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo).SetDelay(0.5f);
            });
        }
        else
        {
            _invokeButton.GetComponent<RectTransform>().DOScale(0f, 0.2f).SetEase(Ease.OutExpo).OnComplete(() =>
            {
                _invokeButton.GetComponent<RectTransform>().localScale = Vector3.zero;
                _invokeButton.GetComponent<RectTransform>().DOKill();
            });

            _invokeButton.interactable = false;
        }
    }

    private void _InvokeSuccess()
    {
        _Victory();
    }

    private void _InvokeFail()
    {
        SetGameState(GAMESTATE.INVOKE_FAIL);
    }

    private void _Victory()
    {
        SetGameState(GAMESTATE.VICTORY);
        _menuManager.DefeatPanel();
    }

    private void _Defeat()
    {
        SetGameState(GAMESTATE.DEFEAT);
        _menuManager.DefeatPanel();
    }

    private void SetGameState(GAMESTATE gameState)
    {
       _state = gameState;
    }

    private void _DisableInteractions()
    {
        //foreach (var item in _emplacements)
        //{
        //    item.DisableInteractions();
        //}

        //foreach (var item in _ingredients)
        //{
        //    item.DisableInteractions();
        //}
    }

    private void _EnableInteractions()
    {
        //foreach (var item in _emplacements)
        //{
        //    item.EnableInteractions();
        //}

        //foreach (var item in _ingredients)
        //{
        //    item.EnableInteractions();
        //}
    }

    private void Init()
    {
        SetGameState(GAMESTATE.INIT);

        _timer = _duration;

        GridLayoutGroup gridLayoutGroup = _ingredientsContainer.GetComponent<GridLayoutGroup>();
        gridLayoutGroup.enabled = true;

        foreach (var item in _ingredients)
        {
            item.GetComponent<Drag>().setCanvas(_canvas);
            Instantiate(item, gridLayoutGroup.transform);
        }

        gridLayoutGroup.enabled = false;

        //_ingredients.
    }
}
