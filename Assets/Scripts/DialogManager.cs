using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class DialogManager : MonoBehaviour
{
    [SerializeField] CanvasGroup dialogContainer;
    [SerializeField] Image dialogBox;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] TypeSentence typeSentence;
    string[] _sentences;
    int _currentSentenceIndex = 0;
    bool _disableSound = false;
    Action _afterDialog;

    bool _onUse = false;

    private void Awake()
    {
        text.text = "";
        dialogContainer.alpha = 0;
        dialogContainer.blocksRaycasts = false;
    }
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space) && _onUse)
        {
            _nextSentence();
        }
    }

    public void ShowDialog(string[] sentences, Color dialogColor, Action afterDialog)
    {
        text.text = "";
        dialogContainer.alpha = 1;
        dialogBox.color = dialogColor;
        _sentences = sentences;
        _afterDialog = afterDialog;
        _onUse = true;
        dialogContainer.blocksRaycasts = true;
        _disableSound = dialogColor == Color.white;
        _readSentence();
    }

    public void HideDialog()
    {
        _reset();
    }

    void _readSentence()
    {
        if (_onUse)
        {
            if (_currentSentenceIndex < _sentences.Length)
            {
                typeSentence.WriteMachinEffect(_sentences[_currentSentenceIndex], text, 0.05f, _disableSound);
            }
            else
            {
                _afterDialog();
                _reset();
            }
        }
        
    }

    void _nextSentence()
    {
        _currentSentenceIndex = _currentSentenceIndex + 1;
        _readSentence();
    }

    void _reset()
    {
        dialogContainer.alpha = 0;
        _currentSentenceIndex = 0;
        _sentences = new string[0];
        _afterDialog = null;
        _onUse = false;
        dialogContainer.blocksRaycasts = false;
        _disableSound = false;
    }
}
