using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class AIScript : MonoBehaviour
{
    public TMP_Text numberText;
    private int number = 0;
    private bool gameRunning = false;
    public float speedCounting = 0.15f;
    public GameObject karmelekAI;

    public GameObject TutorialCanvas;
    public bool tutorialActive = true;
    public Animator animator;
    void Start()
    {  
	
        if (!tutorialActive)
        {
		    StartCoroutine(Odliczanie());

		
        }
    }
	private IEnumerator Odliczanie()
	{
		StopScoreCounting();
		yield return new WaitForSeconds(4f);
		gameRunning = true;
        InvokeRepeating("IncreaseNumberValue", 0.5f, speedCounting);
        animator.SetBool("isCutting", true);
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
