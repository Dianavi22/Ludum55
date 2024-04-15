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
    [SerializeField] GameObject LoosePart;

    [SerializeField] GameObject loose;

    public ShakyCame _shakyCam;

    public void PlayWin(Action after)
    {
        WinPart.Play();
        StartCoroutine(Victory(after));
    }

    public void PlayLoose()
    {
        LoosePart.SetActive(true);
        loose.SetActive(true);
        loose.GetComponent<Loose>().PlayLoose();
    }

    private IEnumerator Victory(Action after)
    {
        _shakyCam.Shake(4f,0.1f);
        // glitch.weight = 1;
        yield return new WaitForSeconds(2.5f);
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
        after();
        imageToLerp.enabled=false;
    }


}
