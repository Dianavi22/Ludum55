using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loose : MonoBehaviour
{
    [SerializeField] GameObject camera;
  [SerializeField] GameObject LoosePart;
 //   [SerializeField] ShakyCame shaky;

    [SerializeField] bool isFinish = false;


    public float speed = 1.0F;

    private float startTime;

    private float journeyLength;

    void Start()
    {
        startTime = Time.time;

        journeyLength = Vector3.Distance(camera.transform.position, new Vector3(camera.transform.position.x, camera.transform.position.y, 25));
    }

    void Update()
    {
        StartCoroutine(GameOver());
    }

    public IEnumerator GameOver()
    {
        LoosePart.SetActive(true);
        yield return new WaitForSeconds(5f);
        ZoomCam();
        yield return null;
    }

    public void ZoomCam()
    {
        float distCovered = (Time.time - startTime) * speed;

        // Fraction of journey completed equals current distance divided by total distance.
        float fractionOfJourney = distCovered / journeyLength;
        camera.transform.position = Vector3.Lerp(camera.transform.position, new Vector3(camera.transform.position.x, camera.transform.position.y, 30), fractionOfJourney);

    }
}


