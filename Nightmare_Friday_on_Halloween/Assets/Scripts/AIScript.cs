using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class AIScript : MonoBehaviour
{
    public TMP_Text numberText;
    private int number = 0;
    private bool gameRunning = true;
    public float speedCounting = 0.15f;
    public GameObject karmelekAI;


    void Start()
    {
        
	
        if (gameRunning)
        {
		StartCoroutine(Odliczanie());
            InvokeRepeating("IncreaseNumberValue", 0.5f, speedCounting);
		
        }
    }

	private IEnumerator Odliczanie()
	{
		StopScoreCounting();
		yield return new WaitForSeconds(3f);
		gameRunning = true;
	}

    void IncreaseNumberValue()
    {
        // Zwi�kszaj liczb� tylko je�li gra si� toczy
        if (gameRunning)
        {
            number++;
            numberText.text = "Score: " + number.ToString();
            SpawnCandy();
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
    void SpawnCandy()
    {
        // Instancjonuj prefab karmelka w pozycji startowej
        GameObject candy = Instantiate(karmelekAI, Vector3.zero, Quaternion.identity);

    }
}
