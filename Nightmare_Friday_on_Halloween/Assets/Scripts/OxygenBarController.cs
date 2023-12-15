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
    public float smoothDampTime = 0.3f; // Czas p�ynnego poruszania paska tlenu

    public int MaxOxygen = 10;

    private bool isUnderwater = false;
    private Vector3 velocity = Vector3.zero;

    public float darkenDuration = 3f; // Czas trwania ciemnienia ekranu po spadni�ciu paska tlenu do zera
    public float restoreDuration = 2f; // Czas przywracania jasno�ci ekranu
    public Image screenOverlay; // Obrazek do pokrycia ekranu w celu uzyskania efektu zaciemnienia
    public Color darkenedColor = new Color(0, 0, 0, 0.5f); // Kolor zaciemnienia
    public float darknessIntensity = 0.5f; // Moc przyciemnienia

    private bool isDarkened = false;
    private float defaultBrightness;

    private void Start()
    {
        SetOxygenBarVisibility(false); // Ukryj pasek tlenu na pocz�tku gry
        defaultBrightness = RenderSettings.ambientIntensity;

        // Ukryj overlay na pocz�tku gry
        screenOverlay.gameObject.SetActive(false);
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

    private void Update()
    {
        // Aktualizuj p�ynne poruszanie paska tlenu nad g�ow� gracza
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
                    // Je�li ekran by� wcze�niej zaciemniony, przywr�� jasno��
                    StartCoroutine(RestoreScreen());
                    isDarkened = false;
                }

                // Gracz jest w obszarze wody, zmniejszaj tlenu
                oxygenSlider.value -= oxygenDepletionRate * Time.deltaTime;

                if (oxygenSlider.value <= 0f)
                {
                    // Pasek tlenu spad� do zera, zacznij zaciemnianie ekranu
                    StartCoroutine(DarkenScreen());
                }
            }
            else if (!isUnderwater && oxygenSlider.value < MaxOxygen)
            {
                if (isDarkened)
                {
                    // Je�li ekran by� wcze�niej zaciemniony, przywr�� jasno��
                    StartCoroutine(RestoreScreen());
                    isDarkened = false;
                }

                // Gracz jest poza obszarem wody, zwi�kszaj tlenu
                oxygenSlider.value += oxygenRegenerationRate * Time.deltaTime;

                if (oxygenSlider.value >= MaxOxygen)
                {
                    SetOxygenBarVisibility(false); // Ukryj pasek tlenu, gdy osi�gni�to pe�n� warto��
                }
            }
            else if (!isUnderwater && oxygenSlider.value >= MaxOxygen)
            {
                SetOxygenBarVisibility(false); // Ukryj pasek tlenu, gdy osi�gni�to pe�n� warto��
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
        screenOverlay.gameObject.SetActive(true);

        float elapsedTime = 0f;

        while (elapsedTime < darkenDuration)
        {
            float t = Mathf.SmoothStep(0f, 1f, elapsedTime / darkenDuration);
            float currentDarknessIntensity = Mathf.Lerp(0f, darknessIntensity, t);
            screenOverlay.color = darkenedColor * currentDarknessIntensity;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isDarkened = true;
    }

    private IEnumerator RestoreScreen()
    {
        float elapsedTime = 0f;

        while (elapsedTime < restoreDuration)
        {
            float t = Mathf.SmoothStep(0f, 1f, elapsedTime / restoreDuration);
            float currentDarknessIntensity = Mathf.Lerp(darknessIntensity, 0f, t);
            screenOverlay.color = darkenedColor * currentDarknessIntensity;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        screenOverlay.gameObject.SetActive(false);
    }

    private void ResetScreenBrightness()
    {
        screenOverlay.gameObject.SetActive(false);
        screenOverlay.color = Color.clear;
    }
}
