using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region
    public float health = 100f;
    public float moveSpeed = 2f;
    public float damage = 10f;
    public float attackCooldown = 1f;
    public GameObject deathEffectPrefab;
    [SerializeField] private GameObject energyStarPrefab;
    [SerializeField] private float dropChance = 0.75f;
    protected Transform player;
    protected Animator animator;
    protected float lastAttackTime;
    protected SpriteRenderer spriteRenderer;
    public int scoreValue = 10;
    #endregion

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        MoveTowardsPlayer();
    }

    protected void MoveTowardsPlayer()
    {
        if (player == null) return;

        Vector2 direction = (player.position - transform.position).normalized; // This is the player position

        // This is to make sure they turn when they go the opposite way
        if (direction.x > 0.1f)
        {
            spriteRenderer.flipX = false;
        }
        else if (direction.x < -0.1f)
        {
            spriteRenderer.flipX = true;
        }

        transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime); // This moves them to the said position
    }

    public virtual void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        if (deathEffectPrefab != null)
        {
            Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        }

        TryDropEnergyStar();
        Destroy(gameObject);
        PlayerController.Instance.AddScore(scoreValue); // Adds to score if they are killed
    }

    void TryDropEnergyStar()
    {
        if (Random.value <= dropChance)
        {
            Instantiate(energyStarPrefab, transform.position, Quaternion.identity); //Drops an energy star with a chance
        }
    }

    // Damages the player on collision, then destroys the enemy
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
