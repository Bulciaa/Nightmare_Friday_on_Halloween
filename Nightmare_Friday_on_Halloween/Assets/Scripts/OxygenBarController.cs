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
    public float smoothDampTime = 0.3f; // Czas p³ynnego poruszania paska tlenu

    public int MaxOxygen = 10;

    private bool isUnderwater = false;
    private Vector3 velocity = Vector3.zero;

    public float darkenDuration = 3f; // Czas trwania ciemnienia ekranu po spadniêciu paska tlenu do zera
    private bool isDarkened = false;
    private float defaultBrightness;

    private void Start()
    {
        SetOxygenBarVisibility(false); // Ukryj pasek tlenu na pocz¹tku gry
        defaultBrightness = RenderSettings.ambientIntensity;
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

    private void Update()
    {
        // Aktualizuj p³ynne poruszanie paska tlenu nad g³ow¹ gracza
        UpdateOxygenBarPosition();
    }

    private IEnumerator UpdateOxygen()
    {
        while (true)
        {
            if (isUnderwater && oxygenSlider.value > 0f)
            {
                if (isDarkened)
                {
                    // Jeœli ekran by³ wczeœniej zaciemniony, przywróæ jasnoœæ
                    ResetScreenBrightness();
                    isDarkened = false;
                }
                // Gracz jest w obszarze wody, zmniejszaj tlenu
                oxygenSlider.value -= oxygenDepletionRate * Time.deltaTime;

                if (oxygenSlider.value <= 0f)
                {
                    // Pasek tlenu spad³ do zera, zacznij zaciemnianie ekranu
                    StartCoroutine(DarkenScreen());
                }
            }
            else if (!isUnderwater && oxygenSlider.value < MaxOxygen)
            {
                // Gracz jest poza obszarem wody, zwiêkszaj tlenu
                oxygenSlider.value += oxygenRegenerationRate * Time.deltaTime;
                if (oxygenSlider.value >= MaxOxygen)
                {
                    SetOxygenBarVisibility(false); // Ukryj pasek tlenu, gdy osi¹gniêto pe³n¹ wartoœæ
                }
            }
            else if (!isUnderwater && oxygenSlider.value >= MaxOxygen)
            {
                SetOxygenBarVisibility(false); // Ukryj pasek tlenu, gdy osi¹gniêto pe³n¹ wartoœæ
            }

            yield return null;
        }
    }
    public void AddOxygenPoints(int points)
    {
        oxygenSlider.value += points;

        if (oxygenSlider.value > MaxOxygen)
        {
            oxygenSlider.value = MaxOxygen;
        }
    }

    private void UpdateOxygenBarPosition()
    {
        if (playerHead != null)
        {
            Vector3 targetPosition = playerHead.position + new Vector3(0f, oxygenBarDistance, 0f);
            oxygenSlider.transform.position = Vector3.SmoothDamp(oxygenSlider.transform.position, targetPosition, ref velocity, smoothDampTime);
        }
    }

    private void SetOxygenBarVisibility(bool isVisible)
    {
        oxygenSlider.gameObject.SetActive(isVisible);
    }
    private IEnumerator DarkenScreen()
    {
        float elapsedTime = 0f;
        float startBrightness = RenderSettings.ambientIntensity;

        while (elapsedTime < darkenDuration)
        {
            // Ciemnienie ekranu zaczyna siê od wszystkich krawêdzi i ciemnieje do œrodka
            float t = Mathf.SmoothStep(0f, 1f, elapsedTime / darkenDuration);
            float darkenedBrightness = Mathf.Lerp(startBrightness, 0f, t);
            RenderSettings.ambientIntensity = darkenedBrightness;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isDarkened = true;
    }

    private void ResetScreenBrightness()
    {
        RenderSettings.ambientIntensity = defaultBrightness;
    }
}
