using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;


public class PlayerController : MonoBehaviour
{
	public AudioSource underwater;

	public TMP_Text complitedText;
	
	private float horizontal;
	private float vertical;	

    [SerializeField] private float speed = 6f;
    [SerializeField] private float jumpingPower = 16f;
    private bool isFacingRight = true;

	public float totalStamina;
	public float stamina;
	public GameObject staminaBar;
	public GameObject staminaBackground;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
	
	public ParticleSystem particleSprint;
	public ParticleSystem particleJump;
	public SpriteRenderer spriteRenderer;
	private float disappearSpeed = 0.8f;
	private float appearSpeed = 2f;

	public float minAlpha = 0.01f;
	public float maxAlpha = 1f;
	
	
	public float swimForce = 40f;
   	public Transform waterLevel;
	private bool isUnderwater;
	

    public Animator animator;

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

	public GameObject jumpBoost;
	public GameObject loader;

	public GameObject schody;
    public GameObject portalPrefab;
    private Vector3 portalPosition;

    public GameObject TutorialCanvas;
    public bool tutorialActive = true;


    public int additionalOxygenPoints = 5; // Liczba dodatkowych punkt w tlenu po zebraniu obiektu "Bubble"

    public void Start()
    {
        if (!tutorialActive)
        {
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            respawnPoint = respaPoint.position;
            isUnderwater = false;
            UpdateUI();

        }
        portalPrefab.SetActive(false);

    }

    void Awake()
	{	
		stamina = totalStamina;
	}


    void Update()
   {
		vertical = Input.GetAxisRaw("Vertical");		

	    if (Input.GetKeyDown(KeyCode.Space) && isUnderwater)
	    {
	 	    rb.velocity = new Vector2(vertical * speed, rb.velocity.x);
            rb.AddForce(Vector2.up * swimForce, ForceMode2D.Impulse);	
		
	    }
	

     	horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
		
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
		
        }
		
	
        Flip();

        //if (direction == 0f)
        //{
         //   animator.SetBool("isWalking", false);
       // }

       // else
        //{
         //   animator.SetBool("isWalking", true);
       // }

        if (score >= maxOrb)
        {
            SpawnPortal();
        }
	
	    if(Input.GetKey(KeyCode.LeftShift) && stamina > 0)
	    {
		    particleSprint.Play();
 	        Color objectColor = staminaBar.GetComponent<Renderer>().material.color;
            objectColor.a += appearSpeed * Time.deltaTime;
	        objectColor.a = Mathf.Clamp(objectColor.a, minAlpha, maxAlpha);
            staminaBar.GetComponent<Renderer>().material.color = objectColor;

	        Color barColor = staminaBackground.GetComponent<Renderer>().material.color;
            barColor.a += appearSpeed * Time.deltaTime;
	        barColor.a = Mathf.Clamp(barColor.a, minAlpha, maxAlpha);
            staminaBackground.GetComponent<Renderer>().material.color = barColor;
		
		    speed = 12;
		    stamina -= 0.5f;
	    }
		
	    else
	    {	
		    particleSprint.Stop();

		    speed = 6;
	    }
	
	    if(stamina == 100)
	    {
		    Color objectColor = staminaBar.GetComponent<Renderer>().material.color;
            objectColor.a -= disappearSpeed * Time.deltaTime;
	        objectColor.a = Mathf.Clamp(objectColor.a, minAlpha, maxAlpha);
            staminaBar.GetComponent<Renderer>().material.color = objectColor;

	        Color barColor = staminaBackground.GetComponent<Renderer>().material.color;
            barColor.a -= disappearSpeed * Time.deltaTime;
	        barColor.a = Mathf.Clamp(barColor.a, minAlpha, maxAlpha);
            staminaBackground.GetComponent<Renderer>().material.color = barColor;	
	    }

	    if(stamina < 100 && !Input.GetKey(KeyCode.LeftShift))
	    {
		    stamina += 0.25f;
	    }

