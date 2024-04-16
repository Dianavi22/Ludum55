using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loose : MonoBehaviour
{
    [SerializeField] GameObject _camera;
    [SerializeField] GameObject LoosePart;
    [SerializeField] InvocationManager _invocationManager;
    [SerializeField] GameObject _invisibleZone;
    [SerializeField] MenuManager _menuManager;
    [SerializeField] DialogManager _dialog;
    [SerializeField] GameObject _emplacementsZone;

    [SerializeField] float speed = 10f;

    private float journeyLength;


    private void Start()
    {
        journeyLength = Vector3.Distance(_camera.transform.position, new Vector3(_camera.transform.position.x, _camera.transform.position.y, 25));
    }

    public void PlayLoose()
    {
        StartCoroutine(GameOver());
    }

    private IEnumerator GameOver()
    {
        LoosePart.SetActive(true);
        yield return new WaitForSeconds(5f);
        _emplacementsZone.SetActive(false);
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

        while (timeElapsed < 2)
        {
            
            timeElapsed += Time.deltaTime * speed;
            float fractionOfJourney = (timeElapsed / journeyLength);
            _camera.transform.position = Vector3.Lerp(_camera.transform.position, new Vector3(_camera.transform.position.x, _camera.transform.position.y, 30), fractionOfJourney);

            yield return null;
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


