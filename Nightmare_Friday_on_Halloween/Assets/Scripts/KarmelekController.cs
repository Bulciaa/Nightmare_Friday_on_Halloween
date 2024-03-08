using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarmelekController : MonoBehaviour
{
    private void Start()
    {
        // Rozpocznij animacj� znikania karmelka
        Invoke("DestroyCandy", 0.5f);
    }

    private void DestroyCandy()
    {
        // Zniszcz obiekt karmelka po zako�czeniu animacji
        Destroy(gameObject);
    }
}
