using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
	
/*	public AudioSource walking;
	public AudioSource idle;*/
	
    public float speed = 5f;

	[field:SerializeField]
    public float jumpingPower = 16f;
    private float direction = 0f;
    private Rigidbody2D player;

    private bool isTouchingGround;

	public Animator animator;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;

    public string nextLevelScene;
    public Slider progressBar;
    private int score = 0;
    public int maxScore = 10;
    private int coinsCollected = 0;

    private Vector3 respawnPoint;
    public Transform respaPoint;


    private bool isOnMovingPlatform = false;
    private Transform currentPlatform = null;

    public Image[] hearts;
    public Sprite blackHeartSprite;
    public Sprite redHeartSprite;
    private int currentLives = 3;

    public Slider oxygenBar;
    public float maxOxygen = 100f;
    public float losingOxygenPerSeconds = 1f;

    void Start()
    {

        animator = GetComponent<Animator>();
        player = GetComponent<Rigidbody2D>();
        respawnPoint = respaPoint.position;

/*        oxygenBar.maxValue = maxOxygen;*/
        oxygenBar.value = maxOxygen;
	
        UpdateUI();
    }

    void Update()
    {
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        direction = Input.GetAxis("Horizontal");

	    if(direction == 0f)
	    {
        animator.SetBool("isWalking", false);
/*        idle.enabled = true;
        walking.enabled = false;*/
        }

	    else
	    {
        animator.SetBool("isWalking", true);
/*        idle.enabled = false;
        walking.enabled = true;*/
        }

        if (direction > 0f)
        {
            player.velocity = new Vector2(direction * speed, player.velocity.y);
            transform.localScale = new Vector2(0.65f, 0.65f);
        }
        else if (direction < 0f)
        {
            player.velocity = new Vector2(direction * speed, player.velocity.y);
            transform.localScale = new Vector2(-0.65f, 0.65f);
        }
        else
        {
            player.velocity = new Vector2(0, player.velocity.y);
        }

        if (Input.GetButtonDown("Jump") && isTouchingGround)
        {
            player.velocity = new Vector2(player.velocity.x, jumpingPower);
        }
        // Sprawd�, czy gracz zebra� wymagan� ilo�� punkt�w
        if (score >= maxScore)
        {
            LoadNextLevel();
        }
	
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {

            transform.position = respawnPoint;
            LoseLife();
        }


        else if (collision.tag == "NextLevel")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            respawnPoint = respaPoint.position;
        }
        else if (collision.tag == "PreviousLevel")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            respawnPoint = respaPoint.position;
        }

        else if (collision.CompareTag("Coin"))
        {
            CollectCoin(collision.gameObject);
        }
        else if (collision.CompareTag("Platform"))
        {
            isOnMovingPlatform = true;
            currentPlatform = collision.transform.parent;
            transform.SetParent(currentPlatform);
        }
        if (collision.CompareTag("Water"))
        {
            LoseOxygen();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform"))
        {
            isOnMovingPlatform = false;
            currentPlatform = null;
            transform.SetParent(null);
        }
    }

    private void LoseOxygen()
    {
        oxygenBar.value = losingOxygenPerSeconds * Time.deltaTime;

        if (oxygenBar.value <= 0)
        {
            Invoke("LoseLifeWithBlackHeart", 2f);
        }
    }
    private void LoseLifeWithBlackHeart()
    {
        // Zmiana obrazka serduszka na czarne serduszko
        hearts[currentLives].sprite = blackHeartSprite;
    }
    private void LoseLife()
    {
        currentLives--;
        UpdateUI();

        if (currentLives <= 0)
        {
            SceneManager.LoadScene(3);
            Debug.Log("Game Over!");
        }
        else
        {
            LoseLifeWithBlackHeart();
        }
    }

    private void UpdateUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentLives)
            {
                hearts[i].sprite = redHeartSprite;
            }
            else
            {
                hearts[i].sprite = blackHeartSprite;
            }
        }
    }

    private void CollectCoin(GameObject coin)
    {
        score++;
        progressBar.value = (float)score / maxScore;

        coinsCollected++;

        Destroy(coin);

        if (coinsCollected >= 20)
        {
            LoadNextLevel();
        }
    }
    private void LoadNextLevel()
    {
        if (!string.IsNullOrEmpty(nextLevelScene))
        {
            SceneManager.LoadScene(nextLevelScene);
        }
    }
}