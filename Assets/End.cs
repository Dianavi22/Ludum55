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


    float timeElapsed;
    float lerpDurationZoom = 0.5f;

    float startValue = 0;
    float endValue = 10;
    float valueToLerp;

    public PostProcessVolume glitch;
    public ShakyCame _cam;





    private float counter = 0f;
    private float duration = 2f;
    private float incrementValue = 0.1f;
    // Start is called before the first frame update
    void Start()
    {

        WinPart.Play();

        StartCoroutine(Victory());
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public IEnumerator Victory()
    {

        // imageToLerp.enabled = true;
        // StartCoroutine(LerpColor());
        yield return new WaitForSeconds(2);
        _cam.isShaking = true;
        // glitch.weight = 1;

       // StartCoroutine(IncrementCounter());
        yield return new WaitForSeconds(2);
        imageToLerp.enabled = true;
        StartCoroutine(LerpColor());
        yield return null;
    }

    private IEnumerator LerpColor()
    {
        float timeElapsed = 0f;
        print("HERE");

        while (timeElapsed < lerpDuration)
        {
            print("HERE AUSSI");

            float t = timeElapsed / lerpDuration;
            imageToLerp.color = Color.Lerp(initialColor, targetColor, t);

            timeElapsed += Time.deltaTime;
            yield return null;

            imageToLerp.color = targetColor;
        }
    }




}
