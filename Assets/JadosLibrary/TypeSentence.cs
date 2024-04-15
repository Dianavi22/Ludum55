using System.Collections;
using TMPro;
using UnityEngine;

// Ce script est a placer dans la hiérarchie (ou dans le canva directement)
// Il contient une fonction qui peut etre appelee partout s'il a la reference de se script 
// Il faut lui passer les parammtres : Textes a ecrire, emplacement du texte, secondes entre chaque caracteres


public class TypeSentence : MonoBehaviour
{
    // Parametres 
    private TMP_Text _textPlace;
    private string _textToShow;
    private float _timeBetweenChar; // Temps en Seconde


    [SerializeField] AudioClip[] voice;
    public AudioSource audioSource;

    public void WriteMachinEffect(string _currentTextToShow, TMP_Text _currentTextPlace, float _currentTimeBetweenChar, bool disableSound = false) // Fonction à appeler depuis un autre script
    {
        _textToShow = _currentTextToShow;
        _textPlace = _currentTextPlace;
        _timeBetweenChar = _currentTimeBetweenChar;
        StartCoroutine(TypeCurrentSentence(_textToShow, _textPlace, disableSound));
    }
    IEnumerator TypeCurrentSentence(string sentence, TMP_Text place, bool disableSound)
    {
        foreach (char letter in sentence.ToCharArray())
        {
            yield return new WaitForSeconds(_timeBetweenChar);
            place.text += letter;
          if(!disableSound)  audioSource.PlayOneShot(voice[Random.Range(0, voice.Length)]);
            yield return null;
        }
    }
}
