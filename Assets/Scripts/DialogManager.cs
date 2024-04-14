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

    private void Awake()
    {
        dialogContainer.alpha = 0;
        dialogContainer.blocksRaycasts = false;
    }

    public void ShowDialog(string[] sentences, Color dialogColor, Action afterDialog)
    {
        dialogContainer.alpha = 1;
        dialogBox.color = dialogColor;
        _readSentence(0,sentences, afterDialog);
    }

    void _readSentence(int sentenceIndex, string[] sentences, Action afterDialog)
    {
        if (sentenceIndex < sentences.Length)
        {
            StartCoroutine(TypeSentence(sentences[sentenceIndex]));
        }
        else
        {
            dialogContainer.alpha = 0;
            afterDialog();
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        text.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            text.text += letter;
            yield return null;
        }
    } 
}
