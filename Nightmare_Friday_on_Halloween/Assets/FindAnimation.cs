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
        // Uruchom animacjê tekstu
        textAnimator.SetTrigger("FindKruegerText");

        // Odtwórz dŸwiêk
        soundSource.clip = startSound;
        soundSource.Play();
    }
}
