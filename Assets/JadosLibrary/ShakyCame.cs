using System.Collections;
using UnityEngine;

// Ce script est a placer sur une camera
// Il prend directement son transform ccomme point de depart de la shaky came
public class ShakyCame : MonoBehaviour
{
    private Transform _pointToShake; //Camera
    private float _speed = 0; // vitesse de deplacement de la camera (pas besoin d'être change : a 0)
    private Vector3 _offset;
    Vector3 center = Vector3.zero; // Sert au calcule du radius de la sphere de secousse

    private void Start()
    {
        _pointToShake = GetComponent<Transform>();
    }

    public void Shake(float duration, float radius)
    {
        StartCoroutine(Shaking(duration, radius));
    }

    // Distance de secousse de la shaky came (1 par defaut)
    // La shaky came fait des lerps tres vite entre des points
    // dans une sphere autour de lui de radius _radius
    // pendant une duree de _duration depuis le point de _pointToShake
    IEnumerator Shaking(float duration, float radius) // Coroutine de secousse
    {
        transform.position = Vector3.Lerp(transform.position, _pointToShake.position + _offset, _speed * Time.deltaTime);

        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            Debug.Log(elapsedTime);
            elapsedTime += Time.deltaTime;
            transform.position = startPosition + Random.insideUnitSphere * radius + center;
            yield return null;
        }
        transform.position = startPosition;
    }
}
