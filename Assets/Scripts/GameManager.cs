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
    [SerializeField] GameObject _ingredientsZone;
    [SerializeField] GameObject _emplacementsZone;
    [SerializeField] Note[] _notes;
    [SerializeField] Button _invokeButton;
    [SerializeField] GameObject _ingredientsContainer;
    [SerializeField] TextMeshProUGUI _timerTxt;
    [SerializeField] MenuManager _menuManager;
    [SerializeField] DialogManager _dialogManager;
    [SerializeField] InvocationManager _invocationManager;
    [SerializeField] GameObject _invisibleZone;
    [SerializeField] GameObject _explications;
    [SerializeField] SoundManager _soundManager;
    [SerializeField] SfxManager _sfxManager;
    [SerializeField] ShakyCame _cam;

    GAMESTATE _state;

    private void Awake()
    {
        _invokeButton.interactable = false;
        _invokeButton.GetComponent<RectTransform>().localScale = Vector3.zero; 
        _invisibleZone.SetActive(true);
        _explications.SetActive(false);
    }

    private void Start()
    {
        //Menu();
        _Defeat();
        //Game();
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            if(_state == GAMESTATE.GAME)
            {
                Pause();
            }
            else if(_state == GAMESTATE.PAUSE)
            {
                Resume();
            }
        }
    }

    private void FixedUpdate()
    {
        if(_state == GAMESTATE.GAME)
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
                _Defeat();
                
            }
        }
    }

    public void OnIntroEnded()
    {
        Play();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    void Play()
    {
        SetGameState(GAMESTATE.DIALOG);
        _menuManager.GamePanel();

        string[] sentences = new List<string>() { "Invoooque moooiii" }.ToArray();

        _dialogManager.ShowDialog(sentences, Color.red, () =>
        {
            _explications.SetActive(true);
        });
    }

    private void Menu()
    {
        SetGameState(GAMESTATE.MENU);
        _menuManager.MenuPanel();
        _invisibleZone.SetActive(true);
        _soundManager.DisablePendulum();
    }

    private void Pause()
    {
        SetGameState(GAMESTATE.PAUSE);
        _menuManager.PausePanel();
        _invisibleZone.SetActive(true);
        _soundManager.DisablePendulum();
    }

    private void Resume()
    {
        Game();
    }

    private void Game(bool start = false)
    {
        SetGameState(GAMESTATE.GAME);
        if(!start) _menuManager.GamePanel();
        _invisibleZone.SetActive(false);
        _soundManager.ActivePendulum();
    }

    public void OnExplicationOkButtonClicked()
    {
        Game(start: true);
        _explications.SetActive(false);
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
        _invisibleZone.SetActive(true);
    }

    public void OnCreditsButtonClicked()
    {
        _menuManager.CreditPanel();
    }

    public void OnResumeButtonClicked()
    {
        Resume();
    }

    public void OnMenuButtonClicked()
    {
        Menu();
    }
    public void OnRestartButtonClicked()
    {
        Init();
        Play();
    }

    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }

    public void CastInvoke()
    {
        SetGameState(GAMESTATE.INVOKE);
        _invisibleZone.SetActive(true);
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
        _invokeButton.gameObject.SetActive(false);
        _ingredientsZone.SetActive(false);
        _emplacementsZone.SetActive(false);

        _invocationManager.InvocationSuccess(() =>
        {
            _dialogManager.ShowDialog(new List<string>() { "Bonjour..." }.ToArray(), Color.red, () =>
            {
                _Victory(); 
                _invocationManager.ResetInvocation();
            });
        });
    }

    private void _InvokeFail()
    {
        _cam.Shake(0.5f, 0.04f);
        _invocationManager.InvocationFail(() =>
        {
            _dialogManager.ShowDialog(new List<string>() { "On non, ce n'est pas mon patron..." }.ToArray(), Color.white, () =>
            {
                ResetEmplacements();
                Game();
                _invocationManager.ResetInvocation();
            });
        });
    }

    private void _Victory()
    {
        SetGameState(GAMESTATE.VICTORY);
        _menuManager.VictoryPanel();
        _invisibleZone.SetActive(true);

        _soundManager.DisablePendulum();
    }

    private void _Defeat()
    {
        SetGameState(GAMESTATE.DEFEAT);
        _ingredientsZone.SetActive(false);
        _emplacementsZone.SetActive(false);
        _dialogManager.ShowDialog(new List<string>() { "Oh non..." }.ToArray(), Color.white, () =>{});

        _cam.Shake(5f, 0.1f);

        _invocationManager.Defeat(() =>{});

        _soundManager.DisablePendulum();
    }

    private void SetGameState(GAMESTATE gameState)
    {
       _state = gameState;
    }

    private void Init()
    {
        SetGameState(GAMESTATE.INIT);

        _timer = _duration;
        _timerTxt.text = "";
        
        ResetEmplacements();
    }

    private void ResetEmplacements()
    {
        foreach (var item in _emplacements.Where(e => !e.isEmpty()))
        {
            item.emptyEmplacement();
        }

        foreach (var item in _ingredients.Where(i => i.isUsed()))
        {
            item.setUse(false);
        }
    }
}
