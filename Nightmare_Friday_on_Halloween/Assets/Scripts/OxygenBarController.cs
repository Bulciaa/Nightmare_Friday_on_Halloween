using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class OxygenBarController : MonoBehaviour
{
    public Slider oxygenSlider; // Referencja do obiektu Slider
    public float oxygenDepletionRate = 1.0f; // Szybkoœæ utraty tlenu na sekundê
    public float oxygenRegenerationRate = 2.0f; // Szybkoœæ odnawiania tlenu na sekundê

    private bool isUnderwater = false;

    private void Start()
    {
        SetOxygenBarVisibility(false); // Ukryj pasek tlenu na pocz¹tku gry
        StartCoroutine(UpdateOxygen());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            // Gracz wszed³ do obszaru z wod¹
            isUnderwater = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            // Gracz opuœci³ obszar z wod¹
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
                SetOxygenBarVisibility(true); // Poka¿ pasek tlenu, gdy gracz jest w wodzie
            }
            else if (!isUnderwater && oxygenSlider.value < 100f)
            {
                // Gracz jest poza obszarem wody, zwiêkszaj tlenu
                oxygenSlider.value += oxygenRegenerationRate * Time.deltaTime;
                SetOxygenBarVisibility(false); // Ukryj pasek tlenu, gdy gracz jest poza wod¹
            }

            yield return null;
        }
    }

    private void SetOxygenBarVisibility(bool isVisible)
    {
        oxygenSlider.gameObject.SetActive(isVisible);
    }
}