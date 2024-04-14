using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] CanvasGroup supportText;
    [SerializeField] Button button;

    CanvasGroup _canvasGroup;
    string[] introSentences = new List<string>() { "VOUS �tes une sorciere !", 
        "Et il est temps de faire un rapport a votre boss de tous les exploits que vous avez fait", 
        "Malheureusement, votre �tag�re d'ingr�dients vous est tomb�e dessus et depuis vous ne vous rappelez plus comment l'invoquer", 
        "Habitu�e a cet incident, vous avez �crit quelques notes pour vous rappeler de l'ordre des ingr�dients � placer sur le cercle !", 
        "D�p�chez vous, votre boss s'impatiente !" }.ToArray();
    int _currentSentencesIndex = 0;
    bool _introEnded = false;

    private void Awake()
    {
        _canvasGroup = text.GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0f;

        button.GetComponent<CanvasGroup>().alpha = 0;
        button.interactable = false;
    }

    void OnEnable()
    {
        _introEnded = false;
        button.GetComponent<CanvasGroup>().alpha = 0;
        button.interactable = false;
        supportText.alpha = 0f;

        _readSentence(0);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) && !_introEnded)
        {
            _canvasGroup.DOFade(0f, 0.8f).OnComplete(() =>
            {
                _readSentence(_currentSentencesIndex + 1);
            });
        }
    }


    void _readSentence(int sentenceIndex)
   {
        supportText.DOKill();
        supportText.alpha = 0;

        _currentSentencesIndex = sentenceIndex;

        if(_currentSentencesIndex < introSentences.Length)
        {
            text.text = introSentences[sentenceIndex];
            _canvasGroup.DOFade(1f, 2f);
            supportText.DOFade(1, 2f).SetDelay(4f).SetLoops(-1, LoopType.Yoyo);
        }
        else
        {
            _introEnded = true;
            button.GetComponent<CanvasGroup>().DOFade(1f, 0.8f).OnComplete(() =>
            {
                button.interactable = true;
                button.GetComponent<RectTransform>().DOScale(1.02f, 0.8f).SetEase(Ease.InOutElastic).SetLoops(-1, LoopType.Yoyo);
            });
        }
    }
}
