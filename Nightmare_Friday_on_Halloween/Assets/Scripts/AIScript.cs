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
        // Rozpocznij odliczanie punktów, ale tylko jeœli gra siê toczy
        if (gameRunning)
        {
            InvokeRepeating("IncreaseNumberValue", 0.5f, speedCounting);
        }
    }

    void IncreaseNumberValue()
    {
        // Zwiêkszaj liczbê tylko jeœli gra siê toczy
        if (gameRunning)
        {
            number++;
            numberText.text = "Score: " + number.ToString();
        }
    }

    // Metoda do zatrzymania naliczania punktów, wywo³ywana z zewn¹trz
    public void StopScoreCounting()
    {
        gameRunning = false;
    }
    // Metoda zwracaj¹ca aktualny wynik przeciwnika
    public int GetScore()
    {
        return number;
    }
}
