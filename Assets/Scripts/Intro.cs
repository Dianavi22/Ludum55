using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Button button;

    CanvasGroup _canvasGroup;
    string[] introSentences = new List<string>() { "VOUS êtes une sorciere !", 
        "Et il est temps de faire un rapport a votre boss de tous les exploits que vous avez fait", 
        "Malheureusement, votre étagère d'ingrédients vous est tombée dessus et depuis vous ne vous rappelez plus comment l'invoquer", 
        "Habituée a cet incident, vous avez écrit quelques notes pour vous rappeler de l'ordre des ingrédients à placer sur le cercle !", 
        "Dépêchez vous, votre boss s'impatiente !" }.ToArray();

    private void Awake()
    {
        _canvasGroup = text.GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0f;

        button.GetComponent<CanvasGroup>().alpha = 0;
        button.interactable = false;
    }

    void Start()
    {
        _readSentence(0);
    }

   void _readSentence(int sentenceIndex)
    {
        if(sentenceIndex < introSentences.Length)
        {
            text.text = introSentences[sentenceIndex];
            _canvasGroup.DOFade(1f, 2f).OnComplete(() =>
            {
                _canvasGroup.DOFade(0f, 2f).SetDelay(1.5f).OnComplete(() =>
                {
                    int nextSentence = sentenceIndex + 1;
                    _readSentence(nextSentence);
                });
            });
        }
        else
        {
            button.GetComponent<CanvasGroup>().DOFade(1f, 0.8f).OnComplete(() =>
            {
                button.interactable = true;
                button.GetComponent<RectTransform>().DOScale(1.02f, 0.8f).SetEase(Ease.InOutElastic).SetLoops(-1, LoopType.Yoyo);
            });
        }
    }
}
