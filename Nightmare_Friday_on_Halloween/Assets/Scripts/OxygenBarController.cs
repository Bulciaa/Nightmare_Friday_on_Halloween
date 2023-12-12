using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class OxygenBarController : MonoBehaviour
{
    public Slider oxygenSlider; // Referencja do obiektu Slider
    public float oxygenDepletionRate = 1.0f; // Szybko�� utraty tlenu na sekund�
    public float oxygenRegenerationRate = 2.0f; // Szybko�� odnawiania tlenu na sekund�

    private bool isUnderwater = false;

    private void Start()
    {
        SetOxygenBarVisibility(false); // Ukryj pasek tlenu na pocz�tku gry
        StartCoroutine(UpdateOxygen());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            // Gracz wszed� do obszaru z wod�
            isUnderwater = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            // Gracz opu�ci� obszar z wod�
            isUnderwater = false;
        }
    }

    private IEnumerator UpdateOxygen()
    {
        while (true)
        {
            if (isUnderwater && oxygenSlider.value > 0f)
            {
                // Gracz jest w obszarze wody, zmniejszaj tlenu
                oxygenSlider.value -= oxygenDepletionRate * Time.deltaTime;
                SetOxygenBarVisibility(true); // Poka� pasek tlenu, gdy gracz jest w wodzie
            }
            else if (!isUnderwater && oxygenSlider.value < 100f)
            {
                // Gracz jest poza obszarem wody, zwi�kszaj tlenu
                oxygenSlider.value += oxygenRegenerationRate * Time.deltaTime;
                SetOxygenBarVisibility(false); // Ukryj pasek tlenu, gdy gracz jest poza wod�
            }

            yield return null;
        }
    }

    private void SetOxygenBarVisibility(bool isVisible)
    {
        oxygenSlider.gameObject.SetActive(isVisible);
    }
}