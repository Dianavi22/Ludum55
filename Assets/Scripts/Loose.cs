using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loose : MonoBehaviour
{
    [SerializeField] GameObject camera;
    [SerializeField] GameObject LoosePart;
    [SerializeField] InvocationManager _invocationManager;
    [SerializeField] GameObject _invisibleZone;
    [SerializeField] MenuManager _menuManager;
    [SerializeField] DialogManager _dialog;

    public float speed = 100f;

    private float journeyLength;

    private Vector3 defaultPos;

    private void Awake()
    {
        defaultPos = camera.transform.position;
    }


    private void Start()
    {
        journeyLength = Vector3.Distance(defaultPos, new Vector3(defaultPos.x, defaultPos.y, 25));
    }

    public void PlayLoose()
    {
        StartCoroutine(GameOver());
    }

    private IEnumerator GameOver()
    {
        LoosePart.SetActive(true);
        yield return new WaitForSeconds(5f);
         StartCoroutine(ZoomCam());
        yield return new WaitForSeconds(1.5f);

        LoosePart.SetActive(false);
        _menuManager.DefeatPanel();
        _invisibleZone.SetActive(true);
        _invocationManager.ResetInvocation();
        _dialog.HideDialog();
        StopCoroutine(ZoomCam());

        yield return null;
    }

    private IEnumerator ZoomCam()
    {

        // Fraction of journey completed equals current distance divided by total distance.

        float timeElapsed = 0f;

        while (true)
        {
            
            timeElapsed += Time.deltaTime;
            float fractionOfJourney = (timeElapsed / journeyLength) * speed;
            camera.transform.position = Vector3.Lerp(camera.transform.position, new Vector3(camera.transform.position.x, camera.transform.position.y, 30), fractionOfJourney);

            yield return null;
            camera.transform.position = defaultPos;
        }

    }

    //public void ZoomCam()
    //{
    //    float distCovered = (Time.time - startTime) * speed;

    //    // Fraction of journey completed equals current distance divided by total distance.
    //    float fractionOfJourney = distCovered / journeyLength;
    //    camera.transform.position = Vector3.Lerp(camera.transform.position, new Vector3(camera.transform.position.x, camera.transform.position.y, 30), fractionOfJourney);

    //}
}


