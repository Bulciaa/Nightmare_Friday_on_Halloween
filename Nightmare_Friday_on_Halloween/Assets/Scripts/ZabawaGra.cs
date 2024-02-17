using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ZabawaGra : MonoBehaviour
{
   public Image image1;
    public Image image2;
	private int score = 0;
	public TMP_Text scoreText;
	public Animator animator;

    void Start()
    {
	animator = GetComponent<Animator>();
        InvokeRepeating("ToggleImage", 1f, 0.5f);
    }

    void ToggleImage()
    {
        image1.enabled = Random.Range(0, 2) == 0;
	
        image2.enabled = Random.Range(0, 2) == 0;
    }

	void Update()
	{
		if (image1.enabled && Input.GetKeyDown(KeyCode.D))
		{
			scoreText.text = score.ToString();
			score++;
			animator.SetTrigger("leftAnim");
		}
		else if (!image1.enabled && Input.GetKeyDown(KeyCode.D))
		{
			scoreText.text = score.ToString();
			score--;
		}
		else if (image2.enabled && Input.GetKeyDown(KeyCode.A))
		{
			scoreText.text = score.ToString();
			score++;
		}
		else if (!image2.enabled && Input.GetKeyDown(KeyCode.A))
		{
			scoreText.text = score.ToString();
			score--;
		}
	}
}
