using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OxygenBarController : MonoBehaviour
{
    public Slider oxygenSlider; // Referencja do obiektu Slider
    public float oxygenDepletionRate = 1.0f; // Szybko�� utraty tlenu na sekund�
    public float oxygenRegenerationRate = 2.0f; // Szybko�� odnawiania tlenu na sekund�
    public Transform playerHead; // Referencja do obiektu reprezentuj�cego g�ow� gracza
    public float oxygenBarDistance = 1.0f; // Odleg�o�� od g�owy gracza, na kt�rej ma by� umieszczony pasek tlenu

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
            SetOxygenBarVisibility(true); // Poka� pasek tlenu, gdy gracz jest w wodzie
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
            }
            else if (!isUnderwater && oxygenSlider.value < 10f)
            {
                // Gracz jest poza obszarem wody, zwi�kszaj tlenu
                oxygenSlider.value += oxygenRegenerationRate * Time.deltaTime;
                if (oxygenSlider.value >= 100f)
                {
                    SetOxygenBarVisibility(false); // Ukryj pasek tlenu, gdy osi�gni�to pe�n� warto��
                }
            }
            else if (!isUnderwater && oxygenSlider.value >= 10f)
            {
                SetOxygenBarVisibility(false); // Ukryj pasek tlenu, gdy osi�gni�to pe�n� warto��
            }

            // Aktualizuj pozycj� paska tlenu nad g�ow� gracza
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