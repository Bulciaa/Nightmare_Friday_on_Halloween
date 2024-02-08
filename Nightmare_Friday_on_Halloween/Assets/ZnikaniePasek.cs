using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZnikaniePasek : MonoBehaviour
{
   private float disappearSpeed = 0.5f;

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
	{
		Color objectColor = GetComponent<Renderer>().material.color;
            objectColor.a -= disappearSpeed * Time.deltaTime;
            GetComponent<Renderer>().material.color = objectColor;
	}
    }
}
