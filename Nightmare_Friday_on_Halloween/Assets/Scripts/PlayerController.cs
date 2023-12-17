using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

   	// public AudioSource walking;
  //      public AudioSource idle;

    public float speed = 5f;

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
    public int maxOrb = 10;
    private int orbCollected = 0;

    private Vector3 respawnPoint;
    public Transform respaPoint;

    [SerializeField]
    public Image[] hearts;
    public Sprite blackHeartSprite;
    public Sprite redHeartSprite;
    private int currentLives = 3;

    public GameObject portalPrefab; // Reference to your portal prefab
    private GameObject currentPortal; // Instance of the portal in the scene
    /*    public float delayBeforeNextLevel = 2f; // Delay in seconds before loading the next level

        private bool isStopped = false;*/

    public int additionalOxygenPoints = 5; // Liczba dodatkowych punkt w tlenu po zebraniu obiektu "Bubble"

    void Start()
    {

        animator = GetComponent<Animator>();
        player = GetComponent<Rigidbody2D>();
        respawnPoint = respaPoint.position;

        UpdateUI();
    }

    void Update()
    {
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        direction = Input.GetAxis("Horizontal");

        if (direction == 0f)
        {
            animator.SetBool("isWalking", false);
/*            idle.enabled = true;
            walking.enabled = false;*/
        }

        else
        {
            animator.SetBool("isWalking", true);
/*            idle.enabled = false;
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
        // Sprawd , czy gracz zebrze  wymagan  ilo   punkt w
        if (score >= maxOrb && currentPortal == null)
        {
            SpawnPortal();
        }
      

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {

            transform.position = respawnPoint;
            LoseLife();
        }


        if (collision.tag == "NextLevel" && currentPortal != null)
        {

            StartCoroutine(DelayBeforeNextLevel());
/*            // Zatrzymaj gracza
            isStopped = true;

            // Zatrzymaj gracza ustawiaj c pr dko   na zero
            player.velocity = Vector2.zero;*/

            // Invoke LoadNextLevel po 3 sekundach
/*            Invoke("LoadNextLevel", delayBeforeNextLevel);*/
        }
        else if (collision.tag == "PreviousLevel")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            respawnPoint = respaPoint.position;
        }

        else if (collision.CompareTag("Orb"))
        {
            CollectOrb(collision.gameObject);
        }

        else if (collision.CompareTag("Bubble"))
        {
            CollectBubble(collision.gameObject);
        }

    }
    private IEnumerator DelayBeforeNextLevel()
    {
        player.constraints = RigidbodyConstraints2D.FreezePosition;
        yield return new WaitForSeconds(3f);
        LoadNextLevel();
    }

    public void LoseLife()
    {
        currentLives--;
        UpdateUI();

        if (currentLives <= 0)
        {
            SceneManager.LoadScene(6);
            Debug.Log("Game Over!");
        }
        else
        {
            // Zmiana obrazka serduszka na czarne serduszko
            hearts[currentLives].sprite = blackHeartSprite;
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

    private void CollectBubble(GameObject bubble)
    {
        OxygenBarController oxygenController = GetComponent<OxygenBarController>();

        if (oxygenController != null)
        {
            // Dodaj dodatkowe punkty do paska tlenu
            oxygenController.AddOxygenPoints(additionalOxygenPoints);

            // Dodatkowe operacje (np. zniszcz obiekt "dodatkowyTlen")
            Destroy(bubble);
        }
    }
    private void CollectOrb(GameObject orb)
    {
        score++;
        progressBar.value = (float)score / maxOrb;

        orbCollected++;

        Destroy(orb);

        if (orbCollected >= maxOrb && currentPortal == null)
        {
            SpawnPortal();
        }
    }
    private void SpawnPortal()
    {
        // Offset to start the raycast slightly above and to the side of the player
        float portalSpawnOffsetX = (transform.localScale.x > 0f) ? 4f : -4f;
        Vector3 raycastStart = transform.position + new Vector3(portalSpawnOffsetX, 0.5f, 0f);

        // Raycast to check for ground beneath the spawn position
        RaycastHit2D hit = Physics2D.Raycast(raycastStart, Vector2.down, Mathf.Infinity, LayerMask.GetMask("Ground"));

        // If the ray hits something, set the portalSpawnPosition to the hit point plus an offset
        Vector3 portalSpawnPosition = hit ? hit.point + new Vector2(0f, 2.5f) : transform.position;

        // Instantiate the portalPrefab at the determined position
        currentPortal = Instantiate(portalPrefab, portalSpawnPosition, Quaternion.identity);
    }

    private void LoadNextLevel()
    {
        if (!string.IsNullOrEmpty(nextLevelScene))
        {
            Destroy(currentPortal);
            SceneManager.LoadScene(nextLevelScene);
        }

    }
}
    
