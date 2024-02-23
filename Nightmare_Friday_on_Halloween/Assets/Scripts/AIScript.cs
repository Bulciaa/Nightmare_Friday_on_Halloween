using UnityEngine;
using TMPro;

public class AIScript : MonoBehaviour
{
    public TMP_Text numberText;
    private int number = 0;
    private bool gameRunning = true;
    public float speedCounting = 0.15f;

    void Start()
    {
        // Rozpocznij odliczanie punkt�w, ale tylko je�li gra si� toczy
        if (gameRunning)
        {
            InvokeRepeating("IncreaseNumberValue", 0.5f, speedCounting);
        }
    }

    void IncreaseNumberValue()
    {
        // Zwi�kszaj liczb� tylko je�li gra si� toczy
        if (gameRunning)
        {
            number++;
            numberText.text = "Score: " + number.ToString();
        }
    }

    // Metoda do zatrzymania naliczania punkt�w, wywo�ywana z zewn�trz
    public void StopScoreCounting()
    {
        gameRunning = false;
    }
    // Metoda zwracaj�ca aktualny wynik przeciwnika
    public int GetScore()
    {
        return number;
    }
}
