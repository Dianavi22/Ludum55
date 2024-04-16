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
    [SerializeField] AudioClip win;
    [SerializeField] AudioClip loose;
    [SerializeField] AudioClip hit;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();  
    }

    public void PageOpen()
    {
        audioSource.volume = 0.2f;
        _Play(pageOpen);
    }

    public void PageClose()
    {
        audioSource.volume = 0.2f;
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

    public void Win()
    {
        audioSource.volume = 0.8f;
        _Play(win);
    }

    public void Hit()
    {
        audioSource.volume = 0.8f;
        _Play(hit);
    }

    public void Loose()
    {
        audioSource.volume = 0.8f;
        _Play(loose);
    }

    private void _Play(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
}
