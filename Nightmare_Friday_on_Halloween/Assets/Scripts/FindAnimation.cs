using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindAnimation : MonoBehaviour
{
    public Animator textAnimator;
    public AudioSource soundSource;
    public AudioClip startSound;

    void Start()
    {
        // Uruchom animacj� tekstu
        textAnimator.SetTrigger("FindKruegerText");

        // Odtw�rz d�wi�k
        soundSource.clip = startSound;
        soundSource.Play();
    }
}
