using UnityEngine;

public class PowiekszaniePostaci : MonoBehaviour
{
    private Vector3 startowaSkala;
    public float powiekszenie = 1.5f;
public AudioSource select;

    private void Start()
    {
        startowaSkala = transform.localScale;
    }

    private void OnMouseEnter()
    {
        transform.localScale = startowaSkala * powiekszenie;
	select.Play();
    }

    private void OnMouseExit()
    {
        transform.localScale = startowaSkala;
    }
}
