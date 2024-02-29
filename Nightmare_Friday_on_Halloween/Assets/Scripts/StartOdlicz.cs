using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartOdlicz : MonoBehaviour
{
   public Animator odlicz;
    void Start()
    {
        odlicz = GetComponent<Animator>();
	odlicz.enabled = false;
    }

   public void RozpOdlicz()
	{
		odlicz.enabled = true;
		odlicz.Play("odliczanie");
	}
}
