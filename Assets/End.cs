using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class End : MonoBehaviour
{

    public Image imageToLerp;
    public Color targetColor;
    public float lerpDuration = 1f;

    public Color initialColor;

    [SerializeField] GameObject WinLight;
    [SerializeField] ParticleSystem WinPart;

    public PostProcessVolume glitch;
    public ShakyCame _cam;

    public void PlayWin(Action after)
    {
        WinPart.Play();
        StartCoroutine(Victory(after));
    }

    private IEnumerator Victory(Action after)
    {

        // imageToLerp.enabled = true;
        // StartCoroutine(LerpColor());
        yield return new WaitForSeconds(2);
        _cam.isShaking = true;
        // glitch.weight = 1;

       // StartCoroutine(IncrementCounter());
        yield return new WaitForSeconds(2);
        imageToLerp.enabled = true;
        StartCoroutine(LerpColor(after));
        yield return new WaitForSeconds(1);
        WinPart.Stop();
        yield return null;
    }

    private IEnumerator LerpColor(Action after)
    {
        float timeElapsed = 0f;

        while (timeElapsed < lerpDuration)
        {

            float t = timeElapsed / lerpDuration;
            imageToLerp.color = Color.Lerp(initialColor, targetColor, t);

            timeElapsed += Time.deltaTime;
            yield return null;

            imageToLerp.color = targetColor;
        }
        yield return new WaitForSeconds(2);
        after();
    }




}
