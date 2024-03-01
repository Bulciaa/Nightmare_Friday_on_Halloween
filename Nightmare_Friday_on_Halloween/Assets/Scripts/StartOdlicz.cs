using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartOdlicz : MonoBehaviour
{
    public Animator odlicz;
	public AudioSource countingdwonSound;
    void Start()
    {
        odlicz = GetComponent<Animator>();
		countingdwonSound = GetComponent<AudioSource>();
		odlicz.enabled = false;
		countingdwonSound.enabled = false;
    }

   public void RozpOdlicz()
	{
		odlicz.enabled = true;
		odlicz.Play("odliczanie");
		countingdwonSound.enabled=true;
		countingdwonSound.Play();
	}
}
