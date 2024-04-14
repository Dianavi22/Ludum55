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
    string[] introSentences = new List<string>() { "YOU ! Are a witch...", 
        "And now it's time to talk to your boss", 
        "But you dont remember how to summon him", 
        "Fortunately, you have few notes about this spell !", 
        "Good luck, your boss is waiting for you" }.ToArray();

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
                _canvasGroup.DOFade(0f, 2f).SetDelay(3).OnComplete(() =>
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
