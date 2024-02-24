using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarmelekController : MonoBehaviour
{
    private void Start()
    {
        // Rozpocznij animację znikania karmelka
        Invoke("DestroyCandy", 0.5f);
    }

    private void DestroyCandy()
    {
        // Zniszcz obiekt karmelka po zakończeniu animacji
        Destroy(gameObject);
    }
}
