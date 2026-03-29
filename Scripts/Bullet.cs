using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float seconds = 5f;
    public float damage = 10f;

    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.right * speed;
        Destroy(gameObject, seconds);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage); // Enemy takes damage if collided with a bullet
            }

            Destroy(gameObject);
        }
    }
}
