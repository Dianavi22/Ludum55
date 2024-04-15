using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxManager : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip pageOpen;
    [SerializeField] AudioClip pageClose;
    [SerializeField] AudioClip hoverButton;
    [SerializeField] AudioClip selectButton;
    [SerializeField] AudioClip takeItem;
    [SerializeField] AudioClip dropItem;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();  
    }

    public void PageOpen()
    {
        audioSource.volume = 0.02f;
        _Play(pageOpen);
    }

    public void PageClose()
    {
        audioSource.volume = 0.02f;
        _Play(pageClose);
    }

    public void HoverButton()
    {
        audioSource.volume = 0.1f;
        _Play(hoverButton);
    }

    public void SelectButton()
    {
        audioSource.volume = 0.2f;
        _Play(selectButton);
    }

    public void TakeItem()
    {
        audioSource.volume = 0.5f;
        _Play(takeItem);
    }
    public void DropItem()
    {
        audioSource.volume = 0.2f;
        _Play(dropItem);
    }


    private void _Play(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
}
