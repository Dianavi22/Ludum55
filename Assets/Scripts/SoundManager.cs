using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource intro;
    [SerializeField] AudioSource musicBase;
    [SerializeField] AudioSource pendulum;

    private void Start()
    {
        intro.Play();
    }

    private void Update()
    {
        if (intro.timeSamples > intro.clip.samples - 4000)
        {
            DisablePendulum();

            musicBase.Play();
            pendulum.Play();
        }
    }

    public void ActivePendulum()
    {
        pendulum.volume = 1;
    }

    public void DisablePendulum()
    {
        pendulum.volume = 0;
    }
}
