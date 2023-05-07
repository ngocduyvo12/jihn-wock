using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float damage = 10f;
    public Vector3 direction;
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        rb.velocity = direction * moveSpeed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();
        PlayerHealth playerMove = collision.GetComponent<PlayerHealth>();
        if (collision.gameObject != enemy)
        {
            Destroy(gameObject);
        }
        if (playerMove != null)
        {
            playerMove.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();
        PlayerHealth playerMove = collision.gameObject.GetComponent<PlayerHealth>();
        if (collision.gameObject != enemy)
        {
            Destroy(gameObject);
        }
        if (playerMove != null)
        {
            playerMove.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}