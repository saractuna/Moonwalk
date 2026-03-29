using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    #region Variables
    public float moveSpeed = 5f;
    public float sprintMultiplier = 1.5f;
    public float energyDrainRate = 20f;
    public Transform background;

    private float currentEnergy;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Vector2 movement;
    public bool isDead = false;
    private bool isSprinting = false;

    [SerializeField] private float maxEnergy = 100f;
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;
    private Color originalColor;
    [SerializeField] private float flashDuration = 0.1f;
    public static PlayerController Instance;

    public TMP_Text scoreText;
    public int currentScore = 0;
    #endregion

    // For a singleton setup
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        currentEnergy = maxEnergy;
        currentHealth = maxHealth;
        originalColor = spriteRenderer.color;
    }

    // Handles input, sprint logic, animation, flipping, and background tracking
    void Update()
    {
        if (isDead) return;

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetBool("isMoving", movement.sqrMagnitude > 0.1f);

        bool wantsToSprint = Input.GetKey(KeyCode.LeftShift) && currentEnergy > 0 && movement.sqrMagnitude > 0.1f;
        isSprinting = wantsToSprint;

        if (isSprinting)
        {
            currentEnergy -= energyDrainRate * Time.deltaTime;
            currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
        }

        // Flips the sprite towards movement or mouse (for firing)
        if (movement.sqrMagnitude > 0.1f)
        {
            if (movement.x > 0) spriteRenderer.flipX = false;
            else if (movement.x < 0) spriteRenderer.flipX = true;
        }
        else
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            spriteRenderer.flipX = mouseWorldPos.x < transform.position.x;
        }

        // Keep the background centered on the player
        if (background != null)
        {
            background.position = new Vector3(transform.position.x, transform.position.y, background.position.z);
        }
    }

    void FixedUpdate()
    {
        if (isDead) return;

        float speed = isSprinting ? moveSpeed * sprintMultiplier : moveSpeed;
        rb.MovePosition(rb.position + movement.normalized * speed * Time.fixedDeltaTime);
    }

    public void Death()
    {
        isDead = true;
        animator.SetBool("isDead", true);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Energy"))
        {
            currentEnergy = Mathf.Min(currentEnergy + maxEnergy * 0.5f, maxEnergy);
            currentHealth = Mathf.Min(currentHealth + maxHealth * 0.25f, maxHealth);
            Destroy(other.gameObject);
        }
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        StartCoroutine(FlashRed());

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Death();
        }
    }

    // For a brief red flash effect when hit
    IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = originalColor;
    }

    public float GetHealthPercent()
    {
        return currentHealth / maxHealth;
    }

    public float GetEnergyPercent()
    {
        return currentEnergy / maxEnergy;
    }

    public void AddScore(int points)
    {
        currentScore += points;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentScore.ToString();
        }
    }
}