using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OxygenBarController : MonoBehaviour
{
    public Slider oxygenSlider; // Referencja do obiektu Slider
    public float oxygenDepletionRate = 1.0f; // Szybkoœæ utraty tlenu na sekundê
    public float oxygenRegenerationRate = 2.0f; // Szybkoœæ odnawiania tlenu na sekundê
    public Transform playerHead; // Referencja do obiektu reprezentuj¹cego g³owê gracza
    public float oxygenBarDistance = 1.0f; // Odleg³oœæ od g³owy gracza, na której ma byæ umieszczony pasek tlenu

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
            SetOxygenBarVisibility(true); // Poka¿ pasek tlenu, gdy gracz jest w wodzie
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
            }
            else if (!isUnderwater && oxygenSlider.value < 10f)
            {
                // Gracz jest poza obszarem wody, zwiêkszaj tlenu
                oxygenSlider.value += oxygenRegenerationRate * Time.deltaTime;
                if (oxygenSlider.value >= 100f)
                {
                    SetOxygenBarVisibility(false); // Ukryj pasek tlenu, gdy osi¹gniêto pe³n¹ wartoœæ
                }
            }
            else if (!isUnderwater && oxygenSlider.value >= 10f)
            {
                SetOxygenBarVisibility(false); // Ukryj pasek tlenu, gdy osi¹gniêto pe³n¹ wartoœæ
            }

            // Aktualizuj pozycjê paska tlenu nad g³ow¹ gracza
            UpdateOxygenBarPosition();

            yield return null;
        }
    }

    private void UpdateOxygenBarPosition()
    {
        if (playerHead != null)
        {
            Vector3 targetPosition = playerHead.position + new Vector3(0f, oxygenBarDistance, 0f);
            oxygenSlider.transform.position = targetPosition;
        }
    }

    private void SetOxygenBarVisibility(bool isVisible)
    {
        oxygenSlider.gameObject.SetActive(isVisible);
    }
}