	    if(staminaBar != null)
	    {
		    staminaBar.transform.localScale = new Vector2(stamina / totalStamina, staminaBar.transform.localScale.y);
	    }
	
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

	private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
	
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            particleJump.Play();
	    }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            transform.position = respawnPoint;
            LoseLife();

        }


        if (collision.tag == "NextLevel")
        {
		    LoadNextLevel();
		    loader.SetActive(true);

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
	
	    else if (collision.CompareTag("Heart"))
	    {
		    CollectHeart(collision.gameObject);
	    }

	    else if (collision.CompareTag("Water"))
	    {
		    isUnderwater = true;
		    underwater.Play();
		
	    }

	    else if (collision.CompareTag("Jump"))
	    {
		
		    Jumping(collision.gameObject);
		    JumpingBoot();
	    }

	    else if (collision.CompareTag("Platforma"))
	    {
	        collision.transform.parent = this.transform;
		
	    }

    }

	
    void OnTriggerExit2D(Collider2D collision)
    {
 	    if (collision.CompareTag("Water"))
	    {
		    isUnderwater = false;
		    Flip();
		    underwater.Stop();
		
		
	    }
	
	    else if (collision.CompareTag("Platforma"))
	    {
		    collision.transform.parent = null;
	    }
	
	
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

    public void Jumping(GameObject jumpBut)
    {
	    StartCoroutine(JumpingBooost(jumpBut));	
    }

    private IEnumerator JumpingBooost(GameObject jumpBut)
    {
	    jumpingPower = 25f;
	
	    jumpBut.SetActive(false);
	
	    yield return new WaitForSeconds(5f);
	

	    jumpBut.SetActive(true);
	
	    jumpingPower = 16f;
	
	
    }

	public void JumpingBoot()
    {

	    StartCoroutine(JumpingIcon());
	
    }

	private IEnumerator JumpingIcon()
    {
	    jumpBoost.SetActive(true);
	    yield return new WaitForSeconds(5f);
	    jumpBoost.SetActive(false);
    }
	public void CollectHeart(GameObject heart)
	{
		if(currentLives < 3)
		{ 
			currentLives++;
			UpdateUI();
			Destroy(heart);
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

           StartCoroutine(RespawnBubble(bubble));
            //
        }
    }


	private IEnumerator RespawnBubble(GameObject bubble)
	{
		bubble.SetActive(false);
	
	    yield return new WaitForSeconds(5f);
	

	    bubble.SetActive(true);
	
	
	}
    private void CollectOrb(GameObject orb)
    {
        score++;
        progressBar.value = (float)score / maxOrb;

        orbCollected++;

        Destroy(orb);
        if (orbCollected >= maxOrb && portalPosition == Vector3.zero)
        {
            // Jeśli portal jeszcze nie istnieje, zapisz pozycję portalu
            Vector3 forwardDirection = transform.right;
            portalPosition = transform.position + forwardDirection * 2f; // Przykładowe przesunięcie portalu względem gracza

        }
        if (orbCollected >= maxOrb && portalPosition != Vector3.zero)
        {
		    complitedText.gameObject.SetActive(true);
            SpawnPortal();
        }
    }
    private void SpawnPortal()
    {
        portalPrefab.SetActive(true);
        /*	    schody.SetActive(true);*/

        // Utwórz portal na wcześniej zapisanej pozycji
        GameObject portal = Instantiate(portalPrefab, portalPosition, Quaternion.identity);

        // Obróć portal w kierunku patrzenia gracza
        Vector3 directionToPlayer = transform.right;
        portal.transform.right = directionToPlayer;

        portalPosition = Vector3.zero; // Zresetuj pozycję portalu, aby nie aktywować go ponownie

    }
    private void LoadNextLevel()
    {
        if (!string.IsNullOrEmpty(nextLevelScene))
        {
            
            SceneManager.LoadScene(nextLevelScene);
        }

    }
}
    
